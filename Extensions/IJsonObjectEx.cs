namespace Nexd.Rest
{
    using System.Text.Json;

    public static class IJsonObjectEx
    {
        public static string ToJSON(this IJsonObject obj, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
