using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.Mms;

/// <summary>
/// Send MMS response model
/// </summary>
public class MmsMessageResponse
{
    /// <summary>
    /// An ID used to identify the HTTP request.
    /// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#response-headers"/>
    /// </summary>
    public string? XRequestId { get; set; }

    /// <summary>
    /// An array of responses
    /// </summary>
    [JsonPropertyName("records")]
    public List<MmsRecord>? Records { get; set; }

    /// <summary>
    /// Error data
    /// </summary>
    [JsonPropertyName("errors")]
    public List<object>? Errors { get; set; }
}