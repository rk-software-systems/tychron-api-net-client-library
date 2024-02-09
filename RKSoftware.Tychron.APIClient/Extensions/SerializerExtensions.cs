using System.Text.Json;
using System.Text.Json.Nodes;

namespace RKSoftware.Tychron.APIClient.Extensions;

internal static class SerializerExtensions
{
    /// <summary>
    /// This method is user to deserialize JSON of type: { [key: string]: T } to List of T
    /// </summary>
    /// <typeparam name="T">Type of list items.</typeparam>
    /// <param name="document">Json Node containing objects.</param>
    /// <returns>List of objects deserialized from JSON.</returns>
    internal static List<T> GetObjectsResponse<T>(this JsonNode document)
    {
        return document.AsObject()
            .Where(x => x.Value != null)
            .Select(x => JsonSerializer.Deserialize<T>(x.Value!.ToJsonString())!)
            .ToList();
    }
}
