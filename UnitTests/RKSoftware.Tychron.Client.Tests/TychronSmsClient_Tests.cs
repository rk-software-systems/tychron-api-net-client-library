using RKSoftware.Tychron.APIClient;
using RKSoftware.Tychron.APIClient.Errors;
using RKSoftware.Tychron.APIClient.Models.Sms;
using RKSoftware.Tychron.APIClient.Models;
using RKSoftware.Tychron.Tests.Factories;
using System.Net;

namespace RKSoftware.Tychron.Tests;

public class TychronSmsClient_Tests
{
    private SendSmsRequest _validPayloadSendSMS = null!;

    [SetUp]
    public void Setup()
    {
        _validPayloadSendSMS = new SendSmsRequest("Sample body", "123456789", new CustomList<string>(["123456777", "123456788", "123456799"]));
    }

    [Test]
    public async Task SendSms_OK_Deserialization()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testSmsResponse.json");
        using var reader = new StreamReader(stream);
        var responseString = await reader.ReadToEndAsync();
        using var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseString)
        };


        using var httpClient = HttpClientMockFactory.GetHttpClientMock(
            response,
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSmsClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendSms(_validPayloadSendSMS);

        //Assert
        Assert.That(result.Messages?.Count, Is.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(result.Messages?.Any(x => x.To == "12003004001"), Is.True);
            Assert.That(result.Messages?.Any(x => x.To == "12003004002"), Is.True);
        });
    }

    //Unit Test Tychron API Exception on non 200, 207 status codes
    [Test]
    public void SendSms_Fail_TychronAPINonSuccessHttpResponse()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        //Arrange
        using var httpClient = HttpClientMockFactory.GetHttpClientMock(
            response,
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSmsClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronApiException>(async () => await tychronSMSAPIClient.SendSms(_validPayloadSendSMS));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    [Test]
    [TestCase(null, "123", "123", TychronSmsClient.ToRequiredErrorCode)]
    [TestCase("123", "123", null, TychronSmsClient.BodyRequiredErrorCode)]
    [TestCase("123", "123", "", TychronSmsClient.BodyRequiredErrorCode)]
    [TestCase("123", null, "123", TychronSmsClient.FromRequiredErrorCode)]
    [TestCase("123", "", "", TychronSmsClient.FromRequiredErrorCode)]
    public void SendSms_Fail_Validation(string? to, string? from, string? body, string errorMessageCode)
    {
#pragma warning disable CS8604 // Possible null reference argument for parameter.
        //Arrange
        var payload = new SendSmsRequest(body, from, string.IsNullOrEmpty(to) ? [] : new CustomList<string>([to]));
#pragma warning restore CS8604 // Possible null reference argument for parameter.
        using var response = new HttpResponseMessage(HttpStatusCode.OK);
        using var httpClient = HttpClientMockFactory.GetHttpClientMock(
           response,
           HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSmsClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronSMSAPIClient.SendSms(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == errorMessageCode), Is.EqualTo(true));
    }
}