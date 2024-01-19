using RK.Tychron.APIClient.Model.MMS.Webhooks;
using System.Text.Json.Serialization;
using RK.Tychron.APIClient.Error;

namespace RK.Tychron.APIClient.Models.MMSDLR.Webhooks;

/// <summary>
/// Mms Dlr Webhook Model
/// </summary>
public class MmsDlrWebhookModel : IValidationSubject
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("from")]
    public string? From { get; set; }

    [JsonPropertyName("to")]
    public List<string>? To { get; set; }

    [JsonPropertyName("csp_campaign")]
    public CspCampaign? CspCampaign { get; set; }

    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    [JsonPropertyName("status_code")]
    public string? StatusCode { get; set; }

    [JsonPropertyName("data")]
    public Data? Data { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    [JsonPropertyName("mms")]
    public Mms? Mms { get; set; }

    public List<TychronValidationError> Validate()
    {
        throw new NotImplementedException();
    }
}