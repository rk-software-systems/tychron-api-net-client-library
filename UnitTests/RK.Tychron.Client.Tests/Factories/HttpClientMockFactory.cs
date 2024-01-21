using Moq;
using Moq.Protected;

namespace RK.Tychron.Tests.Factories
{
    internal class HttpClientMockFactory
    {
        internal static HttpClient GetHttpClientMock(HttpResponseMessage response, HttpMethod methodToMock)
        {
            var httpClientHandler = new Mock<HttpMessageHandler>();
            httpClientHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => m.Method == methodToMock),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

            var httpClient = new HttpClient(httpClientHandler.Object);
            httpClient.BaseAddress = new Uri("http://google.com/");
            return httpClient;
        }
    }
}
