namespace Blogs.Api.Models;

public record struct CreateBlogPostRequest(string Title, string Content, int UserId);