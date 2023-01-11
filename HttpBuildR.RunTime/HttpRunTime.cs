using System.Text;
using LanguageExt.Effects.Traits;

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

public readonly struct HttpRunTime : HasCancel<HttpRunTime>
{
    private readonly HttpRunTimeEnv _env;

    private HttpRunTime(HttpRunTimeEnv env)
    {
        _env = env;
    }

    public HttpRunTime LocalCancel =>
        new(
            _env with
            {
                Source = CancellationTokenSource.CreateLinkedTokenSource(_env.Source.Token)
            }
        );

    public CancellationToken CancellationToken => _env.Source.Token;
    public CancellationTokenSource CancellationTokenSource => _env.Source;
    public IServiceProvider ServiceProvider => _env.ServiceProvider;

    public static HttpRunTime New(IServiceProvider serviceProvider) =>
        new(HttpRunTimeEnv.New(serviceProvider));
}
