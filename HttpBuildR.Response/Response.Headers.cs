using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

/// <summary>
/// Extension methods for modifying headers
/// </summary>
public static partial class Response
{
    /// <summary>
    /// Modifies the response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="action">header modification action</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithHeaderModifications(
        this HttpResponseMessage response,
        Action<HttpResponseHeaders> action
    ) => response.Modify(x => action(x.Headers));

    /// <summary>
    /// Adds a header to the response
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="name">header name</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithHeader(
        this HttpResponseMessage response,
        string name,
        string? value
    ) => response.WithHeaderModifications(x => x.Add(name, value));

    /// <summary>
    /// Adds a header to the response
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="name">header name</param>
    /// <param name="values">values</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithHeader(
        this HttpResponseMessage response,
        string name,
        params string[] values
    ) => response.WithHeaderModifications(x => x.Add(name, values));

    /// <summary>
    /// Adds a age response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithAge(this HttpResponseMessage response, TimeSpan? value) =>
        response.WithHeaderModifications(x => x.Age = value);

    /// <summary>
    /// Adds a ETag response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithETag(
        this HttpResponseMessage response,
        EntityTagHeaderValue value
    ) => response.WithHeaderModifications(x => x.ETag = value);

    /// <summary>
    /// Adds a Location response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithLocation(this HttpResponseMessage response, Uri value) =>
        response.WithHeaderModifications(x => x.Location = value);

    /// <summary>
    /// Adds a Location response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithLocation(
        this HttpResponseMessage response,
        string value
    ) => response.WithLocation(new Uri(value));

    /// <summary>
    /// Adds a RetryAfter response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithRetryAfter(
        this HttpResponseMessage response,
        DateTimeOffset value
    ) => response.WithHeaderModifications(x => x.RetryAfter = new RetryConditionHeaderValue(value));

    /// <summary>
    /// Adds a RetryAfter response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithRetryAfter(
        this HttpResponseMessage response,
        TimeSpan value
    ) => response.WithHeaderModifications(x => x.RetryAfter = new RetryConditionHeaderValue(value));

    /// <summary>
    /// Adds a cache control response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">cache control value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithCacheControl(
        this HttpResponseMessage response,
        CacheControlHeaderValue value
    ) => response.WithHeaderModifications(x => x.CacheControl = value);

    /// <summary>
    /// Adds a connection closed response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithConnectionClose(
        this HttpResponseMessage response,
        bool? value
    ) => response.WithHeaderModifications(x => x.ConnectionClose = value);

    /// <summary>
    /// Adds a date response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithDate(
        this HttpResponseMessage response,
        DateTimeOffset? value
    ) => response.WithHeaderModifications(x => x.Date = value);

    /// <summary>
    /// Adds a Transfer-Encoding response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithTransferEncodingChunked(
        this HttpResponseMessage response,
        bool? value
    ) => response.WithHeaderModifications(x => x.TransferEncodingChunked = value);
}
