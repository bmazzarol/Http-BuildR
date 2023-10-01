using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

public static partial class Response
{
    /// <summary>
    /// Modifies the response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="action">header modification action</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithHeaderModifications(
        this HttpResponseMessage response,
        Action<HttpResponseHeaders> action
    )
    {
        action(response.Headers);
        return response;
    }

    /// <summary>
    /// Adds a header to the response
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="name">header name</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithHeader(
        this HttpResponseMessage response,
        string name,
        string? value
    )
    {
        response.Headers.Add(name, value);
        return response;
    }

    /// <summary>
    /// Adds a header to the response
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="name">header name</param>
    /// <param name="values">values</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithHeader(
        this HttpResponseMessage response,
        string name,
        params string[] values
    )
    {
        response.Headers.Add(name, values);
        return response;
    }

    /// <summary>
    /// Adds a age response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithAge(this HttpResponseMessage response, TimeSpan? value)
    {
        response.Headers.Age = value;
        return response;
    }

    /// <summary>
    /// Adds a ETag response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithETag(
        this HttpResponseMessage response,
        EntityTagHeaderValue value
    )
    {
        response.Headers.ETag = value;
        return response;
    }

    /// <summary>
    /// Adds a Location response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithLocation(this HttpResponseMessage response, Uri value)
    {
        response.Headers.Location = value;
        return response;
    }

    /// <summary>
    /// Adds a Location response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
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
    public static HttpResponseMessage WithRetryAfter(
        this HttpResponseMessage response,
        DateTimeOffset value
    )
    {
        response.Headers.RetryAfter = new RetryConditionHeaderValue(value);
        return response;
    }

    /// <summary>
    /// Adds a RetryAfter response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithRetryAfter(
        this HttpResponseMessage response,
        TimeSpan value
    )
    {
        response.Headers.RetryAfter = new RetryConditionHeaderValue(value);
        return response;
    }

    /// <summary>
    /// Adds a cache control response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">cache control value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithCacheControl(
        this HttpResponseMessage response,
        CacheControlHeaderValue value
    )
    {
        response.Headers.CacheControl = value;
        return response;
    }

    /// <summary>
    /// Adds a connection closed response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithConnectionClose(
        this HttpResponseMessage response,
        bool? value
    )
    {
        response.Headers.ConnectionClose = value;
        return response;
    }

    /// <summary>
    /// Adds a date response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithDate(
        this HttpResponseMessage response,
        DateTimeOffset? value
    )
    {
        response.Headers.Date = value;
        return response;
    }

    /// <summary>
    /// Adds a Transfer-Encoding response header
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="value">value</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithTransferEncodingChunked(
        this HttpResponseMessage response,
        bool? value
    )
    {
        response.Headers.TransferEncodingChunked = value;
        return response;
    }
}
