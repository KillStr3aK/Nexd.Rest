namespace Nexd.Rest
{
    public interface IHttpContent : IJsonObject
    {
        HttpContent GetContent()
        {
            return new StringContent(this.ToJSON());
        }
    }
}
