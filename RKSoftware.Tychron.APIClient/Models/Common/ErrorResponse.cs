using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models;

/// <summary>
/// Error webhook model
/// </summary>
/// <param name="Errors"></param>
public record class ErrorResponse(
    [property: JsonPropertyName("errors")] CustomList<ErrorItem>? Errors
    );


