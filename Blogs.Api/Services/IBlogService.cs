using Blogs.Api.Models;
using LanguageExt;
using LanguageExt.Common;

namespace Blogs.Api.Services;

public interface IBlogService
{
    Task<Either<Error, GetAllPostsResponse>> GetAllPostsAsync();
    Task<Either<Error, GetPostResponse>> CreatePostAsync(CreateBlogPostRequest request);
}