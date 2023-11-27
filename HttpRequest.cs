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
                T? result = default(T);

                Stream responseStream = response.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string responseString = streamReader.ReadToEnd();

                if (response.IsSuccessStatusCode)
                {
                    result = JsonSerializer.Deserialize<T>(responseString);
                } else
                {
                    this.WrongStatusCode(response.StatusCode, responseString);
                }

                return result;
            }
        }

        private async Task<T?> SendInternalAsync<T>(HttpClient client, HttpRequestMessage message, IHttpContent? body) where T: IJsonObject
        {
            if (body != null)
            {
                message.Content = body.GetContent();
            }

            using (HttpResponseMessage response = await client.SendAsync(message))
            {
                T? result = default(T);

                if (response.IsSuccessStatusCode)
                {
                    result = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
                } else
                {
                    this.WrongStatusCode(response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return result;
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
                return await this.SendAsync<T>(client, null);
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

        private void WrongStatusCode(HttpStatusCode statusCode, string responseBody)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    {
                        Console.WriteLine($"REST API: '{this.Url}' => {statusCode}");
                    } break;
                default:
                    {
                        Console.WriteLine($"REST API: '{this.Url} => {statusCode}\nResponse Body:{responseBody}");
                    } break;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
