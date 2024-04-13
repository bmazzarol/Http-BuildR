<!-- markdownlint-disable MD033 MD041 -->
<div align="center">

<img src="construction-icon.png" alt="HTTP BuildR" width="150px"/>

# HTTP BuildR

[:running: **_Getting Started_**](https://bmazzarol.github.io/Http-BuildR/articles/getting-started.html)
[:books: **_Documentation_**](https://bmazzarol.github.io/Http-BuildR)

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Http-BuildR&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Http-BuildR)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Http-BuildR&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Http-BuildR)
[![CD Build](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml)
[![Check Markdown](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml)

Simple C# functions for building :hammer: requests and responses using only the
core System.Net.Http!

</div>

## Features

Simple request response builders in C# without all the fancy frameworks.

* Just System.Net.Http
* Zero dependencies
* Easy to use and understand
* No additional allocations
* Does only what it says on the tin, builds requests and responses :hammer:

```c#
using HttpBuildR;

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
using HttpBuildR;
using Req = HttpMethod;
```

> [!NOTE]
> If you're using
> [implicit usings](https://learn.microsoft.com/en-gb/dotnet/core/project-sdk/overview#implicit-using-directives)
> the above is not necessary.

Click through to the links bellow for further details.
<!-- markdownlint-disable MD013 -->

| Library                                                              | Description                                                                    | Nu-Get                                                                                                                      |
|----------------------------------------------------------------------|--------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| [Request](./src/Request/HttpBuildR.Request/README.md)                | Simple request builder functions on top of System.Net.Http!                    | [![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)           |
| [Response](./src/Response/HttpBuildR.Response/README.md)             | Simple response builder functions on top of System.Net.Http!                   | [![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Response)](https://www.nuget.org/packages/HttpBuildR.Response/)         |
| [ActionResult](./src/ActionResult/HttpBuildR.ActionResult/README.md) | Simple ActionResult builder functions, building on-top of HttpBuildR.Response! | [![Nuget](https://img.shields.io/nuget/v/HttpBuildR.ActionResult)](https://www.nuget.org/packages/HttpBuildR.ActionResult/) |

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
