using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Mms;

/// <summary>
/// MMS has a recursive structure, in which the root Part may have more sub parts. Typically this will be a SMIL, Text and an Image, Audio, or Video part.
/// </summary>
/// <param name="Headers">A map containing header pairs, keys are downcased for easy indexing.</param>
/// <param name="Encoding">
/// An <see href="https://docs.tychron.info/mms-api/receiving-mms-via-http/#encoding">encodings</see> that is applied to the message part's body. The encoding can be either base64 or identity.
/// <example>
/// <code>"base64"</code>
/// </example>
/// </param>
/// <param name="Body">A string containing the part's body or data, this will be encoded according to the value of encoding.</param>
/// <param name="IsMultipart">
/// Denotes whether the message contains more than one part, typically MMS messages will always be multipart, 
/// but in some rare instances they may just be a large text blob.</param>
public record class MmsPart(
    [property: JsonPropertyName("headers")] dynamic? Headers,
    [property: JsonPropertyName("encoding")] string? Encoding,
    [property: JsonPropertyName("body")] string? Body,
    [property: JsonPropertyName("is_multipart")] bool? IsMultipart
    )
{
    /// <summary>
    /// If this part has is_multipart set, then this field will be populated with the further sub parts of the message.
    /// </summary>
    [JsonPropertyName("parts")]
    public CustomList<MmsPart>? Parts { get; set; }
}