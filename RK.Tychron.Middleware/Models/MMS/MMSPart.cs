using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Model.MMS;

/// <summary>
/// MMS has a recursive structure, in which the root Part may have more sub parts. Typically this will be a SMIL, Text and an Image, Audio, or Video part.
/// </summary>
public class MMSPart
{
    /// <summary>
    /// A map containing header pairs, keys are downcased for easy indexing.
    /// </summary>
    [JsonPropertyName("headers")]
    public dynamic? Headers { get; set; }

    /// <summary>
    /// An <see href="https://docs.tychron.info/mms-api/receiving-mms-via-http/#encoding">encodings</see> that is applied to the message part's body. The encoding can be either base64 or identity.
    /// <example>
    /// <code>"base64"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("encoding")]
    public string? Encoding { get; set; }

    /// <summary>
    /// A string containing the part's body or data, this will be encoded according to the value of encoding.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// Denotes whether the message contains more than one part, typically MMS messages will always be multipart, but in some rare instances they may just be a large text blob.
    /// </summary>
    [JsonPropertyName("is_multipart")]
    public bool IsMultipart { get; set; }

    /// <summary>
    /// If this part has is_multipart set, then this field will be populated with the further sub parts of the message.
    /// </summary>
    [JsonPropertyName("parts")]
    public List<MMSPart>? Parts { get; set; }
}