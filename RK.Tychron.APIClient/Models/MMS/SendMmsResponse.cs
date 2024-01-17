using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS;

/// <summary>
/// Send Mms response model
/// </summary>
public class SendMmsResponse
{
    /// <summary>
    /// An array of responses
    /// </summary>
    [JsonPropertyName("records")]
    public List<Record>? Records { get; set; }

    /// <summary>
    /// Error data
    /// </summary>
    [JsonPropertyName("errors")]
    public List<dynamic>? Errors { get; set; }
}