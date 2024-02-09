using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Model.Sms;

/// <summary>
/// This object represents  Part in a multipart message..
/// </summary>
public class SmsPart
{
    /// <summary>
    /// The ID of a single SMS segment.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
