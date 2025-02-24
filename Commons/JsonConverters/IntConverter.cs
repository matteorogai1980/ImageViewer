using System.Text.Json;
using System.Text.Json.Serialization;

namespace Commons.JsonConverters;

public class IntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            var intValue = reader.GetInt32();
            return intValue;
        }
        catch (Exception)
        {
            var value = reader.GetString();
            if (value != null) return int.Parse(value);
            return 0;
        }
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}