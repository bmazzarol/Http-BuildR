using FluentAssertions;
using Moq;
using static HttpBuildR.Examples.BlogsApi.RequestResponseSchema;

namespace HttpBuildR.Examples.BlogsApi;

public class BlogServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockedFactory;

    public BlogServiceTests()
    {
        _mockedFactory = new Mock<IHttpClientFactory>();
        _mockedFactory.Setup(x => x.CreateClient("blogs")).Returns(new HttpClient());
    }

    private static Scenario.Acted<BlogService, TResponse> ArrangeAndAct<TResponse>(
        Mock<IHttpClientFactory> mockedFactory,
        Func<BlogService, Task<TResponse>> fn
    )
    {
        var blogService = new BlogService(
            new BlobServiceConfig("https://jsonplaceholder.typicode.com", "blogs"),
            mockedFactory.Object
        );

        return blogService
            .ArrangeData()
            .Act(async service => await fn(service))
            .And((_, response) => response);
    }

    [Fact]
    public async Task GettingAllPosts()
    {
        await ArrangeAndAct(_mockedFactory, service => service.GetAllPostsAsync())
            .Assert(postsResponse =>
            {
                postsResponse.Posts.Count.Should().Be(100);
            });
    }

    [Fact]
    public async Task GetPost()
    {
        await ArrangeAndAct(
                _mockedFactory,
                service => service.GetPostAsync(new GetPostRequest(Id: 1))
            )
            .Assert(postResponse =>
            {
                postResponse.Post.Id.Should().Be(1);
            });
    }

    [Fact]
    public async Task CreatePost()
    {
        await ArrangeAndAct(
                _mockedFactory,
                service =>
                    service.CreatePostAsync(
                        new CreateBlogPostRequest("HttpBuildR", "This is cool!", 666)
                    )
            )
            .Assert(postResponse =>
            {
                postResponse.Post.Id.Should().Be(101);
            });
    }
}
