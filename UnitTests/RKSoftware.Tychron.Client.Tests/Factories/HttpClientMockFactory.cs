using Moq;
using Moq.Protected;

namespace RKSoftware.Tychron.Tests.Factories;

internal sealed class HttpClientMockFactory
{
    internal static HttpClient GetHttpClientMock(HttpResponseMessage response, HttpMethod methodToMock)
    {
        var httpClientHandler = new Mock<HttpMessageHandler>();
        httpClientHandler.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.Is<HttpRequestMessage>(m => m.Method == methodToMock),
            ItExpr.IsAny<CancellationToken>())
        .ReturnsAsync(response);

        var httpClient = new HttpClient(httpClientHandler.Object)
        {
            BaseAddress = new Uri("http://google.com/")
        };
        return httpClient;
    }
}
