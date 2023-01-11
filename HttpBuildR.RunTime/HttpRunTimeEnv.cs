using System.Text;

namespace HttpBuildR.RunTime;

internal record struct HttpRunTimeEnv(
    IServiceProvider ServiceProvider,
    Encoding Encoding,
    CancellationTokenSource Source
)
{
    private HttpRunTimeEnv(IServiceProvider serviceProvider)
        : this(serviceProvider, Encoding.Default, new CancellationTokenSource()) { }

    public static HttpRunTimeEnv New(IServiceProvider serviceProvider) => new(serviceProvider);
}