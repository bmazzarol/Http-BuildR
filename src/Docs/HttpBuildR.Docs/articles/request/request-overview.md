# Getting Started with HttpBuildR Request

HttpBuildR Request is a fluent API for building and working
with <xref:System.Net.Http.HttpRequestMessage> objects. This guide will help you
get started with using HttpBuildR in your projects.

## Installation

To use this library, simply include `HttpBuildR.Request.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/HttpBuildR.Request/), and add
this to the top of each `.cs` file that needs it:

```C#
using HttpBuildR;
using Req = HttpMethod;
```

If you're using 
[implicit usings](https://learn.microsoft.com/en-gb/dotnet/core/project-sdk/overview#implicit-using-directives) 
the above is not necessary.

## Creating a Request

You can create a new <xref:System.Net.Http.HttpRequestMessage> using the 
<xref:HttpBuildR.Request.To*>
method:

```csharp
HttpRequestMessage request = HttpMethod.Get.To("https://example.com");
```

This creates a new GET request to `https://example.com`.

## Modifying Headers

HttpBuildR provides a set of methods to modify the headers of
an <xref:System.Net.Http.HttpRequestMessage>. For more information, see
the [Request Headers](request-headers.md) guide.

Here's an example of how you can add a header to a request:

```csharp
request = request.WithHeader("headerName", "headerValue");
```

## Modifying Content

HttpBuildR also provides methods to modify the content of
an <xref:System.Net.Http.HttpRequestMessage>. For more information, see
the [Request Content](request-content.md) guide.

Here's an example of how you can add JSON content to a request:

```csharp
request = request.WithJsonContent(new { Name = "Ben", Age = "Unknown" });
```

## Sending the Request

Once you've built your request, you can send it using an `HttpClient`:

```csharp
HttpClient client = new HttpClient();
HttpResponseMessage response = await client.SendAsync(request);
```

This sends the request and returns the response.

That's it! You're now ready to start using HttpBuildR in your projects.
