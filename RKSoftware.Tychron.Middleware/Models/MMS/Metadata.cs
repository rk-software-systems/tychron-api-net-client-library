using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Model.Mms;

/// <summary>
/// A map containing miscellaneous information about the request.
/// </summary>
public class Metadata
{
    /// <summary>
    /// A list of numbers found in the `headers.to`.
    /// </summary>
    [JsonPropertyName("to")]
    public List<string>? To { get; set; }

    /// <summary>
    /// A list of numbers found in the `headers.cc`.
    /// </summary>
    [JsonPropertyName("cc")]
    public List<string>? Cc { get; set; }

    /// <summary>
    /// A list of numbers found in the `headers.bcc`.
    /// </summary>
    [JsonPropertyName("bcc")]
    public List<string>? Bcc { get; set; }

    /// <summary>
    /// Combined list of `to`, `cc` and `bcc`.
    /// </summary>
    [JsonPropertyName("recipients")]
    public List<string>? Recipients { get; set; }
}