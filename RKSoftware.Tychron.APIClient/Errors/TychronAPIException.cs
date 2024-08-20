using RKSoftware.Tychron.APIClient.Models;

namespace RKSoftware.Tychron.APIClient.Errors;

/// <summary>
/// Response Codes
/// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-codes"/>
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TychronApiException"/> class.
/// </remarks>
/// <param name="statusCode">Response HTTP Status Code.</param>
/// <param name="message">Error message.</param>
/// <param name="requestId">An ID used to identify the HTTP request.</param>
/// <param name="errorResponse">Response HTTP Error model</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
public class TychronApiException( 
    int statusCode, 
    string message,
    string? requestId,
    ErrorResponse? errorResponse) : Exception(message)
{
    private readonly string? _requestId = requestId;
    private readonly int _statusCode = statusCode;
    private readonly ErrorResponse? _errorResponse = errorResponse;
        
    /// <summary>
    /// Response HTTP Status Code
    /// </summary>
    public int StatusCode => _statusCode;

    /// <summary>
    /// An ID used to identify the HTTP request.
    /// </summary>
    public string? RequestId => _requestId;

    /// <summary>
    /// Response HTTP Error model
    /// </summary>
    public ErrorResponse? ErrorResponse => _errorResponse;
}
