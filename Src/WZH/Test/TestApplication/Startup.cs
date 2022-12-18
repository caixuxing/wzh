using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WZH.Application.SyncData;
using WZH.Application.SyncData.Impl;
using WZH.Common.Extensions;

namespace TestApplication
{
   public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 服务注册
            services.AddHttpClient();
            services.RunModuleInitializers(AssemblyList());
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationServices"></param>
        public void Configure(IServiceProvider applicationServices)
        {
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Assembly> AssemblyList()
        {
            yield return Assembly.Load("WZH.Application");
        }
    }
}
