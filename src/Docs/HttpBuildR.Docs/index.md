# Http BuildR

![Http BuildR](images/construction-icon.png)

**Requests and responses, without the framework.**

A fluent builder for HttpRequestMessage and HttpResponseMessage built on System.Net.Http alone, with no dependencies and no extra allocations.

```shell
dotnet add package HttpBuildR.Request
```

## Why?

There are a ton of http client libraries out there, but nothing (that I liked)
that was just simple extensions to the core System.Net.Http classes.

I want the request and response building code to be,

* Declarative, it needs to flow and read as well as possible
* Simple, usage should be trivial and learning curve as flat as possible
* Complete, anything you can do with the core class can be done in a fluent
  declarative style
