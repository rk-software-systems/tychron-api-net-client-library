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

        return JsonSerializer.Deserialize<ErrorResponse>(json);
    }

    internal static Dictionary<string, ErrorItem>? ToErrorItemDictionary(this string json)
    {
        ArgumentNullException.ThrowIfNull(json, nameof(json));

        return JsonSerializer.Deserialize<Dictionary<string, ErrorItem>>(json);
    }
}
