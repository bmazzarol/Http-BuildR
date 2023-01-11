using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace HttpBuildR.RunTime;

public interface IHttpBuilderRunTime
{
    internal IServiceCollection Services { get; }
}

internal class Bootstrapper : IHttpBuilderRunTime
{
    public readonly IServiceCollection _services;

    private Bootstrapper(IServiceCollection services)
    {
        _services = services;
    }

    public static Bootstrapper New(IServiceCollection services) => new(services);

    public IServiceCollection Services => _services;
}

public static class BootstrapperExtensions
{
    public static IHttpBuilderRunTime RegisterHttpRunTime(this IServiceCollection services)
    {
        services.AddSingleton(typeof(HttpRunTime), provider => HttpRunTime.New(provider));

        return Bootstrapper.New(services);
    }

    public static IServiceCollection WithHttpClient(
        this IHttpBuilderRunTime runTime,
        string httpClientName,
        Func<IServiceProvider, HttpClient>? configuration = null
    )
    {
        if (configuration == null)
        {
            runTime.Services.AddHttpClient(httpClientName);
            return runTime.Services;
        }

        runTime.Services.AddHttpClient(httpClientName, (provider, _) => configuration(provider));
        return runTime.Services;
    }
}
