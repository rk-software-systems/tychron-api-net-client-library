using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models.Sms;

/// <summary>
/// SMS Message part model
/// </summary>
/// <param name="Id">The ID supplied by the system to identify the segment.</param>
public record class SmsPart(
    [property: JsonPropertyName("id")] string? Id
    );
