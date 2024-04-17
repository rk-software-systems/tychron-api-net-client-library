using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models;

/// <summary>
/// Error reason
/// </summary>
/// <param name="Reason"></param>
public record class ErrorReason(
    [property: JsonPropertyName("reason")] string? Reason
    );
