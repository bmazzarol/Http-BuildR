using System.Net.Http.Headers;

namespace HttpBuildR.Response.Tests;

public static class ResponseHeadersTests
{
    [Fact(DisplayName = "Custom header can be set with a single value")]
    public static void Case1() =>
        new HttpResponseMessage()
            .WithHeader("a", "1")
            .Headers
            .Should()
            .Contain(x => x.Key == "a" && x.Value.Count() == 1 && x.Value.First() == "1");

    [Fact(DisplayName = "Custom header can be set with multiple values")]
    public static void Case2() =>
        new HttpResponseMessage()
            .WithHeader("a", "1", "2", "3")
            .Headers
            .Should()
            .Contain(
                x =>
                    x.Key == "a"
                    && x.Value.Count() == 3
                    && x.Value.Contains("1")
                    && x.Value.Contains("2")
                    && x.Value.Contains("3")
            );

    [Fact(DisplayName = "Cache control header can be set")]
    public static void Case3() =>
        new HttpResponseMessage()
            .WithCacheControl(
                new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(20), NoStore = true }
            )
            .Headers
            .CacheControl
            .Should()
            .BeEquivalentTo(new { MaxAge = TimeSpan.FromSeconds(20), NoStore = true });

    [Fact(DisplayName = "Connection close header can be set")]
    public static void Case4() =>
        new HttpResponseMessage()
            .WithConnectionClose(true)
            .Headers
            .ConnectionClose
            .Should()
            .BeTrue();

    [Fact(DisplayName = "Date header can be set")]
    public static void Case5() =>
        new HttpResponseMessage()
            .WithDate(DateTimeOffset.UtcNow)
            .Headers
            .Date
            .Should()
            .BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(1));

    [Fact(DisplayName = "Transfer-Encoding header can be set")]
    public static void Case6() =>
        new HttpResponseMessage()
            .WithTransferEncodingChunked(true)
            .Headers
            .TransferEncodingChunked
            .Should()
            .BeTrue();

    [Fact(DisplayName = "Age header can be set")]
    public static void Case7() =>
        new HttpResponseMessage()
            .WithAge(TimeSpan.FromDays(1))
            .Headers
            .Age
            .Should()
            .Be(TimeSpan.FromDays(1));

    [Fact(DisplayName = "ETag header can be set")]
    public static void Case8() =>
        new HttpResponseMessage()
            .WithETag(EntityTagHeaderValue.Any)
            .Headers
            .ETag
            .Should()
            .Be(EntityTagHeaderValue.Any);

    [Fact(DisplayName = "Location header can be set")]
    public static void Case9() =>
        new HttpResponseMessage()
            .WithLocation("https://some-host")
            .Headers
            .Location
            .Should()
            .Be(new Uri("https://some-host"));

    [Fact(DisplayName = "RetryAfter header can be set")]
    public static void Case10() =>
        new HttpResponseMessage()
            .WithRetryAfter(TimeSpan.FromMinutes(1))
            .Headers
            .RetryAfter!
            .Delta
            .Should()
            .Be(TimeSpan.FromMinutes(1));

    [Fact(DisplayName = "RetryAfter header can be set using a date time")]
    public static void Case11() =>
        new HttpResponseMessage()
            .WithRetryAfter(DateTimeOffset.UtcNow)
            .Headers
            .RetryAfter!
            .Date
            .Should()
            .BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));

    [Fact(DisplayName = "Headers can be modified using an action")]
    public static void Case19() =>
        new HttpResponseMessage()
            .WithHeaderModifications(h =>
            {
                h.Add("a", "1");
                h.Add("b", "2");
                h.Add("c", "3");
            })
            .Headers
            .Should()
            .BeEquivalentTo(
                new[]
                {
                    KeyValuePair.Create("a", new[] { "1" }),
                    KeyValuePair.Create("b", new[] { "2" }),
                    KeyValuePair.Create("c", new[] { "3" })
                }
            );
}
