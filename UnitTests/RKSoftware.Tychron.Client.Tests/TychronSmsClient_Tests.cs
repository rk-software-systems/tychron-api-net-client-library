using RKSoftware.Tychron.APIClient;
using RKSoftware.Tychron.APIClient.Error;
using RKSoftware.Tychron.APIClient.Model.Sms;
using RKSoftware.Tychron.Tests.Factories;
using System.Net;

namespace RKSoftware.Tychron.Tests;

public class TychronSmsClient_Tests
{
    private SendSmsRequest _validPayloadSendSMS = null!;

    [SetUp]
    public void Setup()
    {
        _validPayloadSendSMS = new SendSmsRequest
        {
            Body = "Sample body",
            To = ["123456777", "123456788", "123456799"],
            From = "123456789"
        };
    }

    [Test]
    public async Task SendSms_OK_Deserialization()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testSmsResponse.json");
        var responseString = await new StreamReader(stream).ReadToEndAsync();


        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseString)
            },
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
        //Arrange
        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.BadRequest),
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSmsClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronApiException>(async () => await tychronSMSAPIClient.SendSms(_validPayloadSendSMS));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    // Unit Test that PartiallySuccessful is true when 207 status code is returned
    [Test]
    public async Task SendSms_PartialFail_TychronAPI207HttpResponse()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testSmsResponse.json");
        var responseString = await new StreamReader(stream).ReadToEndAsync();

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.MultiStatus)
            {
                Content = new StringContent(responseString)
            },
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSmsClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendSms(_validPayloadSendSMS);

        //Assert
        Assert.That(result.PartialFailure, Is.True);
    }

    [Test]
    [TestCase(null, "123", "123", TychronSmsClient.ToRequiredErrorCode)]
    [TestCase("123", "123", null, TychronSmsClient.BodyRequiredErrorCode)]
    [TestCase("123", "123", "", TychronSmsClient.BodyRequiredErrorCode)]
    [TestCase("123", null, "123", TychronSmsClient.FromRequiredErrorCode)]
    [TestCase("123", "", "", TychronSmsClient.FromRequiredErrorCode)]
    public void SendSms_Fail_Validation(string? to, string? from, string? body, string errorMessageCode)
    {
        //Arrange
        var payload = new SendSmsRequest
        {
            Body = body,
            To = string.IsNullOrEmpty(to) ? null : [to],
            From = from,
        };

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK),
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSmsClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronSMSAPIClient.SendSms(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == errorMessageCode), Is.EqualTo(true));
    }
}