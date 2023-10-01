using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace HttpBuildR;

/// <summary>
/// Defines a response cookie
/// </summary>
public readonly struct Cookie
{
    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Cookie options
    /// </summary>
    public CookieOptions Options { get; }

    private Cookie(string key, string value, CookieOptions options)
    {
        Key = key;
        Value = value;
        Options = options;
    }

    /// <summary>
    /// Creates a new cookie
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">value</param>
    /// <param name="options">options</param>
    /// <returns>cookie</returns>
    public static Cookie New(string key, string value, CookieOptions? options = null) =>
        new(key, value, options ?? new CookieOptions());
}
