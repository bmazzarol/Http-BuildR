using System.Net.Mime;
using System.Text.Json;

namespace HttpBuildR.Examples;

public static class HttpBuildRExtensions
{
    public static HttpRequestMessage WithJsonData<TData>(
        this HttpRequestMessage request,
        TData data,
        JsonSerializerOptions? options = null
    ) where TData : notnull =>
        request
            .WithAccept(MediaTypeNames.Application.Json)
            .WithJsonContent(data, options ?? JsonSerializerOptions.Default);
}