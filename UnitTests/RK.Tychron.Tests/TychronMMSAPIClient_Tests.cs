using RK.Tychron.APIClient;
using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.MMS;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.Tests.Factories;
using System.Net;

namespace RK.Tychron.Tests;

public class TychronMMSAPIClient_Tests
{
    private SendMMSRequest _validPayloadSendMMS = null!;

    [SetUp]
    public void Setup()
    {
        _validPayloadSendMMS = new SendMMSRequest
        {
            Id = "my_message_id",
            TransactionId = "my_message_request@example.com",
            To = ["+12003004001", "+12003004002", "+12003004003"],
            RequestDeliveryReport = true,
            RequestReadReplyReport = false,
            Subject = null,
            Parts =
            [
                new MMSPart
                {
                    Id = "text-part",
                    Body = "This is text"
                },
                new MMSPart
                {
                    Id = "text-part",
                    Uri = "https://www.pngall.com/wp-content/uploads/2016/03/Tree-Free-PNG-Image.png",
                },
                new MMSPart
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
    public async Task SendMMS_OK_Deserialization()
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
        var result = await tychronSMSAPIClient.SendMMS(_validPayloadSendMMS);

        //Assert
        Assert.That(result?.Records?.Count, Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result?.Records?.Select(x => x.To.Any(r => r == "12003004001")).Any(), Is.True);
            Assert.That(result?.Records?.Select(x => x.To.Any(r => r == "12003004002")).Any(), Is.True);
        });
    }

    //Unit Test Tychron API Exception on non 200, 207 status codes
    [Test]
    public void SendMMS_Fail_TychronAPINonSuccessHttpResponse()
    {
        //Arrange
        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.BadRequest),
            HttpMethod.Post);

        var tychronMMSAPIClient = new TychronMMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronAPIException>(async () => await tychronMMSAPIClient.SendMMS(_validPayloadSendMMS));

        //Assert
        Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    [Test]
    public void SendMMS_Fail_ValidationFromRequired()
    {
        //Arrange
        var payload = new SendMMSRequest
        {
            To = ["+12003004001", "+12003004002", "+12003004003"],
            From = "",
            Parts = []
        };

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK),
            HttpMethod.Post);
        
        var tychronMMSAPIClient = new TychronMMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronMMSAPIClient.SendMMS(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == TychronMMSAPIClient.FromRequiredErrorCode), Is.EqualTo(true));
    }

    [Test]
    public void SendMMS_Fail_ValidationToRequired()
    {
        //Arrange
        var payload = new SendMMSRequest
        {
            To = [],
            From = "+12003004000",
            Parts = []
        };

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK),
            HttpMethod.Post);

        var tychronMMSAPIClient = new TychronMMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronMMSAPIClient.SendMMS(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == TychronMMSAPIClient.ToRequiredErrorCode), Is.EqualTo(true));
    }

    [Test]
    public void SendMMS_Fail_ValidationPartsRequired()
    {
        //Arrange
        var payload = new SendMMSRequest
        {
            To = ["1231"],
            From = "+12003004000",
            Parts = []
        };

        var httpClient = HttpClientMockFactory.GetHttpClientMock(
            new HttpResponseMessage(HttpStatusCode.OK),
            HttpMethod.Post);

        var tychronMMSAPIClient = new TychronMMSAPIClient(httpClient);

        //Act
        var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronMMSAPIClient.SendMMS(payload));

        //Assert
        Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == TychronMMSAPIClient.PartRequiredErrorCode), Is.EqualTo(true));
    }
}