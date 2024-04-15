using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Mms;

/// <summary>
/// A map containing miscellaneous information about the request.
/// </summary>
public record class Metadata
{
    /// <summary>
    /// A list of numbers found in the `headers.to`.
    /// </summary>
    [JsonPropertyName("to")]
    public CustomList<string>? To { get; set; }

    /// <summary>
    /// A list of numbers found in the `headers.cc`.
    /// </summary>
    [JsonPropertyName("cc")]
    public CustomList<string>? Cc { get; set; }

    /// <summary>
    /// A list of numbers found in the `headers.bcc`.
    /// </summary>
    [JsonPropertyName("bcc")]
    public CustomList<string>? Bcc { get; set; }

    /// <summary>
    /// Combined list of `to`, `cc` and `bcc`.
    /// </summary>
    [JsonPropertyName("recipients")]
    public CustomList<string>? Recipients { get; set; }
}