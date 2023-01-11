using System.Net;
using Blogs.Api.Models;
using Blogs.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Req = System.Net.Http.HttpMethod;

namespace Blogs.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogsController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly ILogger<BlogsController> _logger;

    public BlogsController(IBlogService blogService, ILogger<BlogsController> logger)
    {
        _blogService = blogService;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllPosts")]
    public async Task<IActionResult> GetAllPosts()
    {
        var operation = await _blogService.GetAllPostsAsync();

        return operation.Match<IActionResult>(
            Ok,
            error =>
            {
                _logger.LogError(error.ToException(), "cannot get all posts");
                return new ObjectResult(error)
                {
                    StatusCode = (int)(HttpStatusCode.InternalServerError)
                };
            }
        );
    }

    [HttpPost("CreateBlogPost")]
    public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequest request)
    {
        var operation = await _blogService.CreatePostAsync(request);
        return operation.Match<IActionResult>(
            response => Ok(response),
            error =>
                new ObjectResult(error) { StatusCode = (int)(HttpStatusCode.InternalServerError) }
        );
    }
}
