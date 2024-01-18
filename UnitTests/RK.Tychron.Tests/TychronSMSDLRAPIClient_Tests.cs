using RK.Tychron.APIClient;
using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Models.SMSDLR;
using RK.Tychron.Tests.Factories;
using System.Net;

namespace RK.Tychron.Tests
{
    internal class TychronSMSDLRAPIClient_Tests
    {
        private SendSMSDLRRequest _validPayloadSendSMSDLR = null!;

        [SetUp]
        public void Setup()
        {
            _validPayloadSendSMSDLR = new SendSMSDLRRequest
            {
                From = "123456789",
                SmsId = "01GTFCEBFR5RJG3RWXCD8QRDZN"
            };
        }

        [Test]
        public async Task SendSMSDLR_Deserialization_OK_Deserialization()
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
            var tychronSMSDLRAPIClient = new TychronSMSDLRClient(httpClient);

            //Act
            var result = await tychronSMSDLRAPIClient.SendSMSDLR(_validPayloadSendSMSDLR);

            // TODO: remove after Workflow testing
            Assert.That(true, Is.False);
            //Assert
            Assert.That(result.Messages?.Count, Is.EqualTo(1));
            Assert.That(result.Messages?.Any(x => x.Id == "01GTFCJRXEBPXERXET9J06FS05"), Is.True);
            Assert.That(result.Messages?.Any(x => x.To == "12003004001"), Is.True);
        }

        //Unit Test Tychron API Exception on non 200, 207 status codes
        [Test]
        public void SendSMSDLR_Fail_TychronAPINonSuccessHttpResponse()
        {
            //Arrange
            var httpClient = HttpClientMockFactory.GetHttpClientMock(
                new HttpResponseMessage(HttpStatusCode.BadRequest),
                HttpMethod.Post);

            var tychronSMSDLRAPIClient = new TychronSMSDLRClient(httpClient);

            //Act
            var result = Assert.ThrowsAsync<TychronAPIException>(async () => await tychronSMSDLRAPIClient.SendSMSDLR(_validPayloadSendSMSDLR));

            //Assert
            Assert.That(result!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        // Unit Test that PartiallySuccessful is true when 207 status code is returned
        [Test]
        public async Task SendSMSDLR_PartialFail_TychronAPI207HttpResponse()
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

            var tychronSMSDLRAPIClient = new TychronSMSDLRClient(httpClient);

            //Act
            var result = await tychronSMSDLRAPIClient.SendSMSDLR(_validPayloadSendSMSDLR);

            //Assert
            Assert.That(result.PartialFailure, Is.True);
        }

        [Test]
        [TestCase(null, "123", TychronSMSDLRClient.FromRequiredErrorCode)]
        [TestCase("", "123", TychronSMSDLRClient.FromRequiredErrorCode)]
        [TestCase("123", null, TychronSMSDLRClient.SmsIdRequiredErrorCode)]
        [TestCase("123", "", TychronSMSDLRClient.SmsIdRequiredErrorCode)]
        public void SendSMSDLR_Fail_Validation(string from, string smsId, string errorMessageCode)
        {
            //Arrange
            var payload = new SendSMSDLRRequest
            {
                From = from,
                SmsId = smsId
            };

            var httpClient = HttpClientMockFactory.GetHttpClientMock(
                new HttpResponseMessage(HttpStatusCode.OK),
                HttpMethod.Post);

            var tychronSMSDLRAPIClient = new TychronSMSDLRClient(httpClient);

            //Act
            var result = Assert.ThrowsAsync<TychronValidationException>(async () => await tychronSMSDLRAPIClient.SendSMSDLR(payload));

            //Assert
            Assert.That(result!.ValidationErrors.Any(x => x.ErrorCode == errorMessageCode), Is.EqualTo(true));
        }
    }
}
