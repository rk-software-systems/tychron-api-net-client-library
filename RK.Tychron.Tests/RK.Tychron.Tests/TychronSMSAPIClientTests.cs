using RK.Tychron.APIClient;
using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.Tests.Factories;
using System.Net;

namespace RK.Tychron.Tests;

public class TychronSMSAPIClientTests
{
    private SendSmsRequest _validPayloadSendSms = null!;
    private SendSmsDlrRequest _validPayloadSendSmsDlr = null!;

    [SetUp]
    public void Setup()
    {
        _validPayloadSendSms = new SendSmsRequest
        {
            Body = "Sample body",
            To = ["123456777", "123456788", "123456799"],
            From = "123456789"
        };

        _validPayloadSendSmsDlr = new SendSmsDlrRequest
        {
            From = "123456789",
            SmsId = "01GTFCEBFR5RJG3RWXCD8QRDZN"
        };
    }

    [Test]
    public async Task TychronSMSAPIClient_SendSMS_Deserialization_OK()
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
        var result = await tychronSMSAPIClient.SendSms(_validPayloadSendSms);

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
    public void TychronSMSAPIClient_SendSMS_FailOnNonSuccessHttpResponse()
    {
        //Arrange
        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.BadRequest),
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronAPIException>(async () => await tychronSMSAPIClient.SendSms(_validPayloadSendSms));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    // Unit Test that PartiallySuccessful is true when 207 status code is returned
    [Test]
    public async Task TychronSMSAPIClient_SendSMS_PartialFailOn207HttpResponse()
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
        var result = await tychronSMSAPIClient.SendSms(_validPayloadSendSms);

        //Assert
        Assert.That(result.PartialFailure, Is.True);
    }

    // Unit Test that Tychron Validation Exception is thrown when To is null or Empty Array
    [Test]
    [TestCase(null, "123", "123", TychronSMSAPIClient.ToRequiredErrorCode)]
    [TestCase("123", "123", null, TychronSMSAPIClient.BodyRequiredErrorCode)]
    [TestCase("123", "123", "", TychronSMSAPIClient.BodyRequiredErrorCode)]
    [TestCase("123", null, "123", TychronSMSAPIClient.FromRequiredErrorCode)]
    [TestCase("123", "", "", TychronSMSAPIClient.FromRequiredErrorCode)]
    public void TychronSMSAPIClient_SendSMS_FailOnValidation(string to, string from, string body, string errorMessageCode)
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
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronSMSAPIClient.SendSms(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == errorMessageCode), Is.EqualTo(true));
    }

    [Test]
    public async Task TychronSMSAPIClient_SendSmsDlr_Deserialization_Deserialization_OK()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testSmsDlrResponse.json");
        var responseString = await new StreamReader(stream).ReadToEndAsync();


        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseString)
            },
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendSmsDlr(_validPayloadSendSmsDlr);

        //Assert
        Assert.That(result.Messages?.Count, Is.EqualTo(1));
        Assert.That(result.Messages?.Any(x => x.Id == "01GTFCJRXEBPXERXET9J06FS05"), Is.True);
        Assert.That(result.Messages?.Any(x => x.To == "12003004001"), Is.True);
    }
}