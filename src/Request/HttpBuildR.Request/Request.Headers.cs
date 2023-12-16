using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
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
    )
    {
        action(request.Headers);
        return request;
    }

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
    )
    {
        request.Headers.Add(name, value);
        return request;
    }

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
    )
    {
        request.Headers.Add(name, values);
        return request;
    }

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
    )
    {
        request.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        return request;
    }

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
    )
    {
        request.Headers.ProxyAuthorization = new AuthenticationHeaderValue(scheme, parameter);
        return request;
    }

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
    )
    {
        request.Headers.CacheControl = value;
        return request;
    }

    /// <summary>
    /// Adds a connection closed header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithConnectionClose(
        this HttpRequestMessage request,
        bool? value
    )
    {
        request.Headers.ConnectionClose = value;
        return request;
    }

    /// <summary>
    /// Adds a date header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithDate(
        this HttpRequestMessage request,
        DateTimeOffset? value
    )
    {
        request.Headers.Date = value;
        return request;
    }

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
        double? quality = default
    )
    {
        request
            .Headers
            .Accept
            .Add(
                quality.HasValue
                    ? new MediaTypeWithQualityHeaderValue(value, quality.Value)
                    : new MediaTypeWithQualityHeaderValue(value)
            );
        return request;
    }

    /// <summary>
    /// Adds a If-Modified-Since header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfModifiedSince(
        this HttpRequestMessage request,
        DateTimeOffset? value
    )
    {
        request.Headers.IfModifiedSince = value;
        return request;
    }

    /// <summary>
    /// Adds a If-Range header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfRange(
        this HttpRequestMessage request,
        DateTimeOffset value
    )
    {
        request.Headers.IfRange = new RangeConditionHeaderValue(value);
        return request;
    }

    /// <summary>
    /// Adds a If-Range header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfRange(
        this HttpRequestMessage request,
        EntityTagHeaderValue value
    )
    {
        request.Headers.IfRange = new RangeConditionHeaderValue(value);
        return request;
    }

    /// <summary>
    /// Adds a If-Unmodified-Since header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithIfUnmodifiedSince(
        this HttpRequestMessage request,
        DateTimeOffset? value
    )
    {
        request.Headers.IfUnmodifiedSince = value;
        return request;
    }

    /// <summary>
    /// Adds a Max-Forwards header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithMaxForwards(this HttpRequestMessage request, int? value)
    {
        request.Headers.MaxForwards = value;
        return request;
    }

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
    )
    {
        request.Headers.Range = new RangeHeaderValue(from, to);
        return request;
    }

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
    public static HttpRequestMessage WithReferrer(this HttpRequestMessage request, Uri value)
    {
        request.Headers.Referrer = value;
        return request;
    }

    /// <summary>
    /// Adds a Transfer-Encoding header
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="value">value</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithTransferEncodingChunked(
        this HttpRequestMessage request,
        bool? value
    )
    {
        request.Headers.TransferEncodingChunked = value;
        return request;
    }
}
