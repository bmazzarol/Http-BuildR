# Response Overview

`HttpBuildR.Response` is a set of extension methods that build a
<xref:System.Net.Http.HttpResponseMessage>. The usual place for this is a test
that needs an expected response.

```shell
dotnet add package HttpBuildR.Response
```

```c#
using HttpBuildR;
using Resp = HttpStatusCode;
```

With
[implicit usings](https://learn.microsoft.com/en-gb/dotnet/core/project-sdk/overview#implicit-using-directives)
the `using HttpBuildR;` line is not needed.

## Start with a status code

<xref:HttpBuildR.Response.Result*> takes an optional reason phrase, the request
the response answers, and an http version:

```c#
HttpResponseMessage response = HttpStatusCode.Created.Result();
```

```text
HTTP/2.0 Created Created
```

The reason phrase defaults to the name of the status code.

## Add headers

<xref:HttpBuildR.Response.WithHeader*> takes any header. The typed helpers in the
[headers guide](response-headers.md) cover the standard ones.

```c#
response = response.WithHeader("X-Request-Id", "abc-123");
```

```text
HTTP/2.0 Created Created
X-Request-ID: abc-123
```

## Add content

Content helpers replace any existing content:

```c#
response = response.WithJsonContent(new { Name = "Ben", Age = 36 });
```

```text
HTTP/2.0 Created Created
X-Request-ID: abc-123
Content-Type: application/json; charset=utf-8

{"Name":"Ben","Age":36}
```

Text, xml, form url encoded and raw stream content are all available, see the
[content guide](response-content.md).

## See also

* [Response Headers](response-headers.md) - every header helper
* [Response Content](response-content.md) - every content helper
* [Request Overview](../request/request-overview.md) - the same pattern for
  requests
