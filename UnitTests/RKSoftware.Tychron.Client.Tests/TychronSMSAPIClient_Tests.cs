using RKSoftware.Tychron.APIClient;
using RKSoftware.Tychron.APIClient.Error;
using RKSoftware.Tychron.APIClient.Model.SMS;
using RKSoftware.Tychron.Tests.Factories;
using System.Net;

namespace RKSoftware.Tychron.Tests;

public class TychronSMSAPIClient_Tests
{
    private SendSMSRequest _validPayloadSendSMS = null!;

    [SetUp]
    public void Setup()
    {
        _validPayloadSendSMS = new SendSMSRequest
        {
            Body = "Sample body",
            To = ["123456777", "123456788", "123456799"],
            From = "123456789"
        };
    }

    [Test]
    public async Task TychronSMSAPIClient_SendSMS_OK_Deserialization()
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
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

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
    public void SendSMS_Fail_TychronAPINonSuccessHttpResponse()
    {
        //Arrange
        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.BadRequest),
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronAPIException>(async () => await tychronSMSAPIClient.SendSms(_validPayloadSendSMS));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    // Unit Test that PartiallySuccessful is true when 207 status code is returned
    [Test]
    public async Task SendSMS_PartialFail_TychronAPI207HttpResponse()
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
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendSms(_validPayloadSendSMS);

        //Assert
        Assert.That(result.PartialFailure, Is.True);
    }

    [Test]
    [TestCase(null, "123", "123", TychronSMSAPIClient.ToRequiredErrorCode)]
    [TestCase("123", "123", null, TychronSMSAPIClient.BodyRequiredErrorCode)]
    [TestCase("123", "123", "", TychronSMSAPIClient.BodyRequiredErrorCode)]
    [TestCase("123", null, "123", TychronSMSAPIClient.FromRequiredErrorCode)]
    [TestCase("123", "", "", TychronSMSAPIClient.FromRequiredErrorCode)]
    public void SendSMS_Fail_Validation(string to, string from, string body, string errorMessageCode)
    {
        //Arrange
        var payload = new SendSMSRequest
        {
            Body = body,
            To = string.IsNullOrEmpty(to) ? null : [to],
            From = from,
        };

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK),
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronSMSAPIClient.SendSms(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == errorMessageCode), Is.EqualTo(true));
    }
}