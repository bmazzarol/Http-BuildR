using System.Net.Mime;
using Newtonsoft.Json;
using static HttpBuildR.Examples.BlogsApi.RequestResponseSchema;

namespace HttpBuildR.Examples.BlogsApi;

using Req = HttpMethod;

public interface IBlogService
{
    Task<GetAllPostsResponse> GetAllPostsAsync();
    Task<GetPostResponse> GetPostAsync(GetPostRequest request);
    Task<GetPostResponse> CreatePostAsync(CreateBlogPostRequest request);
}

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
        var httpRequest = Req.Get
            .To($"{_config.BaseUrl}/posts")
            .WithAccept(MediaTypeNames.Application.Json);

        var httpResponse = await _client.SendAsync(httpRequest);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        var blogPosts = JsonConvert.DeserializeObject<List<BlogPost>>(responseContent);
        return new GetAllPostsResponse(blogPosts);
    }

    public async Task<GetPostResponse> GetPostAsync(GetPostRequest request)
    {
        var httpRequest = Req.Get
            .To($"{_config.BaseUrl}/posts/{request.Id}")
            .WithAccept(MediaTypeNames.Application.Json);

        var httpResponse = await _client.SendAsync(httpRequest);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        var post = JsonConvert.DeserializeObject<BlogPost>(responseContent);
        return new GetPostResponse(post);
    }

    public async Task<GetPostResponse> CreatePostAsync(CreateBlogPostRequest request)
    {
        var httpRequest = Req.Post
            .To($"{_config.BaseUrl}/posts")
            .WithJsonData(new { request.Title, Body = request.Content, request.UserId });

        var httpResponse = await _client.SendAsync(httpRequest);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        var post = JsonConvert.DeserializeObject<BlogPost>(responseContent);
        return new GetPostResponse(post);
    }
}
