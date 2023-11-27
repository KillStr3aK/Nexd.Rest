namespace Nexd.Rest
{
    using System.Text.Json;

    public interface IJsonObject
    {
        public string ToJSON(JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(this, options);
        }
    }
}
