<!-- markdownlint-disable MD013 -->

# ![Http BuildR Request](https://raw.githubusercontent.com/bmazzarol/Http-BuildR/main/construction-icon-small.png) Http BuildR Request

<!-- markdownlint-enable MD013 -->

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)

Http BuildR Request is a simple set of C# functions for building requests using
only System.Net.Http!

## Getting Started

To use this library, simply include `HttpBuildR.Request.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/HttpBuildR.Request/), and add
this to the top of each `.cs` file that needs it:

```C#
using static HttpBuildR.Request;
using Req = HttpMethod;
```

Then get building!,

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
     .WithContentType("application/json")
     .WithHeader("x-custom-header", "a","b","c")
     // with some content, they are all supported!
     .WithJsonContent(new {Name = "John", Age = 36});
// now you can send it!!!
...
```

For more details/information have a look the test project or create an issue.
