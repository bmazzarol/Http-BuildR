using System.Net;

namespace HttpBuildR.Request.Tests;

public sealed class RequestTests
{
    [Fact(DisplayName = "A HttpMethod can start building a HttpRequestMessage with a string uri")]
    public void Case1() =>
        Req
            .Get.To("Http://some-host")
            .Should()
            .BeEquivalentTo(
                new
                {
                    Method = Req.Get,
                    RequestUri = new Uri("Http://some-host"),
                    Version = HttpVersion.Version20
                }
            );

    [Fact(DisplayName = "A HttpMethod can start building a HttpRequestMessage with a Uri uri")]
    public void Case2() =>
        Req
            .Get.To(new Uri("Http://some-host"))
            .Should()
            .BeEquivalentTo(
                new
                {
                    Method = Req.Get,
                    RequestUri = new Uri("Http://some-host"),
                    Version = HttpVersion.Version20
                }
            );

    [Fact(
        DisplayName = "A HttpMethod can start building a HttpRequestMessage with a string uri, at version 1.1"
    )]
    public void Case3() =>
        Req
            .Get.To("Http://some-host", HttpVersion.Version11)
            .Should()
            .BeEquivalentTo(
                new
                {
                    Method = Req.Get,
                    RequestUri = new Uri("Http://some-host"),
                    Version = HttpVersion.Version11
                }
            );

    [Fact(
        DisplayName = "A HttpMethod can start building a HttpRequestMessage with a Uri uri, at version 1.1"
    )]
    public void Case4() =>
        Req
            .Get.To(new Uri("Http://some-host"), HttpVersion.Version11)
            .Should()
            .BeEquivalentTo(
                new
                {
                    Method = Req.Get,
                    RequestUri = new Uri("Http://some-host"),
                    Version = HttpVersion.Version11
                }
            );

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
                    req1.Headers.Should().HaveCount(1);
                    req2.Headers.Should().HaveCount(2);
                }
            );
}
