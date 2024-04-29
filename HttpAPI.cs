namespace Nexd.Rest
{
    public class HttpAPI : IDisposable
    {
        public HttpClient Client { get; private set; } = new HttpClient(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        });

        protected string Url { get; private set; }

        public HttpAPI(string baseUrl)
        {
            this.Url = baseUrl;
            this.Client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<T?> SendRequestAsync<T>(string route) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return await request.SendAsync<T>(this.Client);
            }
        }

        public async Task SendRequestNoResultAsync(string route)
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                await request.SendNoResultAsync(this.Client);
            }
        }

        public T? SendRequest<T>(string route) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return request.Send<T>(this.Client);
            }
        }

        public void SendRequestNoResult(string route)
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                request.SendNoResult(this.Client);
            }
        }

        public async Task<T?> SendRequestAsync<T>(string route, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return await request.SendAsync<T>(this.Client, content);
            }
        }

        public async Task SendRequestNoResultAsync(string route, IHttpContent content)
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                await request.SendNoResultAsync(this.Client, content);
            }
        }

        public T? SendRequest<T>(string route, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                return request.Send<T>(this.Client, content);
            }
        }

        public void SendRequestNoResult(string route, IHttpContent content)
        {
            using (HttpRequest request = new HttpRequest(route, HttpMethod.Get))
            {
                request.SendNoResult(this.Client, content);
            }
        }

        public async Task<T?> SendRequestAsync<T>(string route, HttpMethod method) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return await request.SendAsync<T>(this.Client);
            }
        }

        public async Task SendRequestNoResultAsync(string route, HttpMethod method)
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                await request.SendNoResultAsync(this.Client);
            }
        }

        public T? SendRequest<T>(string route, HttpMethod method) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return request.Send<T>(this.Client);
            }
        }

        public void SendRequestNoResult<T>(string route, HttpMethod method)
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                request.SendNoResult(this.Client);
            }
        }

        public async Task<T?> SendRequestAsync<T>(string route, HttpMethod method, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return await request.SendAsync<T>(this.Client, content);
            }
        }

        public async Task SendRequestNoResultAsync(string route, HttpMethod method, IHttpContent content)
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                await request.SendNoResultAsync(this.Client, content);
            }
        }

        public T? SendRequest<T>(string route, HttpMethod method, IHttpContent content) where T : IJsonObject
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                return request.Send<T>(this.Client, content);
            }
        }

        public void SendRequestNoResult(string route, HttpMethod method, IHttpContent content)
        {
            using (HttpRequest request = new HttpRequest(route, method))
            {
                request.SendNoResult(this.Client, content);
            }
        }

        public void Dispose()
        {
            this.Client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
