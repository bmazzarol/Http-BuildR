using System.Net.Mime;
using Newtonsoft.Json;
using static HttpBuildR.Examples.BlogsApi.RequestResponseSchema;
using static HttpBuildR.Examples.HttpBuildRExtensions;

namespace HttpBuildR.Examples.BlogsApi;

using Req = HttpMethod;

public class BlogService : IBlogService
{
    private readonly HttpClient _client;
    private readonly BlobServiceConfig _config;

    public BlogService(BlobServiceConfig config, IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(config.Name);
        _config = config;
    }

    public async Task<GetAllPostsResponse> GetAllPostsAsync()
    {
        var posts = await (
            await _client.SendAsync(
                Req.Get.To($"{_config.BaseUrl}/posts").WithAccept(MediaTypeNames.Application.Json)
            )
        ).ToModelAsync(new List<BlogPost>(), DefaultSerializerSettings);
        return new GetAllPostsResponse(posts);
    }

    public async Task<GetPostResponse> GetPostAsync(GetPostRequest request)
    {
        var post = await (
            await _client.SendAsync(
                Req.Get
                    .To($"{_config.BaseUrl}/posts/{request.Id}")
                    .WithAccept(MediaTypeNames.Application.Json)
            )
        ).ToModelAsync(new BlogPost(), DefaultSerializerSettings);
        return new GetPostResponse(Post: post);
    }

    public async Task<GetPostResponse> CreatePostAsync(CreateBlogPostRequest request)
    {
        var post = await (
            await _client.SendAsync(
                Req.Post
                    .To($"{_config.BaseUrl}/posts")
                    .WithJsonData(new { request.Title, Body = request.Content, request.UserId })
            )
        ).ToModelAsync(new BlogPost(), DefaultSerializerSettings);
        return new GetPostResponse(post);
    }
}
