using System.Net.Mime;
using System.Text.Json;
using System.Xml;

namespace HttpBuildR.Tests;

using Scenario = TestBuilder<ArrangeActAssertSyntax>.Acted<HttpRequestMessage, HttpContent>;

public sealed class RequestContentTests
{
    private static Scenario Arrange(Func<HttpRequestMessage, HttpRequestMessage> fn) =>
        new HttpRequestMessage()
            .Arrange()
            .Act(fn)
            .And((_, req) => req.Content ?? throw new InvalidOperationException());

    [Fact(DisplayName = "Json content can be added to a request")]
    public Task Case1() =>
        Arrange(x => x.WithJsonContent(new { A = 1, B = "2" }, JsonSerializerOptions.Default))
            .Assert(async content =>
            {
                Assert.Equal("application/json", content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal(@"{""A"":1,""B"":""2""}", result);
            });

    [Fact(DisplayName = "Json content can be added to a request without options")]
    public Task Case2() =>
        Arrange(x => x.WithJsonContent(new { A = 1, B = "2" }))
            .Assert(async content =>
            {
                Assert.Equal("application/json", content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal(@"{""A"":1,""B"":""2""}", result);
            });

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    [Fact(DisplayName = "Xml content can be added to a request")]
    public Task Case3() =>
        Arrange(x =>
                x.WithXmlContent(
                    new Person { Name = "John", Age = 36 },
                    new XmlWriterSettings { Indent = false }
                )
            )
            .Assert(async content =>
            {
                Assert.Equal(MediaTypeNames.Text.Xml, content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal(
                    """<?xml version="1.0" encoding="utf-8"?><Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><Name>John</Name><Age>36</Age></Person>""",
                    result
                );
            });

    [Fact(DisplayName = "Xml content can be added and the writer customized")]
    public Task Case4() =>
        Arrange(x =>
                x.WithXmlContent(new Person { Name = "John", Age = 36 }, modifyWriterFunc: w => w)
            )
            .Assert(async content =>
            {
                Assert.Equal(MediaTypeNames.Text.Xml, content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal(
                    """<?xml version="1.0" encoding="utf-8"?><Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><Name>John</Name><Age>36</Age></Person>""",
                    result
                );
            });

    [Fact(DisplayName = "Text content can be added to a request")]
    public Task Case5() =>
        Arrange(x => x.WithTextContent("<div>some text</div>", "text/html"))
            .Assert(async content =>
            {
                Assert.Equal("text/html", content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal("<div>some text</div>", result);
            });

    [Fact(DisplayName = "Text content can be added to a request without media type")]
    public Task Case6() =>
        Arrange(x => x.WithTextContent("hello world"))
            .Assert(async content =>
            {
                Assert.Equal("text/plain", content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal("hello world", result);
            });

    [Fact(DisplayName = "Form url encoded content can be added to a request")]
    public Task Case7() =>
        Arrange(x =>
                x.WithFormUrlContent(
                    new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["A"] = "1",
                        ["B"] = "2",
                    }
                )
            )
            .Assert(async content =>
            {
                Assert.Equal(
                    "application/x-www-form-urlencoded",
                    content.Headers.ContentType!.MediaType
                );
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal("A=1&B=2", result);
            });

    [Fact(DisplayName = "Form url encoded content can be added to a request")]
    public Task Case7B() =>
        Arrange(x =>
                x.WithFormUrlContent(KeyValuePair.Create("A", "1"), KeyValuePair.Create("B", "2"))
            )
            .Assert(async content =>
            {
                Assert.Equal(
                    "application/x-www-form-urlencoded",
                    content.Headers.ContentType!.MediaType
                );
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal("A=1&B=2", result);
            });

    [Fact(DisplayName = "Json content can be added to a request using a source generator")]
    public Task Case8() =>
        Arrange(x =>
                x.WithJsonContent(
                    new Widget("Test", 123.50),
                    ExampleJsonSourceGenerator.Default.Widget
                )
            )
            .Assert(async content =>
            {
                Assert.Equal("application/json", content.Headers.ContentType!.MediaType);
                var result = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
                Assert.Equal("""{"Name":"Test","Cost":123.5}""", result);
            });
}
