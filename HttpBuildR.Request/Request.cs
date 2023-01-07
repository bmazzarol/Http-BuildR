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
    /// <returns>request message</returns>
    public static HttpRequestMessage To(this HttpMethod method, string uri) => new(method, uri);

    /// <summary>
    /// Starts a builder from the given http method
    /// </summary>
    /// <param name="method">http method</param>
    /// <param name="uri">uri</param>
    /// <returns>request message</returns>
    public static HttpRequestMessage To(this HttpMethod method, Uri uri) => new(method, uri);

    private static HttpRequestMessage Modify(
        this HttpRequestMessage request,
        Action<HttpRequestMessage> modifyAction
    )
    {
        modifyAction(request);
        return request;
    }
}
