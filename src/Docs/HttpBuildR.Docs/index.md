# Http BuildR

![Http BuildR](images/construction-icon.png)

**Requests and responses, standard library only.**

A fluent builder for HttpRequestMessage and HttpResponseMessage built on System.Net.Http alone, with no dependencies and no wrapper types.

```shell
dotnet add package HttpBuildR.Request
```

## Why?

`HttpRequestMessage` and `HttpResponseMessage` are awkward to build by hand.
Headers, content and version each need their own line, and the result is a
block of setup code that says nothing about the request itself.

This library turns those assignments into a chain,

```c#
using HttpBuildR;

HttpRequestMessage request = HttpMethod.Post
    .To("https://api.example.com/widgets")
    .WithBearerToken(token)
    .WithAccept("application/json")
    .WithJsonContent(widget);
```

which is the same object a hand written version would produce,

```text
POST https://api.example.com/widgets HTTP/2.0
Authorization: Bearer <token>
Accept: application/json
Content-Type: application/json; charset=utf-8
```

Every method returns a `HttpRequestMessage`, so the chain can be passed,
reassigned or built in stages.

## What it does not do

* It does not send anything. A `HttpRequestMessage` is still handed to an
  `HttpClient`
* It does not add a client, retry policy, caching or middleware
* It does not replace `System.Net.Http`; it is extension methods on the types
  that already exist

## See also

* [Request Overview](articles/request/request-overview.md) - building a request
* [Request Headers](articles/request/request-headers.md) - every header helper
* [Response Overview](articles/response/response-overview.md) - the same pattern
  for responses, which is a separate `HttpBuildR.Response` package
