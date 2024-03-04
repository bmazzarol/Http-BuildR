# Getting Started with HttpBuildR Response

HttpBuildR Response is a fluent API for building and working
with <xref:System.Net.Http.HttpResponseMessage> objects. This guide will help
you get started with using HttpBuildR in your projects.

## Installation

To use this library, simply include `HttpBuildR.Response.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/HttpBuildR.Response/), and add
this to the top of each `.cs` file that needs it:

```C#
using HttpBuildR;
using Resp = HttpStatusCode;
```

## Creating a Response

You can create a new <xref:System.Net.Http.HttpResponseMessage> using
the <xref:HttpBuildR.Response.Result*> method:

```csharp
HttpResponseMessage response = HttpStatusCode.OK.Result();
```

This creates a new response with the status code `OK`.

And with <xref:System.Net.HttpStatusCode> aliased as `Resp`:

```csharp
HttpResponseMessage response = Resp.OK.Result();
```

## Modifying Headers

HttpBuildR provides a set of methods to modify the headers of
an <xref:System.Net.Http.HttpResponseMessage>. For more information, see
the [Response Headers](response-headers.md) guide.

Here's an example of how you can add a header to a response:

```csharp
response = response.WithHeader("headerName", "headerValue");
```

## Modifying Content

HttpBuildR also provides methods to modify the content of
an <xref:System.Net.Http.HttpResponseMessage>. For more information, see
the [Response Content](response-content.md) guide.

Here's an example of how you can add JSON content to a response:

```csharp
response = response.WithJsonContent(new { Name = "Ben", Age = "Unknown" });
```

That's it! You're now ready to start using HttpBuildR in your projects.
