![Http BuildR](construction-icon.png)

# Http BuildR

**Requests and responses, standard library only.**

A fluent builder for HttpRequestMessage and HttpResponseMessage built on System.Net.Http alone, with no dependencies and no wrapper types.

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Http-BuildR&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Http-BuildR) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Http-BuildR&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Http-BuildR) [![CD Build](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml) [![Check Markdown](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml)

[:running: **_Getting Started_**](https://bmazzarol.github.io/Http-BuildR/articles/request/request-overview.html) | [:books: **_Documentation_**](https://bmazzarol.github.io/Http-BuildR/)

## What it does

The builders are extension methods on the types in `System.Net.Http`, so a
request or response stays a plain `HttpRequestMessage` or
`HttpResponseMessage`.

* No dependencies beyond `System.Net.Http`
* No wrapper types
* Headers, content and version set in one chain

```c#
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

The chain builds a request. Sending it is still up to the caller.

## Packages

| Package | Builds | NuGet |
| --- | --- | --- |
| HttpBuildR.Request | `HttpRequestMessage` | [nuget](https://www.nuget.org/packages/HttpBuildR.Request/) |
| HttpBuildR.Response | `HttpResponseMessage` | [nuget](https://www.nuget.org/packages/HttpBuildR.Response/) |
| HttpBuildR.ActionResult | `HttpResponseMessage` from an action result | [nuget](https://www.nuget.org/packages/HttpBuildR.ActionResult/) |

## Documentation

* [Request overview](https://bmazzarol.github.io/Http-BuildR/articles/request/request-overview.html)
* [Request headers](https://bmazzarol.github.io/Http-BuildR/articles/request/request-headers.html)
* [Request content](https://bmazzarol.github.io/Http-BuildR/articles/request/request-content.html)
* [Response overview](https://bmazzarol.github.io/Http-BuildR/articles/response/response-overview.html)

The test projects in `src/Request/HttpBuildR.Request.Tests` and
`src/Response/HttpBuildR.Response.Tests` cover every builder, and are the
closest thing to a reference for edge cases.

## Why another one?

Most http libraries wrap `System.Net.Http` in a client of their own. This one
keeps the core types and only replaces the setup code with a chain, so anything
that already accepts an `HttpRequestMessage` keeps working.

## Attributions

[Construction icons created by juicy_fish](https://www.flaticon.com/free-icons/construction)
