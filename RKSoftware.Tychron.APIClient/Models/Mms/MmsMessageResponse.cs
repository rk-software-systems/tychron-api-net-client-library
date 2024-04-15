using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models.Mms;

/// <summary>
/// Send MMS response model
/// </summary>
/// <param name="Records">
/// An array of responses
/// </param>
/// <param name="Errors">
///  Error data
/// </param>
public record class MmsMessageResponse(
    [property:JsonPropertyName("records")] CustomList<MmsRecord>? Records,
    [property:JsonPropertyName("errors")] CustomList<object>? Errors
    )
{
    /// <summary>
    /// An ID used to identify the HTTP request.
    /// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#response-headers"/>
    /// </summary>
    public string? XRequestId { get; set; }
}