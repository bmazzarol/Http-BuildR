using System.Net.Http.Headers;
using Docfx.ResultSnippets;

namespace HttpBuildR.Request.Tests.Examples;

public class RequestHeadersTests
{
    private static string SaveHeadersAsMdTable(HttpRequestMessage request)
    {
        return request
            .Headers.Select(h => new { Name = h.Key, Value = string.Join(", ", h.Value) })
            .ToTableResult();
    }

    [Fact]
    public void TestWithHeaderMethod()
    {
        #region WithHeaderMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithHeader("headerName", "headerValue");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithAuthorizationMethod()
    {
        #region WithAuthorizationMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithAuthorization("scheme", "parameter");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithProxyAuthorizationMethod()
    {
        #region WithProxyAuthorizationMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithProxyAuthorization("scheme", "parameter");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithBearerTokenMethod()
    {
        #region WithBearerTokenMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithBearerToken("token");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithBasicTokenMethod()
    {
        #region WithBasicTokenMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithBasicToken("token");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithCacheControlMethod()
    {
        #region WithCacheControlMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithCacheControl(new CacheControlHeaderValue { NoCache = true });

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithConnectionCloseMethod()
    {
        #region WithConnectionCloseMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithConnectionClose(true);

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithDateMethod()
    {
        #region WithDateMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithDate(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero));

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithAcceptMethod()
    {
        #region WithAcceptMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithAccept("application/json");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithIfModifiedSinceMethod()
    {
        #region WithIfModifiedSinceMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithIfModifiedSince(
            new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
        );

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithIfRangeMethod()
    {
        #region WithIfRangeMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithIfRange(new EntityTagHeaderValue("\"tag\""));

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithIfUnmodifiedSinceMethod()
    {
        #region WithIfUnmodifiedSinceMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithIfUnmodifiedSince(
            new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
        );

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithMaxForwardsMethod()
    {
        #region WithMaxForwardsMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithMaxForwards(10);

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithRangeMethod()
    {
        #region WithRangeMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithRange(0, 500);

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithReferrerMethod()
    {
        #region WithReferrerMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithReferrer("https://example.com");

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }

    [Fact]
    public void TestWithTransferEncodingChunkedMethod()
    {
        #region WithTransferEncodingChunkedMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithTransferEncodingChunked(true);

        #endregion

        request.Should().NotBeNull();
        SaveHeadersAsMdTable(request).SaveResults();
    }
}
