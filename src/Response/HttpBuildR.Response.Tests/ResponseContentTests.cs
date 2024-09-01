﻿using System.Net.Mime;
using System.Text.Json;
using System.Xml;

namespace HttpBuildR.Response.Tests;

using Scenario = TestBuilder<ArrangeActAssertSyntax>.Acted<HttpResponseMessage, HttpContent>;

public sealed class ResponseContentTests
{
    private static Scenario Arrange(Func<HttpResponseMessage, HttpResponseMessage> fn) =>
        new HttpResponseMessage()
            .Arrange()
            .Act(fn)
            .And((_, req) => req.Content ?? throw new InvalidOperationException());

    [Fact(DisplayName = "Json content can be added to a response")]
    public Task Case1() =>
        Arrange(x => x.WithJsonContent(new { A = 1, B = "2" }, JsonSerializerOptions.Default))
            .Assert(async content =>
            {
                content.Headers.ContentType!.MediaType.Should().Be("application/json");
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo(@"{""A"":1,""B"":""2""}");
            });

    [Fact(DisplayName = "Json content can be added to a response without options")]
    public Task Case2() =>
        Arrange(x => x.WithJsonContent(new { A = 1, B = "2" }))
            .Assert(async content =>
            {
                content.Headers.ContentType!.MediaType.Should().Be("application/json");
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo(@"{""A"":1,""B"":""2""}");
            });

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    [Fact(DisplayName = "Xml content can be added to a response")]
    public Task Case3() =>
        Arrange(x =>
                x.WithXmlContent(
                    new Person { Name = "John", Age = 36 },
                    new XmlWriterSettings { Indent = false }
                )
            )
            .Assert(async content =>
            {
                content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Text.Xml);
                var result = await content.ReadAsStringAsync();
                result
                    .Should()
                    .BeEquivalentTo(
                        @"<?xml version=""1.0"" encoding=""utf-8""?><Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><Name>John</Name><Age>36</Age></Person>"
                    );
            });

    [Fact(DisplayName = "Xml content can be added to a response without settings")]
    public Task Case4() =>
        Arrange(x =>
                x.WithXmlContent(
                    new Person { Name = "John", Age = 36 },
                    modifyWriterFunc: writer => writer
                )
            )
            .Assert(async content =>
            {
                content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Text.Xml);
                var result = await content.ReadAsStringAsync();
                result
                    .Should()
                    .BeEquivalentTo(
                        @"<?xml version=""1.0"" encoding=""utf-8""?><Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><Name>John</Name><Age>36</Age></Person>"
                    );
            });

    [Fact(DisplayName = "Text content can be added to a response")]
    public Task Case5() =>
        Arrange(x => x.WithTextContent("<div>some text</div>", "text/html"))
            .Assert(async content =>
            {
                content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Text.Html);
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo("<div>some text</div>");
            });

    [Fact(DisplayName = "Text content can be added to a response without media type")]
    public Task Case6() =>
        Arrange(x => x.WithTextContent("hello world"))
            .Assert(async content =>
            {
                content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Text.Plain);
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo("hello world");
            });

    [Fact(DisplayName = "Form url encoded content can be added to a response")]
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
                content
                    .Headers.ContentType!.MediaType.Should()
                    .Be("application/x-www-form-urlencoded");
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo("A=1&B=2");
            });

    [Fact(DisplayName = "Form url encoded content can be added to a request")]
    public Task Case7B() =>
        Arrange(x =>
                x.WithFormUrlContent(KeyValuePair.Create("A", "1"), KeyValuePair.Create("B", "2"))
            )
            .Assert(async content =>
            {
                content
                    .Headers.ContentType!.MediaType.Should()
                    .Be("application/x-www-form-urlencoded");
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo("A=1&B=2");
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
                content.Headers.ContentType!.MediaType.Should().Be("application/json");
                var result = await content.ReadAsStringAsync();
                result.Should().BeEquivalentTo(@"{""Name"":""Test"",""Cost"":123.5}");
            });
}
