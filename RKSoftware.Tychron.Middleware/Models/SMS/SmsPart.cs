using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Sms;

/// <summary>
/// This object represents  Part in a multipart message..
/// </summary>
/// <param name="Id"> The ID of a single SMS segment.</param>
public record class SmsPart(
    [property: JsonPropertyName("id")] string? Id
    );
