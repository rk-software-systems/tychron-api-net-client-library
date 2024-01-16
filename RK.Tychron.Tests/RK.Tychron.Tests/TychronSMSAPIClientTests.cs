using RK.Tychron.APIClient;
using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.Tests.Factories;
using System.Net;

namespace RK.Tychron.Tests;

public class TychronSMSAPIClientTests
{
    private SendSmsRequest _validPayload = null!;

    [SetUp]
    public void Setup()
    {
        _validPayload = new SendSmsRequest
        {
            Body = "Sample body",
            To = ["123456777", "123456788", "123456799"],
            From = "123456789",
        };
    }

    [Test]
    public async Task TestingReceivingResponseToSmsMessagesAsCorrect()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testResponse.json");
        var responseString = await new StreamReader(stream).ReadToEndAsync();


        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseString) },
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendSms(_validPayload);

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
    public void TestingReceivingResponseToSmsMessagesAsException()
    {
        //Arrange
        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.BadRequest),
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronAPIException>(async () => await tychronSMSAPIClient.SendSms(_validPayload));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    // Unit Test that PartiallySuccessful is true when 207 status code is returned
    [Test]
    public async Task TestingReceivingResponseToSmsMessagesAsPartiallySuccessful()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testResponse.json");
        var responseString = await new StreamReader(stream).ReadToEndAsync();

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.MultiStatus) { Content = new StringContent(responseString) },
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronSMSAPIClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendSms(_validPayload);

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
    public void TestingReceivingResponseToSmsMessagesAsTychronValidationException(string to, string from, string body, string messageCode)
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
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == messageCode), Is.EqualTo(true));
    }
}