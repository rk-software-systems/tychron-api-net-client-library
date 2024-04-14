using RKSoftware.Tychron.APIClient.Models;

namespace RKSoftware.Tychron.APIClient.Model.Sms;

/// <summary>
/// This object represents SMS message Tychron API response.
/// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-format"/>
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="XRequestId">An ID used to identify the HTTP request.</param>
/// <param name="Messages">Message responses</param>
/// <param name="PartialFailure">
/// This flag is set to true when we obtain the following response from Tychron API (HTTP Status Code 207)
/// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-codes"/>
/// </param>
public record class BaseSmsResponse<T>(string? XRequestId, CustomList<T>? Messages, bool PartialFailure);
