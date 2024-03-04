# Response Content

HttpBuildR provides a set of methods to modify the content of
an <xref:System.Net.Http.HttpResponseMessage> object in a fluent manner.

### WithContent Method

The <xref:HttpBuildR.Response.WithContent*> method modifies the response
content.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseContent.cs#WithContentMethod)]

### WithJsonContent Method

The <xref:HttpBuildR.Response.WithJsonContent*> method modifies the response
content with JSON content.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseContent.cs#WithJsonContentMethod)]

### WithXmlContent Method

The <xref:HttpBuildR.Response.WithXmlContent*> method modifies the response
content with XML content.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseContent.cs#WithXmlContentMethod)]

### WithTextContent Method

The <xref:HttpBuildR.Response.WithTextContent*> method modifies the response
content with text content.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseContent.cs#WithTextContentMethod)]

### WithFormUrlContent Method

The <xref:HttpBuildR.Response.WithFormUrlContent*> method modifies the response
content with `FormUrlEncodedContent`.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseContent.cs#WithFormUrlContentMethod)]