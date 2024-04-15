using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.MmsDlr;

/// <summary>
/// A map containing only the ID of the respective MMS this DLR belongs to.
/// </summary>
/// <param name="Id">
/// The Tychron ULID for the original MMS.
/// <example>
/// <code>"01E7NBVFJA6GQTEEV0YAQP9EMT"</code>
/// </example>
/// </param>
public record class Mms(
    [property: JsonPropertyName("id")] string? Id
    );