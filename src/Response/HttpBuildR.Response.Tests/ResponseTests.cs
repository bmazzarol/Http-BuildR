using System.Net;

namespace HttpBuildR.Response.Tests;

public sealed class ResponseTests
{
    [Fact(DisplayName = "A HttpStatusCode can start building a HttpResponseMessage")]
    public void Case1() =>
        Resp
            .OK.Result()
            .Should()
            .BeEquivalentTo(new { StatusCode = Resp.OK, Version = HttpVersion.Version20 });

    [Fact(
        DisplayName = "A HttpStatusCode can start building a HttpResponseMessage with a reason phrase, at version 1.1"
    )]
    public void Case2() =>
        Resp
            .Accepted.Result("some reason phrase", new HttpRequestMessage(), HttpVersion.Version11)
            .Should()
            .BeEquivalentTo(
                new
                {
                    StatusCode = Resp.Accepted,
                    ReasonPhrase = "some reason phrase",
                    Version = HttpVersion.Version11,
                }
            );

    [Fact(DisplayName = "2 builders can be run one after the other, with independent results")]
    public Task Case3() =>
        Resp
            .OK.Result()
            .WithHeader("a", "1")
            .WithTextContent("test")
            .Arrange()
            .Act(async req => (await req.Clone()).WithHeader("b", "2"))
            .Assert(
                (req1, req2) =>
                {
                    req1.Headers.Should().HaveCount(1);
                    req2.Headers.Should().HaveCount(2);
                }
            );
}
