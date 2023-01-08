// ReSharper disable once CheckNamespace

namespace HttpBuildR;

/// <summary>
/// Builders for HttpRequestMessage
/// </summary>
public static partial class Request
{
    /// <summary>
    /// Starts a builder from the given http method
    /// </summary>
    /// <param name="method">http method</param>
    /// <param name="uri">uri</param>
    /// <param name="version">http version, default is 2.0</param>
    /// <returns>request message</returns>
    public static HttpRequestMessage To(
        this HttpMethod method,
        string uri,
        Version? version = default
    ) => new(method, uri) { Version = version ?? new Version(2, 0) };

    /// <summary>
    /// Starts a builder from the given http method
    /// </summary>
    /// <param name="method">http method</param>
    /// <param name="uri">uri</param>
    /// <param name="version">http version, default is 2.0</param>
    /// <returns>request message</returns>
    public static HttpRequestMessage To(
        this HttpMethod method,
        Uri uri,
        Version? version = default
    ) => new(method, uri) { Version = version ?? new Version(2, 0) };

    private static HttpRequestMessage Modify(
        this HttpRequestMessage request,
        Action<HttpRequestMessage> modifyAction
    )
    {
        modifyAction(request);
        return request;
    }
}
