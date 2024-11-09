namespace API.Extensions;

public static class RelateUri
{
    /// <summary>
    /// Role: convert query string to dictionary key-value query
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static Dictionary<string, string> FormatQueries(this string queryString)
    {
        Dictionary<string, string> pairs = new();
        if (string.IsNullOrEmpty(queryString))
            return pairs;

        string key, value;
        foreach (var query in queryString.TrimStart('?').Split('&'))
        {
            var items = query.Split('=');
            key = items.Count() >= 1 ? items[0] : string.Empty;
            value = items.Count() >= 2 ? items[1] : string.Empty;
            if (!string.IsNullOrEmpty(key))
                pairs.TryAdd(key, value);
        }

        return pairs;
    }
}