using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResultExtensions;

internal sealed class SuccessJsonConverter : JsonConverter<Success>
{
    public override Success Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
        Success.Value;

    public override void Write(Utf8JsonWriter writer, Success value, JsonSerializerOptions options)
    {
    }
}