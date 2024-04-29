namespace Nexd.Rest
{
    using System.Net;
    using System.Text.Json;

    public class HttpRequest : IDisposable
    {
        private readonly string Url;

        private readonly HttpMethod Method = HttpMethod.Get;

        static HttpRequest()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public HttpRequest(string url)
        {
            this.Url = url;
        }

        public HttpRequest(string url, HttpMethod method) : this(url)
        {
            this.Method = method;
        }

        private T? SendInternal<T>(HttpClient client, HttpRequestMessage message, IHttpContent? body) where T : IJsonObject
        {
            if (body != null)
            {
                message.Content = body.GetContent();
            }

            using (HttpResponseMessage response = client.Send(message))
            {
                using (StreamReader reader = new StreamReader(response.Content.ReadAsStream()))
                {
                    response.EnsureSuccessStatusCode();
                    return JsonSerializer.Deserialize<T>(reader.ReadToEnd());
                }
            }
        }

        private void SendInternalNoResult(HttpClient client, HttpRequestMessage message, IHttpContent? body)
        {
            if (body != null)
            {
                message.Content = body.GetContent();
            }

            using (HttpResponseMessage response = client.Send(message))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        private async Task<T?> SendInternalAsync<T>(HttpClient client, HttpRequestMessage message, IHttpContent? body) where T : IJsonObject
        {
            if (body != null)
            {
                message.Content = body.GetContent();
            }

            using (HttpResponseMessage response = await client.SendAsync(message))
            {
                response.EnsureSuccessStatusCode();
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }
        }

        private async Task SendInternalNoResultAsync(HttpClient client, HttpRequestMessage message, IHttpContent? body)
        {
            if (body != null)
            {
                message.Content = body.GetContent();
            }

            using (HttpResponseMessage response = await client.SendAsync(message))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<T?> SendAsync<T>(HttpClient client, IHttpContent? body) where T : IJsonObject
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                return await this.SendInternalAsync<T>(client, message, body);
            }
        }

        public async Task<T?> SendAsync<T>(HttpClient client) where T : IJsonObject
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                return await this.SendInternalAsync<T>(client, message, null);
            }
        }

        public async Task<T?> SendAsync<T>(IHttpContent? body) where T : IJsonObject
        {
            using (HttpClient client = new HttpClient())
            {
                return await this.SendAsync<T>(client, body);
            }
        }

        public async Task<T?> SendAsync<T>() where T : IJsonObject
        {
            using (HttpClient client = new HttpClient())
            {
                return await this.SendAsync<T>(client);
            }
        }

        public async Task SendNoResultAsync(HttpClient client, IHttpContent? body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                await this.SendInternalNoResultAsync(client, message, body);
            }
        }

        public async Task SendNoResultAsync(HttpClient client)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                await this.SendInternalNoResultAsync(client, message, null);
            }
        }

        public async Task SendNoResultAsync(IHttpContent? body)
        {
            using (HttpClient client = new HttpClient())
            {
                await this.SendNoResultAsync(client, body);
            }
        }

        public async Task SendNoResultAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                await this.SendNoResultAsync(client);
            }
        }

        public T? Send<T>(HttpClient client, IHttpContent? body) where T : IJsonObject
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                return this.SendInternal<T>(client, message, body);
            }
        }

        public T? Send<T>(HttpClient client) where T : IJsonObject
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                return this.SendInternal<T>(client, message, null);
            }
        }

        public T? Send<T>(IHttpContent? body) where T : IJsonObject
        {
            using (HttpClient client = new HttpClient())
            {
                return this.Send<T>(client, body);
            }
        }

        public T? Send<T>() where T : IJsonObject
        {
            using (HttpClient client = new HttpClient())
            {
                return this.Send<T>(client, null);
            }
        }

        public void SendNoResult(HttpClient client, IHttpContent? body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                this.SendInternalNoResult(client, message, body);
            }
        }

        public void SendNoResult(HttpClient client)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(this.Method, this.Url))
            {
                this.SendInternalNoResult(client, message, null);
            }
        }

        public void SendNoResult(IHttpContent? body)
        {
            using (HttpClient client = new HttpClient())
            {
                this.SendNoResult(client, body);
            }
        }

        public void SendNoResult()
        {
            using (HttpClient client = new HttpClient())
            {
                this.SendNoResult(client, null);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
