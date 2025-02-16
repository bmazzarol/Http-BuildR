// ReSharper disable once CheckNamespace
namespace HttpBuildR;

/// <summary>
/// Builders for <see cref="HttpRequestMessage"/>
/// </summary>
public static partial class Request
{
    private static readonly Version V2 = new(2, 0);

    /// <summary>
    /// Starts the creation of a new <see cref="HttpRequestMessage"/> from the
    /// given <see cref="HttpMethod"/> and <see cref="Uri"/>
    /// </summary>
    /// <param name="method">http method</param>
    /// <param name="uri">uri</param>
    /// <param name="version">http version, default is 2.0</param>
    /// <returns>request message</returns>
    public static HttpRequestMessage To(
        this HttpMethod method,
        string uri,
        Version? version = default
    ) => new(method, uri) { Version = version ?? V2 };

    /// <summary>
    /// Starts the creation of a new <see cref="HttpRequestMessage"/> from the
    /// given <see cref="HttpMethod"/> and <see cref="Uri"/>
    /// </summary>
    /// <param name="method">http method</param>
    /// <param name="uri">uri</param>
    /// <param name="version">http version, default is 2.0</param>
    /// <returns>request message</returns>
    public static HttpRequestMessage To(
        this HttpMethod method,
        Uri uri,
        Version? version = default
    ) => new(method, uri) { Version = version ?? V2 };

    /// <summary>
    /// Clones the <see cref="HttpRequestMessage"/> returning a new <see cref="HttpRequestMessage"/>
    /// </summary>
    /// <param name="request">existing <see cref="HttpRequestMessage"/></param>
    /// <returns>clone of the <see cref="HttpRequestMessage"/></returns>
    public static async ValueTask<HttpRequestMessage> Clone(this HttpRequestMessage request)
    {
        HttpRequestMessage clone = new(request.Method, request.RequestUri)
        {
            Version = request.Version,
        };

        var ms = new MemoryStream();
        if (request.Content != null)
        {
            await request.Content.CopyToAsync(ms);
            ms.Position = 0;
            clone.Content = new StreamContent(ms);
        }

        foreach (var kvp in request.Headers)
            clone.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);

        return clone;
    }
}
