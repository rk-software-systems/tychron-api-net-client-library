using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Dlr;

/// <summary>
/// Surcharge model
/// </summary>
/// <param name="Cost"></param>
/// <param name="Name"></param>
public record class Surcharge(
    [property: JsonPropertyName("cost")] string? Cost,
    [property: JsonPropertyName("name")] string? Name
    );
