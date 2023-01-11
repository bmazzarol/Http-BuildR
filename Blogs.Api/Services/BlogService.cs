using Blogs.Api.Configurations;
using Blogs.Api.Models;
using HttpBuildR.RunTime;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using Req = System.Net.Http.HttpMethod;

namespace Blogs.Api.Services;

public class BlogService : IBlogService
{
    private readonly HttpRunTime _runTime;
    private readonly BlogConfig _config;
    private readonly ILogger<BlogService> _logger;

    public BlogService(HttpRunTime runTime, BlogConfig config, ILogger<BlogService> logger)
    {
        _runTime = runTime;
        _config = config;
        _logger = logger;
    }

    public async Task<Either<Error, GetAllPostsResponse>> GetAllPostsAsync() =>
        (
            await HttpOperations
                .GetJson<List<BlogPost>>(_config.Name, $"{_config.BaseUrl}/posts")
                .Run(_runTime)
        ).Match(
            posts =>
            {
                _logger.LogInformation("all posts returned successfully");
                return Right<Error, GetAllPostsResponse>(new GetAllPostsResponse(posts));
            },
            error =>
            {
                _logger.LogError(error.ToException(), "cannot get all posts");
                return Left<Error, GetAllPostsResponse>(error);
            }
        );

    public async Task<Either<Error, GetPostResponse>> CreatePostAsync(
        CreateBlogPostRequest request
    ) =>
        (
            await HttpOperations
                .Post<CreateBlogPostRequest, BlogPost>(
                    _config.Name,
                    $"{_config.BaseUrl}/posts",
                    request
                )
                .Run(_runTime)
        ).Match(
            response =>
            {
                _logger.LogInformation("blog post created successfully");
                return Right<Error, GetPostResponse>(new GetPostResponse(response));
            },
            error =>
            {
                _logger.LogError("unable to create the blog post");

                return error switch
                {
                    HttpRunTimeError httpRunTimeError => Left<Error, GetPostResponse>(httpRunTimeError),
                    _ => throw error.ToException()
                };
            }
        );
}
