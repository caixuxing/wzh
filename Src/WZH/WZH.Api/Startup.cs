using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WZH.Api.Filter;
using WZH.Common.Assemblys;
using WZH.Common.Config;
using WZH.Common.Extensions;
using WZH.Infrastructure.DbContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WZH.Common.utils;

namespace WZH.Api
{
    /// <summary>
    ///
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //关闭系统自带的模型验证过滤器
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            services.AddControllers(x =>
            {
                // 全局异常过滤
                x.Filters.Add(typeof(GlobalExceptionsFilterAttribute));
                //Action 参数提示FluentValidation过滤
                x.Filters.Add(typeof(ValidateModelActionFilter));
            }).AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //忽略Model中为null的属性
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                //处理null值
                options.SerializerSettings.ContractResolver = new NullOutputHandResolver();
                //设置本地时间而非UTC时间
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                //添加Enum转string
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.Load("WZH.Application"));
            services.AddFluentValidationClientsideAdapters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("wzh", new OpenApiInfo { Title = "无纸化", Version = "wzh" });
                c.SwaggerDoc("cj", new OpenApiInfo { Title = "数据采集", Version = "cj" });
                c.SwaggerDoc("interfaceServe", new OpenApiInfo { Title = "接口服务", Version = "interfaceServe" });
                c.SwaggerDoc("file", new OpenApiInfo { Title = "文件服务器", Version = "file" });
                c.SwaggerDoc("wechat", new OpenApiInfo { Title = "微信端接口", Version = "wechat" });
                c.SwaggerDoc("os", new OpenApiInfo { Title = "任务调度", Version = "os" });
                //增强swagger接口文档注释
                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //是否显示注释
                c.IncludeXmlComments(xmlPath, true);

                //这个就是Model层的xml文件名
                var xmlModelPath = Path.Combine(AppContext.BaseDirectory, "WZH.Application.xml");
                c.IncludeXmlComments(xmlModelPath);
            });

           // var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("DbConfig");

            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                 .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: false)
                .Build();
            services.AddOptions();
         //   services.Configure<AppSettingConfig>(configuration.GetSection("DbConfig"));

            services.Configure<DbConnectionOption>(configuration.GetSection("DbConfig")).AddScoped<SlaveRoundRobin>()
                    .AddDbContext<WzhDbContext>();

            // 服务注册
            services.AddHttpClient();
            services.RunModuleInitializers(AssemblyList());

            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
            services.RunModuleInitializers(assemblies.ToArray());
            services.AddMediatR(assemblies.ToArray());

            // 添加MiniProfiler服务
            services.AddMiniProfiler(options =>
            {
                // 设定访问分析结果URL的路由基地址
                options.RouteBasePath = "/profiler";
            }).AddEntityFramework(); //显示SQL语句及耗时


        


        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Assembly> AssemblyList()
        {
            yield return Assembly.Load("WZH.Application");
            yield return Assembly.Load("WZH.Infrastructure");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("WZH.Api.index.html");

                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/wzh/swagger.json", "无纸化");
                c.SwaggerEndpoint("/swagger/cj/swagger.json", "数据采集");
                c.SwaggerEndpoint("/swagger/interfaceServe/swagger.json", "接口服务");
                c.SwaggerEndpoint("/swagger/file/swagger.json", "文件服务器");
                c.SwaggerEndpoint("/swagger/wechat/swagger.json", "微信端接口");
                c.SwaggerEndpoint("/swagger/os/swagger.json", "任务调度");
            });
            app.UseMiniProfiler();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}