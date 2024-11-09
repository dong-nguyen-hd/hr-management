using Newtonsoft.Json;
using System.Globalization;

namespace Business.Extensions;

public class DateTimeConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
            return objectType == typeof(DateTime);
        }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
            return DateTime.ParseExact((string)reader.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
            DateTime d = (DateTime)value;
            writer.WriteValue(d.ToString("yyyy-MM-dd"));
        }
}