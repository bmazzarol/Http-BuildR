using System.Net;
using System.Text.Json;
using BunsenBurner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace HttpBuildR.Tests;

public sealed class ActionResultBuilderTests
{
    private static async Task<HttpResponse> ConvertToResponse(ActionResult<string> ar)
    {
        var actionContext = new ActionContext(
            new DefaultHttpContext(),
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary()
        );
        await ((IConvertToActionResult)ar).Convert().ExecuteResultAsync(actionContext);
        return actionContext.HttpContext.Response;
    }

    [Fact(DisplayName = "A T can be converted to an OK response")]
    public void Case1()
    {
        var result = ActionResultBuilder.Ok("this is a test");
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "A T can be converted to an OK response and cookie")]
    public Task Case2() =>
        ActionResultBuilder
            .Ok("this is a test", Cookie.New("a", "c"))
            .Arrange()
            .Act(ConvertToResponse)
            .Assert(r => Assert.Equal((int)HttpStatusCode.OK, r.StatusCode))
            .And(r => Assert.Equal("a=c; path=/", r.Headers.SetCookie))
            .And(async r =>
            {
                var body = await new StreamReader(r.Body).ReadToEndAsync(
                    TestContext.Current.CancellationToken
                );
                Assert.Equal("\"this is a test\"", body);
            });

    [Fact(DisplayName = "A response can be converted to an action response")]
    public Task Case3() =>
        HttpStatusCode
            .BadRequest.Result()
            .WithProblemDetails(
                "a",
                "a",
                "a",
                "A",
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            )
            .ToActionResult<string>(Cookie.New("a", "b"))
            .Arrange()
            .Act(ConvertToResponse)
            .Assert(r => Assert.Equal((int)HttpStatusCode.BadRequest, r.StatusCode))
            .And(r => Assert.Equal("a=b; path=/", r.Headers.SetCookie))
            .And(async r =>
            {
                var body = await new StreamReader(r.Body).ReadToEndAsync(
                    TestContext.Current.CancellationToken
                );
                Assert.Equal(
                    "{\"type\":\"a\",\"title\":\"a\",\"status\":400,\"detail\":\"a\",\"instance\":\"A\"}",
                    body
                );
            });

    [Fact(DisplayName = "A response can be converted to an action response with no content")]
    public Task Case4() =>
        HttpStatusCode
            .NotAcceptable.Result()
            .WithHeader("a", "b")
            .ToActionResult<string>()
            .Arrange()
            .Act(ConvertToResponse)
            .Assert(r => Assert.Equal((int)HttpStatusCode.NotAcceptable, r.StatusCode))
            .And(r => Assert.Equal("b", r.Headers["a"]))
            .And(r => Assert.Null(r.ContentType))
            .And(r => Assert.Equal(0L, r.ContentLength));
}
