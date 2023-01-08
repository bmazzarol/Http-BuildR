<!-- markdownlint-disable MD033 -->

# <img height="50" src="construction-icon.png" width="50"/> Http BuildR

<!-- markdownlint-enabled MD033 -->

[![Coverage Status](https://coveralls.io/repos/github/bmazzarol/Http-BuildR/badge.svg?branch=main)](https://coveralls.io/github/bmazzarol/Http-BuildR?branch=main)
[![CodeQL](https://github.com/bmazzarol/Http-BuildR/actions/workflows/codeql.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/codeql.yml)
[![CD Build](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml)
[![Check Markdown](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml)
[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)

> Simple :wrench: Http Builders :hammer: using vanilla dotnet!
---

Simple request response builders in C# without all the fancy frameworks.

* Just System.Net.Http
* Zero dependencies
* Easy to use and understand
* Does only what it says on the tin, builds requests and responses :hammer:

```c#
using static HttpBuildR.Request;

// its helpful to alias this for readability
using Req = HttpMethod;

...

HttpRequestMessage request = 
  // start with the Http method
  Req.Post
     // now we can say where the request is made to
     // any thing that is a string or Uri will work
     // plays nice with the flurl Url builder!
     .To("http://some-url/some-part")
     // add some headers
     .WithBearerToken(token)
     .WithAccept("application/json")
     .WithHeader("x-custom-header", "a","b","c")
     // with some content, they are all supported!
     .WithJsonContent(new {Name = "John", Age = 36});
// now you can send it!!!
...
```

## Getting Started

To use this library, simply include `HttpBuildR.Request.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/HttpBuildR.Request/), and add
this to the top of each `.cs` file that needs it:

```C#
using static HttpBuildR.Request;
using Req = HttpMethod;
```

Click through to the links bellow for further details.
<!-- markdownlint-disable MD013 -->

| Library                                     | Description                                                  | Nu-Get                                                                                                              |
|---------------------------------------------|--------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------|
| [Request](./HttpBuildR.Request/README.md)   | Simple request builder functions on top of System.Net.Http!  | [![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)   |
| [Response](./HttpBuildR.Response/README.md) | Simple response builder functions on top of System.Net.Http! | [![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Response)](https://www.nuget.org/packages/HttpBuildR.Response/) |

<!-- markdownlint-enable MD013 -->

## Why?

There are a ton of http client libraries out there, but nothing (that I liked)
that was just simple extensions to the core System.Net.Http classes.

I want the request and response building code to be,

* Declarative, it needs to flow and read as well as possible
* Simple, usage should be trivial and leaning curve as flat as possible
* Complete, anything you can do with the core class can be done in a fluent
  declarative style

For more details/information have a look the test projects or create an issue.

## Attributions

[Construction icons created by juicy_fish](https://www.flaticon.com/free-icons/construction)
