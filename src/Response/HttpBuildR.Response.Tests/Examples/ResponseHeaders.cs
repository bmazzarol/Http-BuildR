using System.Net.Http.Headers;
using Docfx.ResultSnippets;

namespace HttpBuildR.Response.Tests.Examples;

public class ResponseHeadersTests
{
    private static string SaveHeadersAsMdTable(HttpResponseMessage request)
    {
        return request
            .Headers.Select(h => new { Name = h.Key, Value = string.Join(", ", h.Value) })
            .ToTableResult();
    }

    [Fact]
    public void TestWithHeaderMethod()
    {
        #region WithHeaderMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithHeader("headerName", "headerValue");

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithAgeMethod()
    {
        #region WithAgeMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithAge(TimeSpan.FromMinutes(5));

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithETagMethod()
    {
        #region WithETagMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithETag(new EntityTagHeaderValue("\"tag\""));

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithLocationMethod()
    {
        #region WithLocationMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithLocation(new Uri("https://example.com"));

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithRetryAfterMethod()
    {
        #region WithRetryAfterMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithRetryAfter(
            new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero).AddHours(1)
        );

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithCacheControlMethod()
    {
        #region WithCacheControlMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithCacheControl(
            new CacheControlHeaderValue { MaxAge = TimeSpan.FromHours(2) }
        );

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithConnectionCloseMethod()
    {
        #region WithConnectionCloseMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithConnectionClose(true);

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithDateMethod()
    {
        #region WithDateMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithDate(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero));

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }

    [Fact]
    public void TestWithTransferEncodingChunkedMethod()
    {
        #region WithTransferEncodingChunkedMethod

        HttpResponseMessage response = new HttpResponseMessage();
        response = response.WithTransferEncodingChunked(true);

        #endregion

        response.Should().NotBeNull();
        SaveHeadersAsMdTable(response).SaveResults();
    }
}
