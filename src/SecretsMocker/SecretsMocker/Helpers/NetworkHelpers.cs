namespace SecretsMocker.Helpers;

public static class NetworkHelpers
{
    //public static string GetRequestIp(this HttpContext context, bool tryUseXForwardHeader = true)
    //{
    //    string ip = null;

    //    if (tryUseXForwardHeader)
    //        ip = context.GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();
    //        //ip = context.GetHeaderValueAs<string>("X-Forwarded-For");

    //    if (ip.IsNullOrWhitespace() && context?.Connection.RemoteIpAddress != null)
    //        ip = context.Connection.RemoteIpAddress.ToString();

    //    if (ip.IsNullOrWhitespace())
    //        ip = context.GetHeaderValueAs<string>("REMOTE_ADDR");

    //    if (ip.IsNullOrWhitespace())
    //        ip = "Unable to determine caller's IP.";

    //    return ip;
    //}

    public static T GetHeaderValueAs<T>(this HttpContext context, string headerName)
    {
        if (context?.Request.Headers.TryGetValue(headerName, out var values) ?? false)
        {
            var rawValues = values.ToString();

            if (!rawValues.IsNullOrWhitespace())
                return (T)Convert.ChangeType(values.ToString(), typeof(T));
        }

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