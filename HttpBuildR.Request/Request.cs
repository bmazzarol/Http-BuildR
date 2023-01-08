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
    [Pure]
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
    [Pure]
    public static HttpRequestMessage To(
        this HttpMethod method,
        Uri uri,
        Version? version = default
    ) => new(method, uri) { Version = version ?? new Version(2, 0) };

    [Pure]
    private static HttpRequestMessage Clone(this HttpRequestMessage request)
    {
        HttpRequestMessage clone =
            new(request.Method, request.RequestUri)
            {
                Version = request.Version,
                Content = request.Content // without async cloning content will not work
            };

        foreach (var kvp in request.Headers)
            clone.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);

        return clone;
    }

    [Pure]
    private static HttpRequestMessage Modify(
        this HttpRequestMessage request,
        Action<HttpRequestMessage> modifyAction
    )
    {
        var clone = request.Clone();
        modifyAction(clone);
        return clone;
    }
}
