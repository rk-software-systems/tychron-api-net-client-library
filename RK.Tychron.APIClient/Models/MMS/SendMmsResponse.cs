namespace RK.Tychron.APIClient.Model.MMS;

/// <summary>
/// This object represents MMS message Tychron API response.
/// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#response-format"/>
/// </summary>
public class SendMmsResponse<T>
{
    /// <summary>
    /// An ID used to identify the HTTP request.
    /// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#response-headers"/>
    /// </summary>
    public string? XRequestID { get; set; }

    /// <summary>
    /// Message responses
    /// </summary>
    public T? Messages { get; init; }
}