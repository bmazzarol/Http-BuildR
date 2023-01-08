namespace HttpBuildR.Request.Tests;

using Req = HttpMethod;

public static class RequestTests
{
    [Fact(DisplayName = "A HttpMethod can start building a HttpRequestMessage with a string uri")]
    public static void Case1() =>
        Req.Get
            .To("Http://some-host")
            .Should()
            .BeEquivalentTo(new { Method = Req.Get, RequestUri = new Uri("Http://some-host") });

    [Fact(DisplayName = "A HttpMethod can start building a HttpRequestMessage with a Uri uri")]
    public static void Case2() =>
        Req.Get
            .To(new Uri("Http://some-host"))
            .Should()
            .BeEquivalentTo(new { Method = Req.Get, RequestUri = new Uri("Http://some-host") });
}
