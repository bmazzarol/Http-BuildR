using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace HttpBuildR.ActionResult.Tests;

public static class ActionResultBuilderTests
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
    public static void Case1() => ActionResultBuilder.Ok("this is a test").Should().NotBeNull();

    [Fact(DisplayName = "A T can be converted to an OK response and cookie")]
    public static async Task Case2() =>
        await ActionResultBuilder
            .Ok("this is a test", Cookie.New("a", "c"))
            .ArrangeData()
            .Act(ConvertToResponse)
            .Assert(r => r.StatusCode.Should().Be((int)HttpStatusCode.OK))
            .And(r => r.Headers.Should().ContainKey("Set-Cookie").And.ContainValue("a=c; path=/"))
            .And(async r =>
                (await new StreamReader(r.Body).ReadToEndAsync()).Should().Be("\"this is a test\"")
            );

    [Fact(DisplayName = "A response can be converted to an action response")]
    public static async Task Case3() =>
        await Resp
            .BadRequest.Result()
            .WithProblemDetails(
                "a",
                "a",
                "a",
                "A",
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            )
            .ToActionResult<string>(Cookie.New("a", "b"))
            .ArrangeData()
            .Act(ConvertToResponse)
            .Assert(r => r.StatusCode.Should().Be((int)HttpStatusCode.BadRequest))
            .And(r => r.Headers.Should().ContainKey("Set-Cookie").And.ContainValue("a=b; path=/"))
            .And(async r =>
                (await new StreamReader(r.Body).ReadToEndAsync())
                    .Should()
                    .Be(
                        "{\"type\":\"a\",\"title\":\"a\",\"status\":400,\"detail\":\"a\",\"instance\":\"A\"}"
                    )
            );

    [Fact(DisplayName = "A response can be converted to an action response with no content")]
    public static async Task Case4() =>
        await Resp
            .NotAcceptable.Result()
            .WithHeader("a", "b")
            .ToActionResult<string>()
            .ArrangeData()
            .Act(ConvertToResponse)
            .Assert(r => r.StatusCode.Should().Be((int)HttpStatusCode.NotAcceptable))
            .And(r => r.Headers.Should().ContainKey("a").And.ContainValue("b"))
            .And(r => r.ContentType.Should().BeNullOrEmpty())
            .And(r => r.ContentLength.Should().Be(0L));
}
