using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

using Resp = System.Net.HttpStatusCode;

/// <summary>
/// ActionResult builders
/// </summary>
public static class ActionResult
{
    private sealed class HttpResponseMessageAction : Microsoft.AspNetCore.Mvc.ActionResult
    {
        private readonly HttpResponseMessage _response;
        private readonly IEnumerable<Cookie> _cookies;

        public HttpResponseMessageAction(HttpResponseMessage response, IEnumerable<Cookie> cookies)
        {
            _response = response;
            _cookies = cookies;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var resp = context.HttpContext.Response;
            resp.StatusCode = (int)_response.StatusCode;
            foreach (var kvp in _response.Headers.Concat(_response.Content.Headers))
                resp.Headers.Add(kvp.Key, new StringValues(kvp.Value.ToArray()));
            resp.Body = await _response.Content.ReadAsStreamAsync(
                context.HttpContext.RequestAborted
            );
            resp.ContentType = _response.Content.Headers.ContentType?.ToString() ?? string.Empty;
            resp.ContentLength = _response.Content.Headers.ContentLength;
            foreach (var cookie in _cookies)
                resp.Cookies.Append(cookie.Key, cookie.Value, cookie.Options);
        }
    }

    /// <summary>
    /// Converts T to an ActionResult of T
    /// </summary>
    /// <param name="result">result</param>
    /// <param name="cookies">optional cookies</param>
    /// <typeparam name="T">some T</typeparam>
    /// <returns>action result of T</returns>
    [Pure]
    public static ActionResult<T> AsOk<T>(this T result, params Cookie[] cookies)
        where T : notnull =>
        cookies.Length == 0
            ? new(result)
            : Resp.OK.Result().WithJsonContent(result).AsAction<T>(cookies);

    /// <summary>
    /// Converts a HttpResponseMessage to an ActionResult of T
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="cookies">optional cookies</param>
    /// <typeparam name="T">some T</typeparam>
    /// <returns>action result of T</returns>
    [Pure]
    public static ActionResult<T> AsAction<T>(
        this HttpResponseMessage response,
        params Cookie[] cookies
    ) => new HttpResponseMessageAction(response, cookies);

    /// <summary>
    /// Adds problem details as a json response body
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="details">problem details</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithProblemDetails(
        this HttpResponseMessage response,
        ProblemDetails details
    ) => response.WithJsonContent(details);

    /// <summary>
    /// Adds problem details as a json response body
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="type">A URI reference [RFC3986] that identifies the problem type</param>
    /// <param name="title">A short, human-readable summary of the problem type</param>
    /// <param name="detail">A human-readable explanation specific to this occurrence of the problem</param>
    /// <param name="instance">A URI reference that identifies the specific occurrence of the problem</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithProblemDetails(
        this HttpResponseMessage response,
        string? type,
        string? title,
        string? detail,
        string? instance
    ) =>
        response.WithProblemDetails(
            new ProblemDetails
            {
                Type = type,
                Title = title,
                Detail = detail,
                Status = (int)response.StatusCode,
                Instance = instance
            }
        );
}
