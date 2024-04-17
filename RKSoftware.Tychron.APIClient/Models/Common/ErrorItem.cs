using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models;

/// <summary>
/// Error model
/// </summary>
/// <param name="Code"></param>
/// <param name="Detail"></param>
/// <param name="ErrorReason"></param>
/// <param name="SubCode"></param>
/// <param name="Title"></param>
public record class ErrorItem(
    [property: JsonPropertyName("code")] string? Code,
    [property: JsonPropertyName("detail")] string? Detail,
    [property: JsonPropertyName("error")] ErrorReason? ErrorReason,
    [property: JsonPropertyName("sub_code")] string? SubCode,
    [property: JsonPropertyName("title")] string? Title
    )
{
    /// <summary>
    /// Target phone number
    /// </summary>
    [JsonIgnore]
    public string? To { get; set; }
}

