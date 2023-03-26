using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using SyZero;
using SyZero.AspNetCore;
using SyZero.AutoMapper;
using SyZero.Consul;
using SyZero.DynamicWebApi;
using SyZero.Feign;
using SyZero.Log4Net;
using SyZero.Redis;
using SyZero.Web.Common;
using SyZero.OpenAI.Repository;
using SyZero.OpenAI.Web.Filter;
using System.Net;
using SyZero.OpenAI.Core.OpenAI;

namespace SyZero.OpenAI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfig.Configuration = configuration;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
         
            services.AddControllers().AddMvcOptions(options =>
            {
                options.Filters.Add(new AppExceptionFilter());
                options.Filters.Add(new AppResultFilter());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new LongToStrConverter());
            });

            //动态WebApi
            services.AddDynamicWebApi(new DynamicWebApiOptions()
            {
                DefaultApiPrefix = "/api",
                DefaultAreaName = AppConfig.ServerOptions.Name
            });
            //Swagger
            services.AddSwagger();
            services.AddSingleton<OpenAIService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //使用SyZero
            builder.RegisterModule<SyZeroModule>();
            //使用AutoMapper
            builder.RegisterModule<AutoMapperModule>();
            //使用SqlSugar仓储
            //builder.RegisterModule<RepositoryModule>();
            //注入控制器
            builder.RegisterModule<SyZeroControllerModule>();
            //注入Log4Net
            builder.RegisterModule<Log4NetModule>();
            //注入Redis
            builder.RegisterModule<RedisModule>();
            //注入公共层
            builder.RegisterModule<CommonModule>();
            //注入Feign
            builder.RegisterModule<FeignModule>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
            app.UseRouting();
            app.UseStaticFiles();
            app.UseSyAuthMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SyZero.Authorization.Web API V1");
                c.RoutePrefix = "api/swagger";

            });
            app.UseConsul();
            app.UseSyZero();
        }
    }
}



