namespace Blogs.Api.Models;

public record struct GetPostResponse(BlogPost Post);
public record GetAllPostsResponse(List<BlogPost> Posts);