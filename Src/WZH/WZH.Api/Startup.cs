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
            //�ر�ϵͳ�Դ���ģ����֤������
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            services.AddControllers(x =>
            {
                // ȫ���쳣����
                x.Filters.Add(typeof(GlobalExceptionsFilterAttribute));
                //Action ������ʾFluentValidation����
                x.Filters.Add(typeof(ValidateModelActionFilter));
            }).AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //����Model��Ϊnull������
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                //����nullֵ
                options.SerializerSettings.ContractResolver = new NullOutputHandResolver();
                //���ñ���ʱ�����UTCʱ��
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                //���Enumתstring
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.Load("WZH.Application"));
            services.AddFluentValidationClientsideAdapters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("wzh", new OpenApiInfo { Title = "��ֽ��", Version = "wzh" });
                c.SwaggerDoc("cj", new OpenApiInfo { Title = "���ݲɼ�", Version = "cj" });
                c.SwaggerDoc("interfaceServe", new OpenApiInfo { Title = "�ӿڷ���", Version = "interfaceServe" });
                c.SwaggerDoc("file", new OpenApiInfo { Title = "�ļ�������", Version = "file" });
                c.SwaggerDoc("wechat", new OpenApiInfo { Title = "΢�Ŷ˽ӿ�", Version = "wechat" });
                c.SwaggerDoc("os", new OpenApiInfo { Title = "�������", Version = "os" });
                //��ǿswagger�ӿ��ĵ�ע��
                // ��ȡxml�ļ���
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // ��ȡxml�ļ�·��
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //�Ƿ���ʾע��
                c.IncludeXmlComments(xmlPath, true);

                //�������Model���xml�ļ���
                var xmlModelPath = Path.Combine(AppContext.BaseDirectory, "WZH.Application.xml");
                c.IncludeXmlComments(xmlModelPath);
            });

           // var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("DbConfig");

            //�����������ļ� �� ���ݻ����������ֿ��ˣ���������д
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                 .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: false)
                .Build();
            services.AddOptions();
         //   services.Configure<AppSettingConfig>(configuration.GetSection("DbConfig"));

            services.Configure<DbConnectionOption>(configuration.GetSection("DbConfig")).AddScoped<SlaveRoundRobin>()
                    .AddDbContext<WzhDbContext>();

            // ����ע��
            services.AddHttpClient();
            services.RunModuleInitializers(AssemblyList());

            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
            services.RunModuleInitializers(assemblies.ToArray());
            services.AddMediatR(assemblies.ToArray());

            // ���MiniProfiler����
            services.AddMiniProfiler(options =>
            {
                // �趨���ʷ������URL��·�ɻ���ַ
                options.RouteBasePath = "/profiler";
            }).AddEntityFramework(); //��ʾSQL��估��ʱ


        


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
                c.SwaggerEndpoint("/swagger/wzh/swagger.json", "��ֽ��");
                c.SwaggerEndpoint("/swagger/cj/swagger.json", "���ݲɼ�");
                c.SwaggerEndpoint("/swagger/interfaceServe/swagger.json", "�ӿڷ���");
                c.SwaggerEndpoint("/swagger/file/swagger.json", "�ļ�������");
                c.SwaggerEndpoint("/swagger/wechat/swagger.json", "΢�Ŷ˽ӿ�");
                c.SwaggerEndpoint("/swagger/os/swagger.json", "�������");
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