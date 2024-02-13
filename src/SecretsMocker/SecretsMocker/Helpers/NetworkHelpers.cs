namespace SecretsMocker.Helpers;

public static class NetworkHelpers
{
    public static T GetHeaderValueAs<T>(this HttpContext context, string headerName)
    {
        if (!(context?.Request.Headers.TryGetValue(headerName, out var values) ?? false)) 
            return default;

        var rawValues = values.ToString();

        if (!rawValues.IsNullOrWhitespace())
            return (T)Convert.ChangeType(values.ToString(), typeof(T));

        return default;
    }

    public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
    {
        if (string.IsNullOrWhiteSpace(csvList))
            return nullOrWhitespaceInputReturnsNull ? null : [];

        return csvList
            .TrimEnd(',')
            .Split(',')
            .AsEnumerable<string>()
            .Select(s => s.Trim())
            .ToList();
    }

    public static bool IsNullOrWhitespace(this string s)
    {
        return string.IsNullOrWhiteSpace(s);
    }
}