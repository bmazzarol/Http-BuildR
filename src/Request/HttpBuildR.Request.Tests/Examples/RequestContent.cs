using System.Xml.Serialization;

namespace HttpBuildR.Tests.Examples;

public class RequestContentTests
{
    [Fact]
    public async Task TestWithContentMethod()
    {
        #region WithContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithContent(new StringContent("content"));
        string result = await request.Content!.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );

        Assert.Equal("content", result);
        Assert.Equal("text/plain; charset=utf-8", request.Content.Headers.ContentType!.ToString());

        #endregion
    }

    [Fact]
    public async Task TestWithJsonContentMethod()
    {
        #region WithJsonContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithJsonContent(new { Name = "Ben", Age = "Unknown" });
        string result = await request.Content!.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );

        Assert.Equal("{\"Name\":\"Ben\",\"Age\":\"Unknown\"}", result);
        Assert.Equal(
            "application/json; charset=utf-8",
            request.Content.Headers.ContentType!.ToString()
        );

        #endregion
    }

    #region WithXmlContentMethod

    [XmlRoot("Widget")]
    public class Widget
    {
        public required string Name { get; set; }
        public int PartNumber { get; set; }
    }

    [Fact]
    public async Task TestWithXmlContentMethod()
    {
        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithXmlContent(new Widget { Name = "Doohickey", PartNumber = 10 });
        string result = await request.Content!.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );

        Assert.Equal(
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                + "<Widget xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"
                + "<Name>Doohickey</Name>"
                + "<PartNumber>10</PartNumber>"
                + "</Widget>",
            result
        );
        Assert.Equal("text/xml; charset=utf-8", request.Content.Headers.ContentType!.ToString());
    }

    #endregion

    [Fact]
    public async Task TestWithTextContentMethod()
    {
        #region WithTextContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithTextContent("content");
        string result = await request.Content!.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );

        Assert.Equal("content", result);
        Assert.Equal("text/plain; charset=utf-8", request.Content.Headers.ContentType!.ToString());

        #endregion
    }

    [Fact]
    public async Task TestWithFormUrlContentMethod()
    {
        #region WithFormUrlContentMethod

        HttpRequestMessage request = new HttpRequestMessage();
        request = request.WithFormUrlContent(new KeyValuePair<string, string>("key", "value"));
        string result = await request.Content!.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );

        Assert.Equal("key=value", result);
        Assert.Equal(
            "application/x-www-form-urlencoded",
            request.Content.Headers.ContentType!.ToString()
        );
        #endregion
    }
}
