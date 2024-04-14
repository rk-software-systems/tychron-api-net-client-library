using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.Sms;

/// <summary>
/// An object containing the UDH information of the request if it was split into multiple messages.
/// </summary>
/// <param name="RefNum">The number assigned to the segments that is used identify them for transport.</param>
/// <param name="Count">The number of segments that were created for the request.</param>
public record class SmsUdh(
    [property: JsonPropertyName("ref_num")] int? RefNum,
    [property: JsonPropertyName("count")] int? Count
    );
