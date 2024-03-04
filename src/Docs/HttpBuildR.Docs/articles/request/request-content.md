# Request Content

HttpBuildR provides a set of methods to modify the content of
an <xref:System.Net.Http.HttpRequestMessage> object in a fluent manner.

### WithContent Method

The <xref:HttpBuildR.Request.WithContent*> method modifies the request content.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestContent.cs#WithContentMethod)]

### WithJsonContent Method

The `WithJsonContent` method modifies the request content with
JSON `StringContent`.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestContent.cs#WithJsonContentMethod)]

### WithXmlContent Method

The `WithXmlContent` method modifies the request content with
XML `StringContent`.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestContent.cs#WithXmlContentMethod)]

### WithTextContent Method

The `WithTextContent` method modifies the request content with
text `StringContent`.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestContent.cs#WithTextContentMethod)]

### WithFormUrlContent Method

The `WithFormUrlContent` method modifies the request content
with `FormUrlEncodedContent`.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestContent.cs#WithFormUrlContentMethod)]