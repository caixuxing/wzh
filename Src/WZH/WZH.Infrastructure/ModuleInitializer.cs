using WZH.Application.Borrow;
using WZH.Common.MemoryCache;
using WZH.Infrastructure.Service.query;

namespace WZH.Infrastructure
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
            services.AddScoped<IBorrowRepo, BorrowRepo>();
            services.AddScoped<IBorrowQueryApp, BorrowQueryApp>();
            services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
            services.AddMemoryCache();
        }
    }
}