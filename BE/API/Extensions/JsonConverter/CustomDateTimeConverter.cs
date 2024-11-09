using System.Globalization;

namespace API.Extensions.JsonConverter;

public sealed class CustomDateTimeConverter(string format) : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var isValid = DateTime.TryParseExact(reader.GetString(), format, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date);
        return isValid ? date : DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}