using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;
using System.Xml.Serialization;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

public static partial class Request
{
    /// <summary>
    /// Modifies the <see cref="HttpContent"/>
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithContent(
        this HttpRequestMessage request,
        HttpContent content
    )
    {
        request.Content = content;
        return request;
    }

    /// <summary>
    /// Modifies the <see cref="HttpRequestMessage"/> with json <see cref="StringContent"/>
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="options">optional <see cref="JsonSerializerOptions"/></param>
    /// <param name="mediaType">media type to use, default is `application/json`</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithJsonContent<T>(
        this HttpRequestMessage request,
        T content,
        JsonSerializerOptions? options = null,
        string mediaType = "application/json"
    )
        where T : notnull =>
        request.WithContent(
            new StringContent(JsonSerializer.Serialize(content, options), Encoding.UTF8, mediaType)
        );

    /// <summary>
    /// Modifies the <see cref="HttpRequestMessage"/> with json <see cref="StringContent"/>
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="typeInfo">json <see cref="JsonTypeInfo{T}"/> for the T</param>
    /// <param name="mediaType">media type to use, default is `application/json`</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithJsonContent<T>(
        this HttpRequestMessage request,
        T content,
        JsonTypeInfo<T> typeInfo,
        string mediaType = "application/json"
    )
        where T : notnull =>
        request.WithContent(
            new StringContent(JsonSerializer.Serialize(content, typeInfo), Encoding.UTF8, mediaType)
        );

    private sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    private static class XmlSerializerFactory<T>
    {
        public static readonly XmlSerializer Instance = new(typeof(T));
    }

    /// <summary>
    /// Modifies the <see cref="HttpRequestMessage"/> with XML <see cref="StringContent"/>
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="settings">optional settings</param>
    /// <param name="modifyWriterFunc">optional <see cref="Func{TResult}"/> that can be
    /// used to modify the <see cref="XmlWriter"/></param>
    /// <param name="mediaType">media type to use, default is `text/xml`</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithXmlContent<T>(
        this HttpRequestMessage request,
        T content,
        XmlWriterSettings? settings = null,
        Func<XmlWriter, XmlWriter>? modifyWriterFunc = null,
        string mediaType = "text/xml"
    )
        where T : class
    {
        using var stringWriter = new Utf8StringWriter();
        using var writer =
            modifyWriterFunc != null
                ? modifyWriterFunc(XmlWriter.Create(stringWriter, settings))
                : XmlWriter.Create(stringWriter, settings);
        XmlSerializerFactory<T>.Instance.Serialize(writer, content);
        return request.WithContent(
            new StringContent(stringWriter.ToString(), Encoding.UTF8, mediaType)
        );
    }

    /// <summary>
    /// Modifies the <see cref="HttpRequestMessage"/> with text <see cref="StringContent"/>
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="mediaTypeName">media type of the text content, defaults to text/plain</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithTextContent(
        this HttpRequestMessage request,
        string content,
        string? mediaTypeName = null
    ) =>
        request.WithContent(
            new StringContent(content, Encoding.UTF8, mediaTypeName ?? MediaTypeNames.Text.Plain)
        );

    /// <summary>
    /// Modifies the <see cref="HttpRequestMessage"/> with <see cref="FormUrlEncodedContent"/> content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithFormUrlContent(
        this HttpRequestMessage request,
        params KeyValuePair<string, string>[] content
    ) => request.WithContent(new FormUrlEncodedContent(content));

    /// <summary>
    /// Modifies the <see cref="HttpRequestMessage"/> with <see cref="FormUrlEncodedContent"/> content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <returns>request</returns>
    public static HttpRequestMessage WithFormUrlContent(
        this HttpRequestMessage request,
        IEnumerable<KeyValuePair<string, string>> content
    ) => request.WithContent(new FormUrlEncodedContent(content));
}
