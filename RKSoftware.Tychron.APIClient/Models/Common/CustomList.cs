using System.Globalization;
using System.Text;

namespace RKSoftware.Tychron.APIClient.Models;

/// <summary>
/// Custom readonly list
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomList<T> : List<T>
{
    /// <summary>
    /// Create empty list
    /// </summary>
    public CustomList(): base() { }

    /// <summary>
    /// Create a list based on collection
    /// </summary>
    /// <param name="collection"></param>
    public CustomList(IEnumerable<T> collection) : base(collection) { }

    /// <summary>
    /// Create a list based on collection with one item
    /// </summary>
    /// <param name="item"></param>
    public CustomList(T item) : base([item]) { }


    /// <summary>
    /// Overriding ToString method
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('[');

        if (Count > 0)
        {
            _ = this.Aggregate(builder,
                                (a, b) => a.Append(CultureInfo.InvariantCulture, $", {b?.ToString()}"),
                                (a) => a.Remove(1, 2).ToString());
        }

        builder.Append(']');
        return builder.ToString();
    }
}
