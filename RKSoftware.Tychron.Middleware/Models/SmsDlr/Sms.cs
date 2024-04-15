using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.SmsDlr;

/// <summary>
/// An object containing limited information on the SMS that the DLR belongs to
/// </summary>
/// <param name="Id">The id of the SMS message part associated with the DLR.</param>
public record class Sms(
    [property: JsonPropertyName("id")] string? Id
       );
