using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;
using System.Xml.Serialization;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

public static partial class Response
{
    /// <summary>
    /// Modifies the <see cref="HttpContent"/>
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithContent(
        this HttpResponseMessage response,
        HttpContent content
    )
    {
        response.Content = content;
        return response;
    }

    /// <summary>
    /// Modifies the <see cref="HttpResponseMessage"/> with json <see cref="StringContent"/>
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="options">optional <see cref="JsonSerializerOptions"/></param>
    /// <param name="mediaType">media type to use, default is `application/json`</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithJsonContent<T>(
        this HttpResponseMessage response,
        T content,
        JsonSerializerOptions? options = null,
        string mediaType = "application/json"
    )
        where T : notnull =>
        response.WithContent(
            new StringContent(JsonSerializer.Serialize(content, options), Encoding.UTF8, mediaType)
        );

    /// <summary>
    /// Modifies the <see cref="HttpResponseMessage"/> with json <see cref="StringContent"/>
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="typeInfo">json <see cref="JsonTypeInfo"/> for the T</param>
    /// <param name="mediaType">media type to use, default is `application/json`</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithJsonContent<T>(
        this HttpResponseMessage response,
        T content,
        JsonTypeInfo<T> typeInfo,
        string mediaType = "application/json"
    )
        where T : notnull =>
        response.WithContent(
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
    /// Modifies the <see cref="HttpResponseMessage"/> with XML <see cref="StringContent"/>
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="settings">optional settings</param>
    /// <param name="modifyWriterFunc">optional <see cref="Func{TResult}"/> that can be
    /// used to modify the <see cref="XmlWriter"/></param>
    /// <param name="mediaType">media type to use, default is `text/xml`</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithXmlContent<T>(
        this HttpResponseMessage response,
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
        return response.WithContent(
            new StringContent(stringWriter.ToString(), Encoding.UTF8, mediaType)
        );
    }

    /// <summary>
    /// Modifies the <see cref="HttpResponseMessage"/> with text <see cref="StringContent"/>
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="mediaTypeName">media type of the text content, defaults to text/plain</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithTextContent(
        this HttpResponseMessage response,
        string content,
        string? mediaTypeName = null
    ) =>
        response.WithContent(
            new StringContent(content, Encoding.UTF8, mediaTypeName ?? MediaTypeNames.Text.Plain)
        );

    /// <summary>
    /// Modifies the <see cref="HttpResponseMessage"/> with <see cref="FormUrlEncodedContent"/> content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithFormUrlContent(
        this HttpResponseMessage response,
        params KeyValuePair<string, string>[] content
    ) => response.WithContent(new FormUrlEncodedContent(content));

    /// <summary>
    /// Modifies the request with from url encoded content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <returns>response</returns>
    public static HttpResponseMessage WithFormUrlContent(
        this HttpResponseMessage response,
        IEnumerable<KeyValuePair<string, string>> content
    ) => response.WithContent(new FormUrlEncodedContent(content));
}
