
using WZH.Common.Response;

namespace WZH.Application.SyncData.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class SyncDataAppCmdImpl : ISyncDataAppCmd
    {
        private readonly HttpWebClient _httpWebClient;
        public SyncDataAppCmdImpl(HttpWebClient httpWebClient)
        {
            _httpWebClient = httpWebClient;
        }

        public async Task<MessageModel<string>> Dept()
        {
            var data = string.Empty;//await _httpWebClient.PostAsync<string>("http://www.baidu.com", string.Empty, null, 30);
            Console.WriteLine($"{data}");
            await Task.CompletedTask;
            return new()
            {
                msg = "",
                response = data
            };

        }
    }
}
