<!-- markdownlint-disable MD013 -->

# ![HTTP BuildR Response](https://raw.githubusercontent.com/bmazzarol/Http-BuildR/main/construction-icon-small.png) HTTP BuildR Response

<!-- markdownlint-enable MD013 -->

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Response)](https://www.nuget.org/packages/HttpBuildR.Response/)

Http BuildR Response is a simple set of C# functions for building responses using
only System.Net.Http!

Can be used on the testing side for building expected responses.

## Getting Started

To use this library, simply include `HttpBuildR.Response.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/HttpBuildR.Response/), and add
this to the top of each `.cs` file that needs it:

```C#
using HttpBuildR;
using Resp = HttpStatusCode;
```

Then get building!,

```c#
using HttpBuildR;

// its helpful to alias this for readability
using Resp = HttpStatusCode;

...

HttpRequestMessage request = 
  // start with a Http status code
  Resp.OK
     .Result() 
     // add some headers
     .WithHeader("x-custom-header", "a","b","c")
     // with some content, they are all supported!
     .WithJsonContent(new {Name = "John", Age = 36});
// now you can send it back!!!
...
```

For more details/information have a look the test project or create an issue.
