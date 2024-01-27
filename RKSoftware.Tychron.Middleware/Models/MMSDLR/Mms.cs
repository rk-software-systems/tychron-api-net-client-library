using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.MMSDLR;

/// <summary>
/// A map containing only the ID of the respective MMS this DLR belongs to.
/// </summary>
public class Mms
{
    /// <summary>
    /// The Tychron ULID for the original MMS.
    /// <example>
    /// <code>"01E7NBVFJA6GQTEEV0YAQP9EMT"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}