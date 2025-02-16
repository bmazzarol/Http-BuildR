using System.Net;

namespace HttpBuildR.Response.Tests;

public sealed class ResponseTests
{
    [Fact(DisplayName = "A HttpStatusCode can start building a HttpResponseMessage")]
    public void Case1()
    {
        var resp = Resp.OK.Result();
        Assert.Equal(Resp.OK, resp.StatusCode);
        Assert.Equal(2, resp.Version.Major);
        Assert.Equal(0, resp.Version.Minor);
    }

    [Fact(
        DisplayName = "A HttpStatusCode can start building a HttpResponseMessage with a reason phrase, at version 1.1"
    )]
    public void Case2()
    {
        var resp = Resp.Accepted.Result(
            "some reason phrase",
            new HttpRequestMessage(),
            HttpVersion.Version11
        );
        Assert.Equal(Resp.Accepted, resp.StatusCode);
        Assert.Equal("some reason phrase", resp.ReasonPhrase);
        Assert.Equal(HttpVersion.Version11, resp.Version);
    }

    [Fact(DisplayName = "2 builders can be run one after the other, with independent results")]
    public async Task Case3()
    {
        var resp = Resp.OK.Result().WithHeader("a", "1").WithTextContent("test");
        var cloneResp = await resp.Clone();
        cloneResp = cloneResp.WithHeader("b", "2");
        Assert.Single(resp.Headers);
        Assert.Equal(2, cloneResp.Headers.Count());
    }
}
