using Newtonsoft.Json;

namespace PandoNexis.Accelerator.Extensions
{
    public class DecimalFormatConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => 
            writer.WriteRawValue(FormattableString.Invariant($"{value:0.00}"));
        
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) => 
            throw new NotImplementedException();

        public override bool CanConvert(Type objectType) => 
            objectType == typeof(decimal);
    }
}
