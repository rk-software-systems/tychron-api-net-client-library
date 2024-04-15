using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Dlr;

/// <summary>
/// Dlr webhook model
/// </summary>
/// <param name="Data"></param>
/// <param name="Type"></param>
public record class DlrWebhookModel(
    [property: JsonPropertyName("data")] DlrData? Data,
    [property: JsonPropertyName("type")] string? Type
    );

