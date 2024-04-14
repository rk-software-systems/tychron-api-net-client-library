using System.Text.Json.Serialization;
using RKSoftware.Tychron.APIClient.Models;

namespace RKSoftware.Tychron.APIClient.Model.Mms;

/// <summary>
/// Response Record model
/// </summary>
/// <param name="Id">The ID supplied by the system to identify the message.</param>
/// <param name="Type">The created message type which may be an <i>"SMS"</i> or <i>"MMS"</i> message based on account settings.</param>
/// <param name="ReferenceId">The ID originally supplied in the requests.</param>
/// <param name="InsertedAt">Denotes when the message's status was last modified.</param>
/// <param name="From">The sender number.</param>
/// <param name="To">The recipient number, which is identical to the key.</param>
/// <param name="RequestDeliveryReport">Denotes under what conditions a delivery report be sent back for the message.</param>
/// <param name="RequestReadReplyReport">Denotes under what conditions a read-reply report be sent back for the message.</param>
public record class MmsRecord(
    [property:JsonPropertyName("id")] string Id,
    [property:JsonPropertyName("type")] string Type,
    [property:JsonPropertyName("reference_id")] string? ReferenceId,
    [property:JsonPropertyName("inserted_at")] DateTime InsertedAt,
    [property:JsonPropertyName("from")] string From,
    [property:JsonPropertyName("to")] CustomList<string> To,
    [property:JsonPropertyName("request_delivery_report")] bool? RequestDeliveryReport,
    [property:JsonPropertyName("request_read_reply_report")] bool? RequestReadReplyReport
    );