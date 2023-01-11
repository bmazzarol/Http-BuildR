namespace Blogs.Api.Models;

public record BlogPost(int Id, string Title, string Body, int UserId);

public record GetAllPostsResponse(List<BlogPost> Posts);