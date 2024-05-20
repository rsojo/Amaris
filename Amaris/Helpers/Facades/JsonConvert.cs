namespace Amaris.Helpers.Facades
{
    public class JsonConvert
    {
        public static string SerializeObject(object value)
        {
            return System.Text.Json.JsonSerializer.Serialize(value);
        }

        public static T DeserializeObject<T>(string value)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}
