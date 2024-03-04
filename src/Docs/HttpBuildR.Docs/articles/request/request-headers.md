# Request Headers

HttpBuildR provides a set of methods to modify the headers of
an <xref:System.Net.Http.HttpRequestMessage> object in a fluent manner.

### WithHeader Method

The <xref:HttpBuildR.Request.WithHeader*> method adds a header to the request.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithHeaderMethod)]

[!INCLUDE[TestWithHeaderMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithHeaderMethod.md)]

### WithAuthorization Method

The <xref:HttpBuildR.Request.WithAuthorization*> method adds an authentication header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithAuthorizationMethod)]

[!INCLUDE[TestWithAuthorizationMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithAuthorizationMethod.md)]

### WithProxyAuthorization Method

The <xref:HttpBuildR.Request.WithProxyAuthorization*> method adds a Proxy-Authorization header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithProxyAuthorizationMethod)]

[!INCLUDE[TestWithProxyAuthorizationMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithProxyAuthorizationMethod.md)]

### WithBearerToken Method

The <xref:HttpBuildR.Request.WithBearerToken*> method adds a Bearer authentication token header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithBearerTokenMethod)]

[!INCLUDE[TestWithBearerTokenMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithBearerTokenMethod.md)]

### WithBasicToken Method

The <xref:HttpBuildR.Request.WithBasicToken*> method adds a Basic authentication token header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithBasicTokenMethod)]

[!INCLUDE[TestWithBasicTokenMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithBasicTokenMethod.md)]

### WithCacheControl Method

The <xref:HttpBuildR.Request.WithCacheControl*> method adds a cache control header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithCacheControlMethod)]

[!INCLUDE[TestWithCacheControlMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithCacheControlMethod.md)]

### WithConnectionClose Method

The <xref:HttpBuildR.Request.WithConnectionClose*> method adds a connection closed header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithConnectionCloseMethod)]

[!INCLUDE[TestWithConnectionCloseMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithConnectionCloseMethod.md)]

### WithDate Method

The <xref:HttpBuildR.Request.WithDate*> method adds a date header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithDateMethod)]

[!INCLUDE[TestWithDateMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithDateMethod.md)]

### WithAccept Method

The <xref:HttpBuildR.Request.WithAccept*> method adds to the accept content type header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithAcceptMethod)]

[!INCLUDE[TestWithAcceptMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithAcceptMethod.md)]

### WithIfModifiedSince Method

The <xref:HttpBuildR.Request.WithIfModifiedSince*> method adds a If-Modified-Since header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithIfModifiedSinceMethod)]

[!INCLUDE[TestWithIfModifiedSinceMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithIfModifiedSinceMethod.md)]

### WithIfRange Method

The <xref:HttpBuildR.Request.WithIfRange*> method adds a If-Range header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithIfRangeMethod)]

[!INCLUDE[TestWithIfRangeMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithIfRangeMethod.md)]

### WithIfUnmodifiedSince Method

The <xref:HttpBuildR.Request.WithIfUnmodifiedSince*> method adds a If-Unmodified-Since header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithIfUnmodifiedSinceMethod)]

[!INCLUDE[TestWithIfUnmodifiedSinceMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithIfUnmodifiedSinceMethod.md)]

### WithMaxForwards Method

The <xref:HttpBuildR.Request.WithMaxForwards*> method adds a Max-Forwards header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithMaxForwardsMethod)]

[!INCLUDE[TestWithMaxForwardsMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithMaxForwardsMethod.md)]

### WithRange Method

The <xref:HttpBuildR.Request.WithRange*> method adds a Range header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithRangeMethod)]

[!INCLUDE[TestWithRangeMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithRangeMethod.md)]

### WithReferrer Method

The <xref:HttpBuildR.Request.WithReferrer*> method adds a Referer header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithReferrerMethod)]

[!INCLUDE[TestWithReferrerMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithReferrerMethod.md)]

### WithTransferEncodingChunked Method

The <xref:HttpBuildR.Request.WithTransferEncodingChunked*> method adds a Transfer-Encoding header.

Usage:

[!code-csharp[](../../../../Request/HttpBuildR.Request.Tests/Examples/RequestHeaders.cs#WithTransferEncodingChunkedMethod)]

[!INCLUDE[TestWithTransferEncodingChunkedMethod](../../../../Request/HttpBuildR.Request.Tests/Examples/__examples__/RequestHeaders.TestWithTransferEncodingChunkedMethod.md)]