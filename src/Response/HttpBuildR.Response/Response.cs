// ReSharper disable once CheckNamespace
namespace HttpBuildR;

/// <summary>
/// Builders for <see cref="HttpResponseMessage"/>
/// </summary>
public static partial class Response
{
    /// <summary>
    /// Starts the creation of a new <see cref="HttpResponseMessage"/> from the
    /// given <see cref="HttpStatusCode"/>
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

    /// <summary>
    /// Clones the <see cref="HttpResponseMessage"/> returning a new <see cref="HttpResponseMessage"/>
    /// </summary>
    /// <param name="response">existing <see cref="HttpResponseMessage"/></param>
    /// <returns>clone of the <see cref="HttpResponseMessage"/></returns>
    [Pure]
    public static async ValueTask<HttpResponseMessage> Clone(this HttpResponseMessage response)
    {
        HttpResponseMessage clone =
            new(response.StatusCode)
            {
                Version = response.Version,
                ReasonPhrase = response.ReasonPhrase
            };

        var ms = new MemoryStream();
        if (response.Content != null)
        {
            await response.Content.CopyToAsync(ms);
            ms.Position = 0;
            clone.Content = new StreamContent(ms);
        }

        foreach (var kvp in response.Headers)
            clone.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);

        return clone;
    }
}
