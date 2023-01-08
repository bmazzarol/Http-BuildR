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
public static partial class Response
{
    /// <summary>
    /// Modifies the response content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithContent(
        this HttpResponseMessage response,
        HttpContent content
    ) => response.Modify(x => x.Content = content);

    /// <summary>
    /// Modifies the response with json content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="options">json serializer options</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithJsonContent<T>(
        this HttpResponseMessage response,
        T content,
        JsonSerializerOptions? options = null
    ) where T : notnull =>
        response.WithContent(
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

    private static string SerializeToXml<T>(
        this XmlSerializer serializer,
        T content,
        XmlWriterSettings? settings = null
    ) where T : notnull
    {
        using var stringWriter = new Utf8StringWriter();
        using var writer = XmlWriter.Create(stringWriter, settings);
        serializer.Serialize(writer, content);
        return stringWriter.ToString();
    }

    /// <summary>
    /// Modifies the response with xml content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="serializer">xml serializer to use</param>
    /// <param name="settings">optional settings</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithXmlContent<T>(
        this HttpResponseMessage response,
        T content,
        XmlSerializer serializer,
        XmlWriterSettings? settings = null
    ) where T : notnull =>
        response.WithContent(
            new StringContent(
                serializer.SerializeToXml(content, settings),
                Encoding.UTF8,
                MediaTypeNames.Text.Xml
            )
        );

    /// <summary>
    /// Modifies the response with xml content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="settings">optional settings</param>
    /// <param name="defaultNamespace">default namespace</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithXmlContent<T>(
        this HttpResponseMessage response,
        T content,
        XmlWriterSettings? settings = null,
        string? defaultNamespace = null
    ) where T : notnull =>
        response.WithXmlContent(content, new XmlSerializer(typeof(T), defaultNamespace), settings);

    /// <summary>
    /// Modifies the response with text content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <param name="mediaTypeName">media type of the text content, defaults to text/plain</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithTextContent(
        this HttpResponseMessage response,
        string content,
        string? mediaTypeName = null
    ) =>
        response.WithContent(
            new StringContent(content, Encoding.UTF8, mediaTypeName ?? MediaTypeNames.Text.Plain)
        );

    /// <summary>
    /// Modifies the response with from url encoded content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithFormUrlContent(
        this HttpResponseMessage response,
        params KeyValuePair<string, string>[] content
    ) => response.WithContent(new FormUrlEncodedContent(content));

    /// <summary>
    /// Modifies the response with from url encoded content
    /// </summary>
    /// <param name="response">response</param>
    /// <param name="content">response content</param>
    /// <returns>response</returns>
    [Pure]
    public static HttpResponseMessage WithFormUrlContent(
        this HttpResponseMessage response,
        IDictionary<string, string> content
    ) => response.WithFormUrlContent(content.AsEnumerable().ToArray());
}
