using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using LanguageExt;
using static LanguageExt.Prelude;
using Req = System.Net.Http.HttpMethod;

namespace HttpBuildR.RunTime;

/// <summary>
/// The schema for supported HTTP operations through the runtime.
/// TODO: Write method overloads which supports different types of requests such as headers, and other options
/// </summary>
public static class HttpOperations
{
    private const int NoHttpFactory = 500;
    private const int UnregisteredClient = 501;
    private const int InvalidEndpoint = 502;
    private const int FailedApiCall = 503;
    private const int IncompatibleResponse = 504;

    public static Aff<HttpRunTime, TResponse> GetJson<TResponse>(
        string httpClientName,
        string url,
        JsonSerializerSettings? settings = null
    ) where TResponse : notnull => SendJson<TResponse>(httpClientName, Req.Get.To(url), settings);

    public static Aff<HttpRunTime, TResponse> Post<TRequest, TResponse>(
        string httpClientName,
        string url,
        TRequest request,
        JsonSerializerSettings? settings = null
    )
        where TRequest : notnull
        where TResponse : notnull =>
        SendJson<TResponse>(httpClientName, Req.Post.To(url).WithJsonData(request), settings);

    private static Eff<HttpRunTime, IHttpBuilderRunTime> GetBootstrapper() =>
        EffMaybe<HttpRunTime, IHttpBuilderRunTime>(_ => Bootstrapper.New(new ServiceCollection()))
            .MapFail(error => error);

    private static Aff<HttpRunTime, TResponse> SendJson<TResponse>(
        string httpClientName,
        HttpRequestMessage request,
        JsonSerializerSettings? settings = null
    ) where TResponse : notnull =>
        from sp in Eff<HttpRunTime, IServiceProvider>(rt => rt.ServiceProvider)
        from fac in EffMaybe<IHttpClientFactory>(
                () => FinSucc(sp.GetRequiredService<IHttpClientFactory>())
            )
            .MapFail(error => NoHttpFactory.ToHttpRunTimeError("no factory", error.ToException()))
        from client in EffMaybe<HttpClient>(() => fac.CreateClient(httpClientName))
            .MapFail(
                error => UnregisteredClient.ToHttpRunTimeError("no httpclient", error.ToException())
            )
        from response in AffMaybe<HttpResponseMessage>(async () => await client.SendAsync(request))
            .MapFail(
                error => InvalidEndpoint.ToHttpRunTimeError("invalid endpoint", error.ToException())
            )
        from data in response.IsSuccessStatusCode
            ? AffMaybe<TResponse>(async () =>
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<TResponse>(content, settings);

                    return model;
                })
                .MapFail(
                    error =>
                        IncompatibleResponse.ToHttpRunTimeError("invalid data", error.ToException())
                )
            : FailAff<TResponse>(
                FailedApiCall.ToHttpRunTimeError(
                    response.ReasonPhrase ?? $"api returned {response.ReasonPhrase}"
                )
            )
        select data;
}
