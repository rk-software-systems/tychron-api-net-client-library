using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.SMS;

/// <summary>
/// An object containing limited information on the SMS that the DLR belongs to
/// </summary>
public class Sms
{
    /// <summary>
    /// The id of the SMS message part associated with the DLR.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}