using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.APIClient;
using RK.Tychron.Tests.Factories;
using System.Net;
using RK.Tychron.APIClient.Model.MMS;
using System.Linq;

namespace RK.Tychron.Tests;

public class TychronMMSAPIClientTests
{
    private SendMmsRequest _validPayloadSendMms = null!;

    [SetUp]
    public void Setup()
    {
        _validPayloadSendMms = new SendMmsRequest
        {
            Id = "my_message_id",
            TransactionId = "my_message_request@example.com",
            To = ["+12003004001", "+12003004002", "+12003004003"],
            RequestDeliveryReport = true,
            RequestReadReplyReport = false,
            Subject = null,
            Parts =
            [
                new Part
                {
                    Id = "text-part",
                    Body = "This is text"
                },
                new Part
                {
                    Id = "text-part",
                    Uri = "https://www.pngall.com/wp-content/uploads/2016/03/Tree-Free-PNG-Image.png",
                },
                new Part
                {
                    Id = "embedded-content-part",
                    Body = "BASE64DIGEST",
                    TransferEncoding = "base64"
                },
            ],
            From = "+12003004000"
        };
    }

    [Test]
    public async Task TychronMMSAPIClient_SendMMS_Deserialization_OK()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testMmsResponse.json");
        var responseString = await new StreamReader(stream).ReadToEndAsync();


        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseString)
            },
            HttpMethod.Post);
        var tychronSMSAPIClient = new TychronMMSAPIClient(httpClient);

        //Act
        var result = await tychronSMSAPIClient.SendMms(_validPayloadSendMms);

        //Assert
        Assert.That(result.Messages?.Records?.Count, Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.Messages?.Records?.Select(x => x.To.Any(r => r == "12003004001")).Any(), Is.True);
            Assert.That(result.Messages?.Records?.Select(x => x.To.Any(r => r == "12003004002")).Any(), Is.True);
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
        var tychronMMSAPIClient = new TychronMMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronAPIException>(async () => await tychronMMSAPIClient.SendMms(_validPayloadSendMms));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
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
}