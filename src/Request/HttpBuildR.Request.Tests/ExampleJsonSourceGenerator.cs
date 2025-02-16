using System.Text.Json.Serialization;

namespace HttpBuildR.Tests;

public sealed record Widget(string Name, double Cost);

[JsonSerializable(typeof(Widget))]
public partial class ExampleJsonSourceGenerator : JsonSerializerContext;
