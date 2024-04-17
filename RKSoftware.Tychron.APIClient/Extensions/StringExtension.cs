using System.Text.Json;
using RKSoftware.Tychron.APIClient.Models;

namespace RKSoftware.Tychron.APIClient.Extensions;

/// <summary>
/// String extensions
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Convert string to ErrorResponse
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    internal static ErrorResponse? ToErrorResponse(this string json)
    {
        ArgumentNullException.ThrowIfNull(json, nameof(json));

        try
        {
            return JsonSerializer.Deserialize<ErrorResponse>(json);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    internal static Dictionary<string, ErrorItem>? ToErrorItemDictionary(this string json)
    {
        ArgumentNullException.ThrowIfNull(json, nameof(json));

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, ErrorItem>>(json);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
