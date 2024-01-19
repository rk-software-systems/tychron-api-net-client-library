﻿using RK.Tychron.APIClient.Models;

using System.Text.Json.Serialization;
using RK.Tychron.APIClient.Error;
using static System.Runtime.InteropServices.JavaScript.JSType;
using RK.Tychron.APIClient.TextResources;

namespace RK.Tychron.APIClient.Model.MMS.Webhooks;

/// <summary>
/// Mms Webhook Model
/// </summary>
public class MmsWebhookModel : IValidationSubject
{
    /// <summary>
    /// The ID used to identify the message.
    /// <para>
    /// Example:
    /// <code>"01E7NBVFJA6GQTEEV0YAQP9EMT" </code>
    /// </para>  
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Denotes whether the message is an MMS Forward Request, or an MMS Delivery Report Request.
    /// <para>
    /// Example:
    /// <code>"mms_forward_req" </code>
    /// </para> 
    /// </summary>
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    /// <summary>
    /// The timestamp when the message was received by Tychron's system initially.
    /// <para>
    /// Example:
    /// <code>"2020-04-18T11:30:00.000000Z"</code>
    /// </para>
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime? InsertedAt { get; set; }

    /// <summary>
    /// The timestamp when the message was sent from Tychron's mailing system.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// The sending number that will appear in the message.
    /// The number must be formatted in a plain format, e.g. (12003004000).
    /// <para>
    /// Example:
    /// <code> "12003004000" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// The recipient numbers. The numbers will be formatted in a plain format,
    /// e.g (12003004000). The MMS API currently does not support multiple recipients,
    /// but this field is provided as a list.
    /// <para>
    /// Example:
    /// <code> ["12003004001", "12003004002"] </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("to")]
    public List<string>? To { get; set; }

    /// <summary>
    /// A map containing basic information on the Campaign associated with this message.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public CspCampaign? CspCampaign { get; set; }

    /// <summary>
    /// The carrier or service provider of the remote_number.
    /// <para>
    /// Example:
    /// <code> "ACME Corp." </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    /// <summary>
    /// A Tychron issued ID for grouping service providers for billing purposes.
    /// <para>
    /// Example:
    /// <code> "us_acme" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    /// <summary>
    /// MMS has a recursive structure, in which the root Part may have more sub parts.
    /// Typically this will be a SMIL, Text and an Image, Audio, or Video part.
    /// </summary>
    [JsonPropertyName("data")]
    public Data? Data { get; set; }

    /// <summary>
    /// A map containing miscellaneous information about the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    public List<TychronValidationError> Validate()
    {
        // Validate: id, timestamp, inserted_at, kind, from, to, metadata, data

        var result = new List<TychronValidationError>();

        if (string.IsNullOrEmpty(Id))
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = IdRequiredErrorCode,
                FieldName = nameof(Id),
                Message = ValidationMessages.ReceiveMmsIdRequired
            });
        }

        if (Timestamp == null)
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = TimestampRequiredErrorCode,
                FieldName = nameof(Timestamp),
                Message = ValidationMessages.ReceiveMmsTimestampRequired
            });
        }

        if (InsertedAt == null)
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = InsertedAtRequiredErrorCode,
                FieldName = nameof(InsertedAt),
                Message = ValidationMessages.ReceiveMmsInsertedAtRequired
            });
        }

        if (string.IsNullOrEmpty(Kind))
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = KindRequiredErrorCode,
                FieldName = nameof(Kind),
                Message = ValidationMessages.ReceiveMmsKindRequired
            });
        }

        if (string.IsNullOrEmpty(From))
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = FromRequiredErrorCode,
                FieldName = nameof(From),
                Message = ValidationMessages.ReceiveMmsFromRequired
            });
        }

        if (To == null || To.Count == 0)
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = ToRequiredErrorCode,
                FieldName = nameof(To),
                Message = ValidationMessages.ReceiveMmsToRequired
            });
        }

        if (Metadata == null)
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = MetadataRequiredErrorCode,
                FieldName = nameof(Metadata),
                Message = ValidationMessages.ReceiveMmsMetadataRequired
            });
        }

        if (Data == null)
        {
            result.Add(new TychronValidationError
            {
                ErrorCode = DataRequiredErrorCode,
                FieldName = nameof(Data),
                Message = ValidationMessages.ReceiveMmsDataRequired
            });
        }

        return result;
    }

    public const string IdRequiredErrorCode = "ReceiveMMS_Id_Required";
    public const string TimestampRequiredErrorCode = "ReceiveMMS_Timestamp_Required";
    public const string InsertedAtRequiredErrorCode = "ReceiveMMS_InsertedAt_Required";
    public const string KindRequiredErrorCode = "ReceiveMMS_Kind_Required";
    public const string FromRequiredErrorCode = "ReceiveMMS_From_Required";
    public const string ToRequiredErrorCode = "ReceiveMMS_To_Required";
    public const string StatusCodeRequiredErrorCode = "ReceiveMMS_StatusCode_Required";
    public const string MetadataRequiredErrorCode = "ReceiveMMS_Metadata_Required";
    public const string DataRequiredErrorCode = "ReceiveMMS_Data_Required";
}