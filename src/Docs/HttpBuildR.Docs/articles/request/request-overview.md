# Request Overview

`HttpBuildR.Request` is a set of extension methods that build a
<xref:System.Net.Http.HttpRequestMessage>. Nothing is wrapped, the result is the
core type.

```shell
dotnet add package HttpBuildR.Request
```

```c#
using HttpBuildR;
using Req = HttpMethod;
```

With
[implicit usings](https://learn.microsoft.com/en-gb/dotnet/core/project-sdk/overview#implicit-using-directives)
the `using HttpBuildR;` line is not needed.

## Start with a method and a url

<xref:HttpBuildR.Request.To*> takes a string or a `Uri`, and an optional http
version:

```c#
HttpRequestMessage request = HttpMethod.Get.To("https://example.com");
```

```text
GET https://example.com/ HTTP/2.0
```

## Add headers

<xref:HttpBuildR.Request.WithHeader*> takes any header. The typed helpers in the
[headers guide](request-headers.md) cover the standard ones.

```c#
request = request.WithHeader("X-Request-Id", "abc-123");
```

```text
GET https://example.com/ HTTP/2.0
X-Request-ID: abc-123
```

Headers are normalised by `System.Net.Http`, so `X-Request-Id` comes back as
`X-Request-ID`.

## Add content

Content helpers replace any existing content:

```c#
request = HttpMethod.Post.To("https://example.com/widgets")
    .WithJsonContent(new { Name = "Ben", Age = 36 });
```

```text
POST https://example.com/widgets HTTP/2.0
Content-Type: application/json; charset=utf-8

{"Name":"Ben","Age":36}
```

Text, xml, form url encoded and raw stream content are all available, see the
[content guide](request-content.md).

## Send it

The builder stops at the request. Sending is still the caller's job:

```c#
using HttpClient client = new HttpClient();
HttpResponseMessage response = await client.SendAsync(request);
```

## See also

* [Request Headers](request-headers.md) - every header helper
* [Request Content](request-content.md) - every content helper
* [Response Overview](../response/response-overview.md) - the same pattern for
  responses
