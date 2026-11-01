![Http BuildR Request](https://raw.githubusercontent.com/bmazzarol/Http-BuildR/main/construction-icon-small.png)

# Http BuildR Request

**Requests and responses, standard library only.**

Extension methods that build an HttpRequestMessage out of a chain, using nothing but System.Net.Http.

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)

```shell
dotnet add package HttpBuildR.Request
```

[Getting started](articles/request/request-overview.html) - [Full documentation](https://bmazzarol.github.io/Http-BuildR/)

## Building a request

```csharp
using HttpBuildR;

// alias the method so the chain reads like a sentence
using Req = HttpMethod;

HttpRequestMessage request =
    Req.Post
        .To("http://some-url/some-part")
        .WithBearerToken(token)
        .WithAccept("application/json")
        .WithHeader("x-custom-header", "a", "b", "c")
        .WithJsonContent(new { Name = "John", Age = 36 });
```

`To` starts the request, every `With*` returns a new `HttpRequestMessage`, and
the result is a plain `HttpRequestMessage` that any `HttpClient` can send.

Content helpers cover json, xml, text, form url encoded and raw streams. Header
helpers cover the standard headers plus arbitrary ones.

## What it does not do

- It does not send the request
- It does not wrap the request in a client or a retry policy
- `Clone` is the only async method; everything else is synchronous

## Examples

The test project in `HttpBuildR.Request.Tests` exercises every builder.
