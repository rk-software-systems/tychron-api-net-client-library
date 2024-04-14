namespace RKSoftware.Tychron.APIClient.Error;

/// <summary>
/// Response Codes
/// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-codes"/>
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TychronApiException"/> class.
/// </remarks>
/// <param name="requestId">An ID used to identify the HTTP request.</param>
/// <param name="statusCode">Response HTTP Status Code.</param>
/// <param name="message">Error message.</param>
public class TychronApiException(string? requestId, int statusCode, string message) : Exception(message)
{
    private readonly string? _requestId = requestId;
    private readonly int _statusCode = statusCode;

    /// <summary>
    /// An ID used to identify the HTTP request.
    /// </summary>
    public string? RequestId => _requestId;

    /// <summary>
    /// Response HTTP Status Code
    /// </summary>
    public int StatusCode => _statusCode;
}
