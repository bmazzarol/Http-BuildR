using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace HttpBuildR.Tests;

[SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded")]
public sealed class RequestHeadersTests
{
    [Fact(DisplayName = "Base authentication can be set")]
    public void Case1()
    {
        var req = new HttpRequestMessage().WithBasicToken("abcd");
        Assert.Equal("Basic", req.Headers.Authorization!.Scheme);
        Assert.Equal("abcd", req.Headers.Authorization!.Parameter);
    }

    [Fact(DisplayName = "Bearer authentication can be set")]
    public void Case2()
    {
        var req = new HttpRequestMessage().WithBearerToken("abcde");
        Assert.Equal("Bearer", req.Headers.Authorization!.Scheme);
        Assert.Equal("abcde", req.Headers.Authorization!.Parameter);
    }

    [Fact(DisplayName = "Custom header can be set with a single value")]
    public void Case3()
    {
        var req = new HttpRequestMessage().WithHeader("a", "1");
        Assert.Single(req.Headers);
        Assert.Equal("1", req.Headers.GetValues("a").First());
    }

    [Fact(DisplayName = "Custom header can be set with multiple values")]
    public void Case4()
    {
        var req = new HttpRequestMessage().WithHeader("a", "1", "2", "3");
        Assert.Single(req.Headers);
        Assert.Equal(3, req.Headers.GetValues("a").Count());
        Assert.Contains("1", req.Headers.GetValues("a"));
        Assert.Contains("2", req.Headers.GetValues("a"));
        Assert.Contains("3", req.Headers.GetValues("a"));
    }

    [Fact(DisplayName = "Proxy authorization header can be set")]
    public void Case5()
    {
        var req = new HttpRequestMessage().WithProxyAuthorization("Test", "abcdef");
        Assert.Equal("Test", req.Headers.ProxyAuthorization!.Scheme);
        Assert.Equal("abcdef", req.Headers.ProxyAuthorization!.Parameter);
    }

    [Fact(DisplayName = "Cache control header can be set")]
    public void Case6()
    {
        var req = new HttpRequestMessage().WithCacheControl(
            new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(20), NoStore = true }
        );
        Assert.Equal(TimeSpan.FromSeconds(20), req.Headers.CacheControl!.MaxAge);
        Assert.True(req.Headers.CacheControl.NoStore);
    }

    [Fact(DisplayName = "Connection close header can be set")]
    public void Case7()
    {
        var req = new HttpRequestMessage().WithConnectionClose(value: true);
        Assert.True(req.Headers.ConnectionClose);
    }

    [Fact(DisplayName = "Date header can be set")]
    public void Case8()
    {
        var req = new HttpRequestMessage().WithDate(DateTimeOffset.UtcNow);
        Assert.NotNull(req.Headers.Date);
        Assert.Equal(DateTimeOffset.UtcNow, req.Headers.Date.Value, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = "Accept headers can be set")]
    public void Case9()
    {
        var req = new HttpRequestMessage().WithAccept("text/json", 0.20);
        Assert.Contains(new MediaTypeWithQualityHeaderValue("text/json", 0.20), req.Headers.Accept);
    }

    [Fact(DisplayName = "Accept headers can be set without quality")]
    public void Case10()
    {
        var req = new HttpRequestMessage().WithAccept("text/json2");
        Assert.Contains(new MediaTypeWithQualityHeaderValue("text/json2"), req.Headers.Accept);
    }

    [Fact(DisplayName = "If-Modified-Since header can be set")]
    public void Case11()
    {
        var req = new HttpRequestMessage().WithIfModifiedSince(DateTimeOffset.UtcNow);
        Assert.NotNull(req.Headers.IfModifiedSince);
        Assert.Equal(
            DateTimeOffset.UtcNow,
            req.Headers.IfModifiedSince.Value,
            TimeSpan.FromSeconds(1)
        );
    }

    [Fact(DisplayName = "Range header can be set")]
    public void Case12()
    {
        var req = new HttpRequestMessage().WithRange(20, 50);
        Assert.NotNull(req.Headers.Range);
        Assert.Equal(new RangeHeaderValue(20, 50), req.Headers.Range);
    }

    [Fact(DisplayName = "If-Range header can be set using data time")]
    public void Case13()
    {
        var req = new HttpRequestMessage().WithIfRange(DateTimeOffset.Now);
        Assert.NotNull(req.Headers.IfRange);
        Assert.NotNull(req.Headers.IfRange.Date);
        Assert.Equal(DateTimeOffset.Now, req.Headers.IfRange.Date.Value, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = "If-Range header can be set using e-tag")]
    public void Case14()
    {
        var req = new HttpRequestMessage().WithIfRange(new EntityTagHeaderValue("\"a\""));
        Assert.NotNull(req.Headers.IfRange);
        Assert.NotNull(req.Headers.IfRange.EntityTag);
        Assert.Equal(new EntityTagHeaderValue("\"a\""), req.Headers.IfRange.EntityTag);
    }

    [Fact(DisplayName = "If-Unmodified-Since header can be set")]
    public void Case15()
    {
        var req = new HttpRequestMessage().WithIfUnmodifiedSince(DateTimeOffset.UtcNow);
        Assert.NotNull(req.Headers.IfUnmodifiedSince);
        Assert.Equal(
            DateTimeOffset.UtcNow,
            req.Headers.IfUnmodifiedSince.Value,
            TimeSpan.FromSeconds(1)
        );
    }

    [Fact(DisplayName = "Max-Forwards header can be set")]
    public void Case16()
    {
        var req = new HttpRequestMessage().WithMaxForwards(3);
        Assert.Equal(3, req.Headers.MaxForwards);
    }

    [Fact(DisplayName = "Referrer header can be set")]
    public void Case17()
    {
        var req = new HttpRequestMessage().WithReferrer("https://some-domain");
        Assert.NotNull(req.Headers.Referrer);
        Assert.Equal(new Uri("https://some-domain"), req.Headers.Referrer);
    }

    [Fact(DisplayName = "Transfer-Encoding header can be set")]
    public void Case18()
    {
        var req = new HttpRequestMessage().WithTransferEncodingChunked(true);
        Assert.True(req.Headers.TransferEncodingChunked);
    }

    [Fact(DisplayName = "Headers can be modified using an action")]
    public void Case19()
    {
        var req = new HttpRequestMessage().WithHeaderModifications(h =>
        {
            h.Add("a", "1");
            h.Add("b", "2");
            h.Add("c", "3");
        });
        Assert.Equal(3, req.Headers.Count());
        Assert.Equal("1", req.Headers.GetValues("a").First());
        Assert.Equal("2", req.Headers.GetValues("b").First());
        Assert.Equal("3", req.Headers.GetValues("c").First());
    }
}
