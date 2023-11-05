namespace Nexd.Rest
{
    public class HttpAPI : IDisposable
    {
        public HttpClient Client { get; private set; } = new HttpClient();

        public HttpAPI(string baseUrl)
        {
            this.Client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<T?> SendRequestAsync<T>(string route) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return await request.SendAsync<T>(this.Client);
            }    
        }

        public T? SendRequest<T>(string route) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return request.Send<T>(this.Client);
            }
        }

        public async Task<T?> SendRequestAsync<T>(string route, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return await request.SendAsync<T>(this.Client, content);
            }
        }

        public T? SendRequest<T>(string route, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return request.Send<T>(this.Client, content);
            }
        }

        public async Task<T?> SendRequestAsync<T>(string route, HttpMethod method) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return await request.SendAsync<T>(this.Client);
            }
        }

        public T? SendRequest<T>(string route, HttpMethod method) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return request.Send<T>(this.Client);
            }
        }

        public async Task<T?> SendRequestAsync<T>(string route, HttpMethod method, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return await request.SendAsync<T>(this.Client, content);
            }
        }

        public T? SendRequest<T>(string route, HttpMethod method, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return request.Send<T>(this.Client, content);
            }
        }

        public void Dispose()
        {
            this.Client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
