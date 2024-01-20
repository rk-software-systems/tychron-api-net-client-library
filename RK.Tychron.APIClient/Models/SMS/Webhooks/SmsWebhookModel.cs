﻿using RK.Tychron.APIClient.Models;
using System.Text.Json.Serialization;
using RK.Tychron.APIClient.Error;

namespace RK.Tychron.APIClient.Model.SMS;

/// <summary>
/// Sms Webhook Model
/// </summary>
public class SmsWebhookModel : IValidationSubject
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }


    [JsonPropertyName("type")]
    public string? Type { get; set; }


    [JsonPropertyName("from")]
    public string? From { get; set; }


    [JsonPropertyName("to")]
    public string? To { get; set; }


    [JsonPropertyName("csp_campaign")]
    public SmsCspCampaign? CspCampaign { get; set; }


    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }


    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }


    [JsonPropertyName("parts")]
    public List<SmsPart>? Parts { get; set; }


    [JsonPropertyName("body")]
    public string? Body { get; set; }


    [JsonPropertyName("sms_encoding")]
    public int SmsEncoding { get; set; }


    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }


    [JsonPropertyName("udh")]
    public SmsUdh? Udh { get; set; }


    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }


    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }


    [JsonPropertyName("processed_at")]
    public DateTime ProcessedAt { get; set; }


    [JsonPropertyName("delivered_at")]
    public DateTime DeliveredAt { get; set; }


    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; }


    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    public List<TychronValidationError> Validate()
    {
        throw new NotImplementedException();
    }
}