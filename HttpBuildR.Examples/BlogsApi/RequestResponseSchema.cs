namespace HttpBuildR.Examples.BlogsApi;

public static class RequestResponseSchema
{
    public record struct BlobServiceConfig(string BaseUrl, string Name);
    public record struct BlogPost(int Id, string Title, string Body, int UserId);
    public record struct GetPostRequest(int Id);

    public record struct CreateBlogPostRequest(string Title, string Content, int UserId);

    public record struct GetAllPostsResponse(List<BlogPost> Posts);

    public record struct GetPostResponse(BlogPost Post);
}
