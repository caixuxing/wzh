

using WZH.Application.Borrow;
using WZH.Application.Borrow.Impl;
using WZH.Application.SyncData;
using WZH.Application.SyncData.Impl;

namespace WZH.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Initialize(IServiceCollection services)
        {
            services.AddTransient<HttpWebClient>();
            services.AddScoped<IBorrowCmdApp, BorrowCmdApp>();
            services.AddScoped<ISyncDataAppCmd, SyncDataAppCmdImpl>();
        }
    }
}
