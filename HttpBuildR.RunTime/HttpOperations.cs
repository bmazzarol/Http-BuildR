using System.Net.Mime;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using static LanguageExt.Prelude;
using Req = System.Net.Http.HttpMethod;

namespace HttpBuildR.RunTime;

/// <summary>
/// The schema for supported HTTP operations through the runtime.
/// TODO: Write method overloads which supports different types of requests such as headers, and other options
/// </summary>
public static class HttpOperations
{
    private static readonly JsonSerializerSettings SerializerSettings =
        new() { Error = (_, args) => args.ErrorContext.Handled = true };

    public static Aff<HttpRunTime, TResponse> GetJson<TResponse>(
        string httpClientName,
        string url,
        JsonSerializerSettings? settings = null
    ) => SendJson<TResponse>(httpClientName, Req.Get.To(url), settings);

    public static Aff<HttpRunTime, TResponse> Post<TRequest, TResponse>(
        string httpClientName,
        string url,
        TRequest request,
        JsonSerializerSettings? settings = null
    ) where TRequest : notnull =>
        SendJson<TResponse>(
            httpClientName,
            Req.Get.To(url).WithAccept(MediaTypeNames.Application.Json).WithJsonData(request),
            settings
        );

    private static Aff<HttpRunTime, TResponse> SendJson<TResponse>(
        string httpClientName,
        HttpRequestMessage request,
        JsonSerializerSettings? settings = null
    ) =>
        from sp in Eff<HttpRunTime, IServiceProvider>(rt => rt.ServiceProvider)
        from fac in Eff(sp.GetRequiredService<IHttpClientFactory>)
        from client in Eff(() => fac.CreateClient(httpClientName))
        from response in Aff(async () => await client.SendAsync(request))
        from data in Aff(async () =>
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(
                content,
                settings ?? SerializerSettings
            );
        })
        select data;
}
