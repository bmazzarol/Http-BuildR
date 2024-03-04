using System.Xml.Serialization;

namespace HttpBuildR.Request.Tests.Examples;

public class RequestContentTests
{
    [Fact]
    public async Task TestWithContentMethod()
    {
        #region WithContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithContent(new StringContent("content"));
        string result = await request.Content!.ReadAsStringAsync();
        result.Should().Be("content");
        request.Content.Headers.ContentType!.ToString().Should().Be("text/plain; charset=utf-8");

        #endregion
    }

    [Fact]
    public async Task TestWithJsonContentMethod()
    {
        #region WithJsonContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithJsonContent(new { Name = "Ben", Age = "Unknown" });
        string result = await request.Content!.ReadAsStringAsync();
        result.Should().Be("{\"Name\":\"Ben\",\"Age\":\"Unknown\"}");
        request
            .Content.Headers.ContentType!.ToString()
            .Should()
            .Be("application/json; charset=utf-8");

        #endregion
    }

    #region WithXmlContentMethod

    [XmlRoot("Widget")]
    public class Widget
    {
        public string Name { get; set; }
        public int PartNumber { get; set; }
    }

    [Fact]
    public async Task TestWithXmlContentMethod()
    {
        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithXmlContent(new Widget { Name = "Doohickey", PartNumber = 10 });
        string result = await request.Content!.ReadAsStringAsync();
        result
            .Should()
            .Be(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                    + "<Widget xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"
                    + "<Name>Doohickey</Name>"
                    + "<PartNumber>10</PartNumber>"
                    + "</Widget>"
            );
        request.Content.Headers.ContentType!.ToString().Should().Be("text/xml; charset=utf-8");
    }

    #endregion

    [Fact]
    public async Task TestWithTextContentMethod()
    {
        #region WithTextContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithTextContent("content");
        string result = await request.Content!.ReadAsStringAsync();
        result.Should().Be("content");
        request.Content.Headers.ContentType!.ToString().Should().Be("text/plain; charset=utf-8");

        #endregion
    }

    [Fact]
    public async Task TestWithFormUrlContentMethod()
    {
        #region WithFormUrlContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithFormUrlContent(new KeyValuePair<string, string>("key", "value"));
        string result = await request.Content!.ReadAsStringAsync();
        result.Should().Be("key=value");
        request
            .Content.Headers.ContentType!.ToString()
            .Should()
            .Be("application/x-www-form-urlencoded");

        #endregion
    }
}
