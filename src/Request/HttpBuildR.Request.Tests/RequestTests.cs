using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace HttpBuildR.Tests;

[SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded")]
public sealed class RequestTests
{
    [Fact(DisplayName = "A HttpMethod can start building a HttpRequestMessage with a string uri")]
    public void Case1()
    {
        var req = Req.Get.To("Http://some-host");
        Assert.Equal(Req.Get, req.Method);
        Assert.Equal(new Uri("Http://some-host"), req.RequestUri);
        Assert.Equal(HttpVersion.Version20, req.Version);
    }

    [Fact(DisplayName = "A HttpMethod can start building a HttpRequestMessage with a Uri uri")]
    public void Case2()
    {
        var req = Req.Get.To(new Uri("Http://some-host"));
        Assert.Equal(Req.Get, req.Method);
        Assert.Equal(new Uri("Http://some-host"), req.RequestUri);
        Assert.Equal(HttpVersion.Version20, req.Version);
    }

    [Fact(
        DisplayName = "A HttpMethod can start building a HttpRequestMessage with a string uri, at version 1.1"
    )]
    public void Case3()
    {
        var req = Req.Get.To("Http://some-host", HttpVersion.Version11);
        Assert.Equal(Req.Get, req.Method);
        Assert.Equal(new Uri("Http://some-host"), req.RequestUri);
        Assert.Equal(HttpVersion.Version11, req.Version);
    }

    [Fact(
        DisplayName = "A HttpMethod can start building a HttpRequestMessage with a Uri uri, at version 1.1"
    )]
    public void Case4()
    {
        var req = Req.Get.To(new Uri("Http://some-host"), HttpVersion.Version11);
        Assert.Equal(Req.Get, req.Method);
        Assert.Equal(new Uri("Http://some-host"), req.RequestUri);
        Assert.Equal(HttpVersion.Version11, req.Version);
    }

    [Fact(DisplayName = "2 builders can be run one after the other, with independent results")]
    public Task Case5() =>
        Req
            .Get.To(new Uri("Http://some-host"))
            .WithHeader("a", "1")
            .WithTextContent("test")
            .Arrange()
            .Act(async req => (await req.Clone()).WithHeader("b", "2"))
            .Assert(
                (req1, req2) =>
                {
                    Assert.Single(req1.Headers);
                    Assert.Equal(2, req2.Headers.Count());
                }
            );
}
