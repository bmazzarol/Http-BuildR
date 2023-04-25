using System.Net.Mime;
using System.Text.Json;
using Newtonsoft.Json;

namespace HttpBuildR.Examples;

public static class HttpBuildRExtensions
{
    public static readonly Func<JsonSerializerSettings> DefaultSerializerSettings = () =>
        new JsonSerializerSettings { Error = (_, args) => args.ErrorContext.Handled = true };

    public static HttpRequestMessage WithJsonData<TData>(
        this HttpRequestMessage request,
        TData data,
        JsonSerializerOptions? options = null
    )
        where TData : notnull =>
        request
            .WithAccept(MediaTypeNames.Application.Json)
            .WithJsonContent(data, options ?? JsonSerializerOptions.Default);

    public static async Task<TData> ToModelAsync<TData>(
        this HttpResponseMessage responseMessage,
        TData errorModel,
        Func<JsonSerializerSettings> settings
    )
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return errorModel;
        }

        var data = JsonConvert.DeserializeObject<TData>(responseContent, settings());
        return data!;
    }
}
