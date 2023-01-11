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
    private readonly ILogger<BlogService> _logger;

    public BlogService(HttpRunTime runTime, ILogger<BlogService> logger)
    {
        _runTime = runTime;
        _logger = logger;
    }

    public async Task<Either<Error, GetAllPostsResponse>> GetAllPostsAsync() =>
        (
            await HttpOperations
                .GetJson<List<BlogPost>>("BlogService", "https://jsonplaceholder.typicode.com/posts")
                .Run(_runTime)
        ).Match(
            posts => Right<Error, GetAllPostsResponse>(new GetAllPostsResponse(posts)),
            error =>
            {
                _logger.LogError(error.ToException(), "cannot get all posts");
                return Left<Error, GetAllPostsResponse>(error);
            }
        );
}
