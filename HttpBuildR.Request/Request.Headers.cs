using System.Net.Http.Headers;

namespace HttpBuildR;

public static partial class Request
{
    /// <summary>
    /// Modifies the request header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="action">header modification action</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithHeaderModifications(
        this HttpRequestMessage request,
        Action<HttpRequestHeaders> action
    ) => request.Modify(x => action(x.Headers));

    /// <summary>
    /// Adds a header to the request
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="name">header name</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithHeader(
        this HttpRequestMessage request,
        string name,
        string? value
    ) => request.WithHeaderModifications(x => x.Add(name, value));

    /// <summary>
    /// Adds a header to the request
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="name">header name</param>
    /// <param name="values">values</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithHeader(
        this HttpRequestMessage request,
        string name,
        params string[] values
    ) => request.WithHeaderModifications(x => x.Add(name, values));

    /// <summary>
    /// Adds a authentication header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="scheme">authentication scheme</param>
    /// <param name="parameter">authentication parameter</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithAuthorization(
        this HttpRequestMessage request,
        string scheme,
        string parameter
    ) =>
        request.WithHeaderModifications(
            x => x.Authorization = new AuthenticationHeaderValue(scheme, parameter)
        );

    /// <summary>
    /// Adds a Proxy-Authorization header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="scheme">authentication scheme</param>
    /// <param name="parameter">authentication parameter</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithProxyAuthorization(
        this HttpRequestMessage request,
        string scheme,
        string parameter
    ) =>
        request.WithHeaderModifications(
            x => x.ProxyAuthorization = new AuthenticationHeaderValue(scheme, parameter)
        );

    /// <summary>
    /// Adds a Bearer authentication token header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="token">bearer token</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithBearerToken(
        this HttpRequestMessage request,
        string token
    ) => request.WithAuthorization("Bearer", token);

    /// <summary>
    /// Adds a Basic authentication token header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="token">basic token</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithBasicToken(
        this HttpRequestMessage request,
        string token
    ) => request.WithAuthorization("Basic", token);

    /// <summary>
    /// Adds a cache control header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">cache control value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithCacheControl(
        this HttpRequestMessage request,
        CacheControlHeaderValue value
    ) => request.WithHeaderModifications(x => x.CacheControl = value);

    /// <summary>
    /// Adds a connection closed header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithConnectionClose(
        this HttpRequestMessage request,
        bool? value
    ) => request.WithHeaderModifications(x => x.ConnectionClose = value);

    /// <summary>
    /// Adds a date header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithDate(
        this HttpRequestMessage request,
        DateTimeOffset? value
    ) => request.WithHeaderModifications(x => x.Date = value);

    /// <summary>
    /// Add to the accept content type header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <param name="quality">quality</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithAccept(
        this HttpRequestMessage request,
        string value,
        double? quality
    ) =>
        request.WithHeaderModifications(
            x =>
                x.Accept.Add(
                    quality.HasValue
                        ? new MediaTypeWithQualityHeaderValue(value, quality.Value)
                        : new MediaTypeWithQualityHeaderValue(value)
                )
        );

    /// <summary>
    /// Adds a If-Modified-Since header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfModifiedSince(
        this HttpRequestMessage request,
        DateTimeOffset? value
    ) => request.WithHeaderModifications(x => x.IfModifiedSince = value);

    /// <summary>
    /// Adds a If-Range header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfRange(
        this HttpRequestMessage request,
        DateTimeOffset value
    ) => request.WithHeaderModifications(x => x.IfRange = new RangeConditionHeaderValue(value));

    /// <summary>
    /// Adds a If-Range header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfRange(
        this HttpRequestMessage request,
        EntityTagHeaderValue value
    ) => request.WithHeaderModifications(x => x.IfRange = new RangeConditionHeaderValue(value));

    /// <summary>
    /// Adds a If-Unmodified-Since header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfUnmodifiedSince(
        this HttpRequestMessage request,
        DateTimeOffset? value
    ) => request.WithHeaderModifications(x => x.IfUnmodifiedSince = value);

    /// <summary>
    /// Adds a Max-Forwards header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithMaxForwards(this HttpRequestMessage request, int? value) =>
        request.WithHeaderModifications(x => x.MaxForwards = value);

    /// <summary>
    /// Adds a Range header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="from">The position at which to start sending data.</param>
    /// <param name="to">The position at which to stop sending data.</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithRange(
        this HttpRequestMessage request,
        long? from,
        long? to
    ) => request.WithHeaderModifications(x => x.Range = new RangeHeaderValue(from, to));

    /// <summary>
    /// Adds a Referer header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithReferrer(this HttpRequestMessage request, string value) =>
        request.WithReferrer(new Uri(value));

    /// <summary>
    /// Adds a Referer header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithReferrer(this HttpRequestMessage request, Uri value) =>
        request.WithHeaderModifications(x => x.Referrer = value);

    /// <summary>
    /// Adds a Transfer-Encoding header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithTransferEncodingChunked(
        this HttpRequestMessage request,
        bool? value
    ) => request.WithHeaderModifications(x => x.TransferEncodingChunked = value);
}
