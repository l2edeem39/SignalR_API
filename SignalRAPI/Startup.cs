using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRAPI.HubSignalr;
using Microsoft.OpenApi.Models;
using SignalRAPI.Service;
using SignalRAPI.Service.Interface;
using SignalRAPI.Repository;
using SignalRAPI.Context;

namespace SignalRAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddTransient<IRepositoryService, RepositoryService>();
            services.AddScoped<IChatManageService, ChatManageService>();
            
            services.AddDbContext<DbContextClass>(ServiceLifetime.Transient);
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option =>
                option.WithOrigins("http://localhost:2000")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials());

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalrHub>("/chat");
            });

            ChatManageService._configuration = Configuration;
        }
    }
}
