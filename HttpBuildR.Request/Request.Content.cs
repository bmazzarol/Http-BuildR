using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

/// <summary>
/// Extension methods for modifying content
/// </summary>
public static partial class Request
{
    /// <summary>
    /// Modifies the request content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithContent(
        this HttpRequestMessage request,
        HttpContent content
    ) => request.Modify(x => x.Content = content);

    /// <summary>
    /// Modifies the request with json content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="options">json serializer options</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithJsonContent<T>(
        this HttpRequestMessage request,
        T content,
        JsonSerializerOptions? options = null
    )
        where T : notnull =>
        request.WithContent(
            new StringContent(
                JsonSerializer.Serialize(content, options),
                Encoding.UTF8,
                "application/json"
            )
        );

    private sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    [Pure]
    private static string SerializeToXml<T>(
        this XmlSerializer serializer,
        T content,
        XmlWriterSettings? settings = null
    )
        where T : notnull
    {
        using var stringWriter = new Utf8StringWriter();
        using var writer = XmlWriter.Create(stringWriter, settings);
        serializer.Serialize(writer, content);
        return stringWriter.ToString();
    }

    /// <summary>
    /// Modifies the request with xml content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="serializer">xml serializer to use</param>
    /// <param name="settings">optional settings</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithXmlContent<T>(
        this HttpRequestMessage request,
        T content,
        XmlSerializer serializer,
        XmlWriterSettings? settings = null
    )
        where T : notnull =>
        request.WithContent(
            new StringContent(
                serializer.SerializeToXml(content, settings),
                Encoding.UTF8,
                MediaTypeNames.Text.Xml
            )
        );

    /// <summary>
    /// Modifies the request with xml content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="settings">optional settings</param>
    /// <param name="defaultNamespace">default namespace</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithXmlContent<T>(
        this HttpRequestMessage request,
        T content,
        XmlWriterSettings? settings = null,
        string? defaultNamespace = null
    )
        where T : notnull =>
        request.WithXmlContent(content, new XmlSerializer(typeof(T), defaultNamespace), settings);

    /// <summary>
    /// Modifies the request with text content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <param name="mediaTypeName">media type of the text content, defaults to text/plain</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithTextContent(
        this HttpRequestMessage request,
        string content,
        string? mediaTypeName = null
    ) =>
        request.WithContent(
            new StringContent(content, Encoding.UTF8, mediaTypeName ?? MediaTypeNames.Text.Plain)
        );

    /// <summary>
    /// Modifies the request with from url encoded content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithFormUrlContent(
        this HttpRequestMessage request,
        params KeyValuePair<string, string>[] content
    ) => request.WithContent(new FormUrlEncodedContent(content));

    /// <summary>
    /// Modifies the request with from url encoded content
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="content">request content</param>
    /// <returns>request</returns>
    [Pure]
    public static HttpRequestMessage WithFormUrlContent(
        this HttpRequestMessage request,
        IDictionary<string, string> content
    ) => request.WithFormUrlContent(content.AsEnumerable().ToArray());
}
