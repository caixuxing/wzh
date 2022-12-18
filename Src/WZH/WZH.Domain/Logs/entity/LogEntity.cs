using WZH.Domain.Base;

namespace WZH.Domain.Logs.entity
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public sealed record LogEntity : AggregateRootEntity
    {
        /// <summary>
        ///
        /// </summary>
        private LogEntity() { }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; init; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public string LogMsg { get; set; }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <returns></returns>
        public static LogEntity Create(string logtype)
        {
            LogEntity entity = new LogEntity()
            {
                LogType = logtype
            };
            return entity;
        }
    }
}