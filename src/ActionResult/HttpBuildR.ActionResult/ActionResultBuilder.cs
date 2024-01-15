using System.Diagnostics.Contracts;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

using Resp = System.Net.HttpStatusCode;

/// <summary>
/// ActionResult builders
/// </summary>
public static class ActionResultBuilder
{
    private sealed class HttpResponseMessageAction(HttpResponseMessage response, Cookie[] cookies)
        : Microsoft.AspNetCore.Mvc.ActionResult
    {
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var resp = context.HttpContext.Response;
            resp.StatusCode = (int)response.StatusCode;
            foreach (var kvp in response.Headers.Concat(response.Content.Headers))
            {
                resp.Headers[kvp.Key] = new StringValues(kvp.Value.ToArray());
            }

            resp.Body = await response.Content.ReadAsStreamAsync(
                context.HttpContext.RequestAborted
            );
            resp.ContentType = response.Content.Headers.ContentType?.ToString() ?? string.Empty;
            resp.ContentLength = response.Content.Headers.ContentLength;

            for (var i = 0; i < cookies.Length; i++)
            {
                resp.Cookies.Append(cookies[i].Key, cookies[i].Value, cookies[i].Options);
            }
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
    public static ActionResult<T> Ok<T>(T result, params Cookie[] cookies)
        where T : notnull =>
        cookies.Length == 0
            ? new(result)
            : Resp.OK.Result().WithJsonContent(result).ToActionResult<T>(cookies);

    /// <summary>
    /// Converts a HttpResponseMessage to an ActionResult of T
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="cookies">optional cookies</param>
    /// <typeparam name="T">some T</typeparam>
    /// <returns>action result of T</returns>
    [Pure]
    public static ActionResult<T> ToActionResult<T>(
        this HttpResponseMessage response,
        params Cookie[] cookies
    ) => new HttpResponseMessageAction(response, cookies);

    /// <summary>
    /// Adds problem details as a json response body
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="details">problem details</param>
    /// <param name="options">optional <see cref="JsonSerializerOptions"/></param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithProblemDetails(
        this HttpResponseMessage response,
        ProblemDetails details,
        JsonSerializerOptions? options = null
    ) => response.WithJsonContent(details, options);

    /// <summary>
    /// Adds problem details as a json response body
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="type">A URI reference [RFC3986] that identifies the problem type</param>
    /// <param name="title">A short, human-readable summary of the problem type</param>
    /// <param name="detail">A human-readable explanation specific to this occurrence of the problem</param>
    /// <param name="instance">A URI reference that identifies the specific occurrence of the problem</param>
    /// <param name="options">optional <see cref="JsonSerializerOptions"/></param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithProblemDetails(
        this HttpResponseMessage response,
        string? type,
        string? title,
        string? detail,
        string? instance,
        JsonSerializerOptions? options = null
    ) =>
        response.WithProblemDetails(
            new ProblemDetails
            {
                Type = type,
                Title = title,
                Detail = detail,
                Status = (int)response.StatusCode,
                Instance = instance
            },
            options
        );
}
