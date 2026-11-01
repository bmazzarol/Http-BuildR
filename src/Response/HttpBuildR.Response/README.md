![Http BuildR Response](https://raw.githubusercontent.com/bmazzarol/Http-BuildR/main/construction-icon-small.png)

# Http BuildR Response

**Requests and responses, standard library only.**

Extension methods that build a HttpResponseMessage out of a chain, using nothing but System.Net.Http. The usual place for this is a test that needs an expected response.

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Response)](https://www.nuget.org/packages/HttpBuildR.Response/)

```shell
dotnet add package HttpBuildR.Response
```

[Getting started](articles/response/response-overview.html) - [Full documentation](https://bmazzarol.github.io/Http-BuildR/)

## Building a response

```csharp
using HttpBuildR;

// alias the status code so the chain reads like a sentence
using Resp = HttpStatusCode;

HttpResponseMessage response =
    Resp.Created
        .Result()
        .WithHeader("x-custom-header", "a", "b", "c")
        .WithJsonContent(new { Name = "John", Age = 36 });
```

`Result` starts the response from a status code, every `With*` returns a new
`HttpResponseMessage`, and the result is a plain `HttpResponseMessage`.

Content helpers cover json, xml, text, form url encoded and raw streams. Header
helpers cover the standard response headers plus arbitrary ones.

## What it does not do

- It does not send or serve the response
- It does not wrap the response in a framework specific type

## Examples

The test project in `HttpBuildR.Response.Tests` exercises every builder.
