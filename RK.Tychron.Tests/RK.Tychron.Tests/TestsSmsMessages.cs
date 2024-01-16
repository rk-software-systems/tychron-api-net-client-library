using System.Diagnostics;
using RK.Tychron.APIClient;
using System.Reflection;
using Moq;
using Moq.Protected;
using System.Text.Json.Nodes;
using RK.Tychron.APIClient.Model.SMS;
using System.Text.Json;

namespace RK.Tychron.Tests;

public class TestsSmsMessages
{
    private List<SmsMessageResponseModel>? _messageResponse;

    [SetUp]
    public void Setup()
    {
        _messageResponse = [new SmsMessageResponseModel()];
    }

    [Test]
    public void TestingReceivingResponseToSmsMessagesAsCorrect()
    {
        //Arrange
        using var stream = File.OpenRead("Data/testResponse.json");

        var document = JsonNode.Parse(stream, nodeOptions: new JsonNodeOptions
        {
            PropertyNameCaseInsensitive = false
        });

        _messageResponse = document.AsObject()
            .Where(x => x.Value != null)
            .Select(x => JsonSerializer.Deserialize<SmsMessageResponseModel>(x.Value!.ToJsonString()))
            .ToList();

        var TychronSMSAPIClientMock = new Mock<TychronSMSAPIClient>(new Mock<IHttpClientFactory>());
        TychronSMSAPIClientMock.Protected().Setup<List<SmsMessageResponseModel>>("GetSmsMessageResponse", document)
            .Returns(_messageResponse)
            .Verifiable(); 

        //Act

        //Assert
        TychronSMSAPIClientMock.Verify();
        
    }
}