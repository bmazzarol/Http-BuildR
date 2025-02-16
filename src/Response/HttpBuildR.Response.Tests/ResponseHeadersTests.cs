using System.Net.Http.Headers;

namespace HttpBuildR.Response.Tests;

public sealed class ResponseHeadersTests
{
    [Fact(DisplayName = "Custom header can be set with a single value")]
    public void Case1()
    {
        var resp = new HttpResponseMessage().WithHeader("a", "1");
        Assert.Single(resp.Headers);
        Assert.Equal("1", resp.Headers.GetValues("a").First());
    }

    [Fact(DisplayName = "Custom header can be set with multiple values")]
    public void Case2()
    {
        var resp = new HttpResponseMessage().WithHeader("a", "1", "2", "3");
        Assert.Single(resp.Headers);
        Assert.Equal(3, resp.Headers.GetValues("a").Count());
        Assert.Contains("1", resp.Headers.GetValues("a"));
        Assert.Contains("2", resp.Headers.GetValues("a"));
        Assert.Contains("3", resp.Headers.GetValues("a"));
    }

    [Fact(DisplayName = "Cache control header can be set")]
    public void Case3()
    {
        var resp = new HttpResponseMessage().WithCacheControl(
            new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(20), NoStore = true }
        );
        Assert.NotNull(resp.Headers.CacheControl);
        Assert.Equal(TimeSpan.FromSeconds(20), resp.Headers.CacheControl.MaxAge);
    }

    [Fact(DisplayName = "Connection close header can be set")]
    public void Case4()
    {
        var resp = new HttpResponseMessage().WithConnectionClose(true);
        Assert.True(resp.Headers.ConnectionClose);
    }

    [Fact(DisplayName = "Date header can be set")]
    public void Case5()
    {
        var resp = new HttpResponseMessage().WithDate(DateTimeOffset.UtcNow);
        Assert.NotNull(resp.Headers.Date);
        Assert.Equal(DateTimeOffset.UtcNow, resp.Headers.Date.Value, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = "Transfer-Encoding header can be set")]
    public void Case6()
    {
        var resp = new HttpResponseMessage().WithTransferEncodingChunked(value: true);
        Assert.True(resp.Headers.TransferEncodingChunked);
    }

    [Fact(DisplayName = "Age header can be set")]
    public void Case7()
    {
        var resp = new HttpResponseMessage().WithAge(TimeSpan.FromDays(1));
        Assert.NotNull(resp.Headers.Age);
        Assert.Equal(TimeSpan.FromDays(1), resp.Headers.Age);
    }

    [Fact(DisplayName = "ETag header can be set")]
    public void Case8()
    {
        var resp = new HttpResponseMessage().WithETag(EntityTagHeaderValue.Any);
        Assert.NotNull(resp.Headers.ETag);
    }

    [Fact(DisplayName = "Location header can be set")]
    public void Case9()
    {
        var resp = new HttpResponseMessage().WithLocation("https://some-host");
        Assert.NotNull(resp.Headers.Location);
    }

    [Fact(DisplayName = "RetryAfter header can be set")]
    public void Case10()
    {
        var resp = new HttpResponseMessage().WithRetryAfter(TimeSpan.FromMinutes(1));
        Assert.NotNull(resp.Headers.RetryAfter);
        Assert.NotNull(resp.Headers.RetryAfter.Delta);
        Assert.Equal(TimeSpan.FromMinutes(1), resp.Headers.RetryAfter.Delta.Value);
    }

    [Fact(DisplayName = "RetryAfter header can be set using a date time")]
    public void Case11()
    {
        var resp = new HttpResponseMessage().WithRetryAfter(DateTimeOffset.UtcNow);
        Assert.NotNull(resp.Headers.RetryAfter);
        Assert.NotNull(resp.Headers.RetryAfter.Date);
        Assert.Equal(
            DateTimeOffset.UtcNow,
            resp.Headers.RetryAfter.Date.Value,
            TimeSpan.FromSeconds(1)
        );
    }

    [Fact(DisplayName = "Headers can be modified using an action")]
    public void Case19()
    {
        var resp = new HttpResponseMessage().WithHeaderModifications(h =>
        {
            h.Add("a", "1");
            h.Add("b", "2");
            h.Add("c", "3");
        });
        Assert.Equal(3, resp.Headers.Count());
        Assert.Equal("1", resp.Headers.GetValues("a").First());
        Assert.Equal("2", resp.Headers.GetValues("b").First());
        Assert.Equal("3", resp.Headers.GetValues("c").First());
    }
}
