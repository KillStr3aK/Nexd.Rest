namespace Nexd.Rest
{
    using Newtonsoft.Json;

    public interface IJsonObject
    {
        public string ToJSON(Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(this, formatting);
        }
    }
}
