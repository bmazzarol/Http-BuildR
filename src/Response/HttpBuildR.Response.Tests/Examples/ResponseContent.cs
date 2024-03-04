using System.Xml.Serialization;

namespace HttpBuildR.Response.Tests.Examples
{
    public class ResponseContentTests
    {
        [Fact]
        public async Task TestWithContentMethod()
        {
            #region WithContentMethod

            HttpResponseMessage response = new HttpResponseMessage();
            response = response.WithContent(new StringContent("content"));
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("content");
            response
                .Content.Headers.ContentType.ToString()
                .Should()
                .Be("text/plain; charset=utf-8");

            #endregion
        }

        [Fact]
        public async Task TestWithJsonContentMethod()
        {
            #region WithJsonContentMethod

            HttpResponseMessage response = new HttpResponseMessage();
            response = response.WithJsonContent(new { Name = "Ben", Age = "Unknown" });
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("{\"Name\":\"Ben\",\"Age\":\"Unknown\"}");
            response
                .Content.Headers.ContentType.ToString()
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
            HttpResponseMessage response = new HttpResponseMessage();
            response = response.WithXmlContent(new Widget { Name = "Doohickey", PartNumber = 10 });
            string result = await response.Content.ReadAsStringAsync();
            result
                .Should()
                .Be(
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                        + "<Widget xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"
                        + "<Name>Doohickey</Name>"
                        + "<PartNumber>10</PartNumber>"
                        + "</Widget>"
                );
            response.Content.Headers.ContentType!.ToString().Should().Be("text/xml; charset=utf-8");
        }

        #endregion

        [Fact]
        public async Task TestWithTextContentMethod()
        {
            #region WithTextContentMethod

            HttpResponseMessage response = new HttpResponseMessage();
            response = response.WithTextContent("content");
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("content");
            response
                .Content.Headers.ContentType.ToString()
                .Should()
                .Be("text/plain; charset=utf-8");

            #endregion
        }

        [Fact]
        public async Task TestWithFormUrlContentMethod()
        {
            #region WithFormUrlContentMethod

            HttpResponseMessage response = new HttpResponseMessage();
            response = response.WithFormUrlContent(
                new KeyValuePair<string, string>("key", "value")
            );
            string result = await response.Content!.ReadAsStringAsync();
            result.Should().Be("key=value");
            response
                .Content.Headers.ContentType!.ToString()
                .Should()
                .Be("application/x-www-form-urlencoded");

            #endregion
        }
    }
}
