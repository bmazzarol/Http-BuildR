// ReSharper disable once CheckNamespace
namespace HttpBuildR;

/// <summary>
/// Builders for HttpResponseMessage
/// </summary>
public static partial class Response
{
    /// <summary>
    /// Starts a builder from the given http status code
    /// </summary>
    /// <param name="code">http status code</param>
    /// <param name="reasonPhrase">response reason phrase</param>
    /// <param name="request">request message the generated the response</param>
    /// <param name="version">http version, default is 2.0</param>
    /// <returns>response message</returns>
    [Pure]
    public static HttpResponseMessage Result(
        this HttpStatusCode code,
        string? reasonPhrase = null,
        HttpRequestMessage? request = null,
        Version? version = default
    ) =>
        new(code)
        {
            ReasonPhrase = reasonPhrase,
            RequestMessage = request,
            Version = version ?? new Version(2, 0)
        };

    [Pure]
    private static HttpResponseMessage Clone(this HttpResponseMessage response)
    {
        HttpResponseMessage clone =
            new(response.StatusCode)
            {
                Version = response.Version,
                ReasonPhrase = response.ReasonPhrase,
                Content = response.Content // without async cloning content will not work
            };

        foreach (var kvp in response.Headers)
            clone.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);

        return clone;
    }

    [Pure]
    private static HttpResponseMessage Modify(
        this HttpResponseMessage response,
        Action<HttpResponseMessage> modifyAction
    )
    {
        var clone = response.Clone();
        modifyAction(clone);
        return clone;
    }
}
