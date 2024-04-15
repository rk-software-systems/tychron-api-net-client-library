using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Dlr;

/// <summary>
/// Dlr report data
/// </summary>
/// <param name="Id"></param>
/// <param name="HostNumber">
/// Phone number of the host.
/// <para>
/// Example:
/// <code> "12003004000" </code>
/// </para>
/// </param>
/// <param name="RemoteNumber">
/// Phone number of the remote.
/// <para>
/// Example:
/// <code> "12003004000" </code>
/// </para>
/// </param>
/// <param name="Type">Message type: SMS or MMS</param>
/// <param name="Subtype">Message direction: INBOUND or OUTBOUND</param>
/// <param name="Status">Message processing status: OK</param>
/// <param name="Description"></param>
/// <param name="ItemCount"></param>
/// <param name="ItemId"></param>
/// <param name="SellRate"></param>
/// <param name="Surcharges"></param>
/// <param name="Total"></param>
/// <param name="InsertedAt"></param>
/// <param name="UpdatedAt"></param>
/// <param name="StartedAt"></param>
/// <param name="StoppedAt"></param>
public record class DlrData(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("host_number")] string? HostNumber,
    [property: JsonPropertyName("remote_number")] string? RemoteNumber,
    [property: JsonPropertyName("type")] string? Type,
    [property: JsonPropertyName("subtype")] string? Subtype,
    [property: JsonPropertyName("status")] string? Status,
    [property: JsonPropertyName("description")] string? Description,    
    [property: JsonPropertyName("item_count")] int? ItemCount,
    [property: JsonPropertyName("item_id")] string? ItemId,   
    [property: JsonPropertyName("sell_rate")] string? SellRate,
    [property: JsonPropertyName("surcharges")] CustomList<Surcharge>? Surcharges,
    [property: JsonPropertyName("total")] string? Total,    
    [property: JsonPropertyName("inserted_at")] DateTime? InsertedAt,
    [property: JsonPropertyName("updated_at")] DateTime? UpdatedAt,
    [property: JsonPropertyName("started_at")] DateTime? StartedAt,
    [property: JsonPropertyName("stopped_at")] DateTime? StoppedAt
    );
