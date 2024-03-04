# Response Headers

HttpBuildR provides a set of methods to modify the headers of
an <xref:System.Net.Http.HttpResponseMessage> object in a fluent manner.

### WithHeader Method

The <xref:HttpBuildR.Response.WithHeader*> method adds a header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithHeaderMethod)]

[!INCLUDE[TestWithHeaderMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithHeaderMethod.md)]

### WithAge Method

The <xref:HttpBuildR.Response.WithAge*> method adds an Age header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithAgeMethod)]

[!INCLUDE[TestWithAgeMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithAgeMethod.md)]

### WithETag Method

The <xref:HttpBuildR.Response.WithETag*> method adds an ETag header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithETagMethod)]

[!INCLUDE[TestWithETagMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithETagMethod.md)]

### WithLocation Method

The <xref:HttpBuildR.Response.WithLocation*> method adds a Location header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithLocationMethod)]

[!INCLUDE[TestWithLocationMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithLocationMethod.md)]

### WithRetryAfter Method

The <xref:HttpBuildR.Response.WithRetryAfter*> method adds a Retry-After header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithRetryAfterMethod)]

[!INCLUDE[TestWithRetryAfterMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithRetryAfterMethod.md)]

### WithCacheControl Method

The <xref:HttpBuildR.Response.WithCacheControl*> method adds a Cache-Control header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithCacheControlMethod)]

[!INCLUDE[TestWithCacheControlMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithCacheControlMethod.md)]

### WithConnectionClose Method

The <xref:HttpBuildR.Response.WithConnectionClose*> method adds a Connection-Close header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithConnectionCloseMethod)]

[!INCLUDE[TestWithConnectionCloseMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithConnectionCloseMethod.md)]

### WithDate Method

The <xref:HttpBuildR.Response.WithDate*> method adds a Date header to the response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithDateMethod)]

[!INCLUDE[TestWithDateMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithDateMethod.md)]

### WithTransferEncodingChunked Method

The <xref:HttpBuildR.Response.WithTransferEncodingChunked*> method adds a Transfer-Encoding header to the
response.

Usage:

[!code-csharp[](../../../../Response/HttpBuildR.Response.Tests/Examples/ResponseHeaders.cs#WithTransferEncodingChunkedMethod)]

[!INCLUDE[TestWithTransferEncodingChunkedMethod](../../../../Response/HttpBuildR.Response.Tests/Examples/__examples__/ResponseHeaders.TestWithTransferEncodingChunkedMethod.md)]
