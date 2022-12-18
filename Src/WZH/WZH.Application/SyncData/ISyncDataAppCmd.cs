
using WZH.Common.Response;

namespace WZH.Application.SyncData
{
   public interface ISyncDataAppCmd
    {
        /// <summary>
        /// 同步部门信息
        /// </summary>
        /// <returns></returns>
        Task<MessageModel<string>> Dept();
    }
}
