namespace HttpBuildR.Examples.BlogsApi;

public interface IBlogService
{
    Task<RequestResponseSchema.GetAllPostsResponse> GetAllPostsAsync();
    Task<RequestResponseSchema.GetPostResponse> GetPostAsync(RequestResponseSchema.GetPostRequest request);
    Task<RequestResponseSchema.GetPostResponse> CreatePostAsync(RequestResponseSchema.CreateBlogPostRequest request);
}