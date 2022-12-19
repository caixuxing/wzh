

namespace WZH.Common.Config
{
  public  class DbConfig
    {
        /// <summary>
        /// 主库
        /// </summary>
        public string MasterConnection { get; set; } = string.Empty;

        /// <summary>
        /// 从库
        /// </summary>
        public List<string> SlaveConnections { get; set; } = Array.Empty<string>().ToList();
    }
}
