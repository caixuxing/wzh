

namespace WZH.Common.Https
{
    public class HttpWebClient
    {


        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpWebClient> _logger;
        public HttpWebClient(IHttpClientFactory httpClientFactory, ILogger<HttpWebClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<T?> GetAsync<T>(string url, Dictionary<string, string> dicHeaders, int timeoutSecond = 180)
        {

            var client = BuildHttpClient(dicHeaders, timeoutSecond);
            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestBody">请求体</param>
        /// <param name="dicHeaders">头</param>
        /// <param name="timeoutSecond">响应超时值</param>
        /// <returns></returns>
        public async Task<T?> PostAsync<T>(string url, string requestBody, Dictionary<string, string> dicHeaders, int timeoutSecond = 180)
            {


                var client = BuildHttpClient(null, timeoutSecond);
                var requestContent = GenerateStringContent(requestBody, dicHeaders);
                var response = await client.PostAsync(url, requestContent);
                _logger.LogInformation($"请求地址:{url}; 参数:{requestContent}");
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);

            }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestBody"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<T?> PutAsync<T>(string url, string requestBody, Dictionary<string, string> dicHeaders, int timeoutSecond = 180)
        {
           
                var client = BuildHttpClient(null, timeoutSecond);
                var requestContent = GenerateStringContent(requestBody, dicHeaders);
                var response = await client.PutAsync(url, requestContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);

        }

        /// <summary>
        /// Patch
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestString"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<T?> PatchAsync<T>(string url, string requestBody, Dictionary<string, string> dicHeaders, int timeoutSecond = 180)
        {

            var client = BuildHttpClient(null, timeoutSecond);
            var requestContent = GenerateStringContent(requestBody, dicHeaders);
            var response = await client.PatchAsync(url, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);

        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<T?> DeleteAsync<T>(string url, Dictionary<string, string> dicHeaders, int timeoutSecond = 180)
        {
            
                var client = BuildHttpClient(dicHeaders, timeoutSecond);
                var response = await client.DeleteAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
        
        }
        /// <summary>
        /// common request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="requestBody"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<T?> ExecuteAsync<T>(string url, HttpMethod method, string requestBody, Dictionary<string, string> dicHeaders, int timeoutSecond = 180)
        {
           
                var client = BuildHttpClient(null, timeoutSecond);
                var request = GenerateHttpRequestMessage(url, requestBody, method, dicHeaders);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
         

        }
        /// <summary>
        /// Build HttpClient
        /// </summary>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        private HttpClient BuildHttpClient(Dictionary<string, string>? dicDefaultHeaders, int? timeoutSecond)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();   //in order that the client is not affected by the last request,it need to clear DefaultRequestHeaders
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (dicDefaultHeaders != null)
            {
                foreach (var headerItem in dicDefaultHeaders)
                {
                    if (!httpClient.DefaultRequestHeaders.Contains(headerItem.Key))
                    {
                        httpClient.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                    }
                }
            }
            if (timeoutSecond != (int?)null)
            {
                httpClient.Timeout = TimeSpan.FromSeconds(timeoutSecond.Value);
            }
            return httpClient;
        }

        /// <summary>
        /// Generate HttpRequestMessage
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestBody"></param>
        /// <param name="method"></param>
        /// <param name="dicHeaders"></param>
        /// <returns></returns>
        private HttpRequestMessage GenerateHttpRequestMessage(string url, string requestBody, HttpMethod method, Dictionary<string, string> dicHeaders)
        {
            var request = new HttpRequestMessage(method, url);
            if (!string.IsNullOrEmpty(requestBody))
            {
                request.Content = new StringContent(requestBody);
            }
            if (dicHeaders != null)
            {
                foreach (var header in dicHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            return request;
        }
        /// <summary>
        ///  Generate StringContent
        /// </summary>
        /// <param name="requestBody"></param>
        /// <param name="dicHeaders"></param>
        /// <returns></returns>
        private StringContent GenerateStringContent(string requestBody, Dictionary<string, string> dicHeaders)
        {
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            if (dicHeaders != null)
            {
                foreach (var headerItem in dicHeaders)
                {
                    content.Headers.Add(headerItem.Key, headerItem.Value);
                }
            }
            return content;
        }

    }
}
