# Getting Started

## Request

To use this library, simply include `HttpBuildR.Request.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/HttpBuildR.Request/), and add
this to the top of each `.cs` file that needs it:

```C#
using HttpBuildR;
using Req = HttpMethod;
```

Then get building!,

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

## Response

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

## Action Result

To use this library, simply include `HttpBuildR.ActionResult.dll` in your
project or grab it
from [NuGet](https://www.nuget.org/packages/HttpBuildR.ActionResult/), and add
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
// all types can be converted to ok responses
ActionResult<string> result = ActionResultBuilder.Ok("some content");
// non-ok responses can be built from response status codes
ActionResult<string> result = Resp.BadRequest
                 .Result()
                  // add some headers
                 .WithHeader("x-custom-header", "a","b","c")
                  // with some content, they are all supported!
                 .WithProblemDetails(new ProblemDetails(){...})
                 // only typed action results are supported
                 .ToActionResult<string>();
...
```
