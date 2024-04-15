using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Sms;

/// <summary>
/// A map containing the user data header (UDH) information of a message.
/// </summary>
/// <param name="RefNum">An ID representing the entire message's reference number in Tychron's network.</param>
/// <param name="Count">Denotes how many parts the entire message consists of.</param>
public record class SmsUdh(
    [property: JsonPropertyName("ref_num")] int? RefNum,
    [property: JsonPropertyName("count")] int? Count
    );
