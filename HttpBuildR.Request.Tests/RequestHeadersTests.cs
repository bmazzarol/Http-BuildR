using System.Net.Http.Headers;

namespace HttpBuildR.Request.Tests;

public static class RequestHeadersTests
{
    [Fact(DisplayName = "Base authentication can be set")]
    public static void Case1() =>
        new HttpRequestMessage()
            .WithBasicToken("abcd")
            .Headers.Authorization.Should()
            .BeEquivalentTo(new { Scheme = "Basic", Parameter = "abcd" });

    [Fact(DisplayName = "Bearer authentication can be set")]
    public static void Case2() =>
        new HttpRequestMessage()
            .WithBearerToken("abcde")
            .Headers.Authorization.Should()
            .BeEquivalentTo(new { Scheme = "Bearer", Parameter = "abcde" });

    [Fact(DisplayName = "Custom header can be set with a single value")]
    public static void Case3() =>
        new HttpRequestMessage()
            .WithHeader("a", "1")
            .Headers.Should()
            .Contain(x => x.Key == "a" && x.Value.Count() == 1 && x.Value.First() == "1");

    [Fact(DisplayName = "Custom header can be set with multiple values")]
    public static void Case4() =>
        new HttpRequestMessage()
            .WithHeader("a", "1", "2", "3")
            .Headers.Should()
            .Contain(
                x =>
                    x.Key == "a"
                    && x.Value.Count() == 3
                    && x.Value.Contains("1")
                    && x.Value.Contains("2")
                    && x.Value.Contains("3")
            );

    [Fact(DisplayName = "Proxy authorization header can be set")]
    public static void Case5() =>
        new HttpRequestMessage()
            .WithProxyAuthorization("Test", "abcdef")
            .Headers.ProxyAuthorization.Should()
            .BeEquivalentTo(new { Scheme = "Test", Parameter = "abcdef" });

    [Fact(DisplayName = "Cache control header can be set")]
    public static void Case6() =>
        new HttpRequestMessage()
            .WithCacheControl(
                new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(20), NoStore = true }
            )
            .Headers.CacheControl.Should()
            .BeEquivalentTo(new { MaxAge = TimeSpan.FromSeconds(20), NoStore = true });

    [Fact(DisplayName = "Connection close header can be set")]
    public static void Case7() =>
        new HttpRequestMessage()
            .WithConnectionClose(true)
            .Headers.ConnectionClose.Should()
            .BeTrue();

    [Fact(DisplayName = "Date header can be set")]
    public static void Case8() =>
        new HttpRequestMessage()
            .WithDate(DateTimeOffset.UtcNow)
            .Headers.Date.Should()
            .BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(1));

    [Fact(DisplayName = "Accept headers can be set")]
    public static void Case9() =>
        new HttpRequestMessage()
            .WithAccept("text/json", 0.20)
            .Headers.Accept.Should()
            .Contain(x => x.MediaType == "text/json" && x.Quality == 0.20);

    [Fact(DisplayName = "Accept headers can be set without quality")]
    public static void Case10() =>
        new HttpRequestMessage()
            .WithAccept("text/json2")
            .Headers.Accept.Should()
            .Contain(x => x.MediaType == "text/json2" && !x.Quality.HasValue);

    [Fact(DisplayName = "If-Modified-Since header can be set")]
    public static void Case11() =>
        new HttpRequestMessage()
            .WithIfModifiedSince(DateTimeOffset.UtcNow)
            .Headers.IfModifiedSince.Should()
            .BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));

    [Fact(DisplayName = "Range header can be set")]
    public static void Case12() =>
        new HttpRequestMessage()
            .WithRange(20, 50)
            .Headers.Range.Should()
            .BeEquivalentTo(new RangeHeaderValue(20, 50));

    [Fact(DisplayName = "If-Range header can be set using data time")]
    public static void Case13() =>
        new HttpRequestMessage()
            .WithIfRange(DateTimeOffset.Now)
            .Headers.IfRange?.Date.Should()
            .BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(1));

    [Fact(DisplayName = "If-Range header can be set using e-tag")]
    public static void Case14() =>
        new HttpRequestMessage()
            .WithIfRange(new EntityTagHeaderValue("\"a\""))
            .Headers.IfRange?.EntityTag.Should()
            .BeEquivalentTo(new EntityTagHeaderValue("\"a\""));

    [Fact(DisplayName = "If-Unmodified-Since header can be set")]
    public static void Case15() =>
        new HttpRequestMessage()
            .WithIfUnmodifiedSince(DateTimeOffset.UtcNow)
            .Headers.IfUnmodifiedSince.Should()
            .BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));

    [Fact(DisplayName = "Max-Forwards header can be set")]
    public static void Case16() =>
        new HttpRequestMessage().WithMaxForwards(3).Headers.MaxForwards.Should().Be(3);

    [Fact(DisplayName = "Referrer header can be set")]
    public static void Case17() =>
        new HttpRequestMessage()
            .WithReferrer("https://some-domain")
            .Headers.Referrer.Should()
            .Be(new Uri("https://some-domain"));

    [Fact(DisplayName = "Transfer-Encoding header can be set")]
    public static void Case18() =>
        new HttpRequestMessage()
            .WithTransferEncodingChunked(true)
            .Headers.TransferEncodingChunked.Should()
            .BeTrue();
}
