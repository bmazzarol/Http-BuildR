<!-- markdownlint-disable MD013 -->

# ![HTTP BuildR ActionResult](https://raw.githubusercontent.com/bmazzarol/Http-BuildR/main/construction-icon-small.png) HTTP BuildR ActionResult

<!-- markdownlint-enable MD013 -->

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.ActionResult)](https://www.nuget.org/packages/HttpBuildR.ActionResult/)

Http BuildR ActionResult is a simple set of C# functions for building responses
using ActionResult!

They are always typed (unlike the static action result builders in
BaseController) and easy to build using the existing fluent API on-top
HttpResponseMessage, with some added methods that make it easy on the happy and
unhappy path.

## Getting Started

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

For more details/information have a look the test project or create an issue.
