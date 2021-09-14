using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using WebApplication.Services;

namespace WebApplication
{
    public sealed class Startup : Object
    {
        private static IServiceCollection services;
        
        public Startup()
            : base()
        { }

        public void ConfigureServices(IServiceCollection services)
        {
            Startup.services = services;

            services.AddMessageSenderService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessageSenderService mss)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMessageSender();

            app.Map("/test", builder =>
            {
                builder.Run(async context =>
                {
                    await context.Response.WriteAsync(mss.Send());
                });
            });

            app.Run(InfoAboutServices);
        }

        private async static Task InfoAboutServices(HttpContext context)
        {
            var info = new StringBuilder();

            info.Append("");

            info.Append("<h1>Все сервисы</h1>");
            info.Append("<table style=\"border: 2px solid black\">");
            info.Append("<tr style=\"border: 2px solid black\"><th style=\"border: 2px solid black\">Тип</th><th style=\"border: 2px solid black\">Lifetime</th><th style=\"border: 2px solid black\">Реализация</th></tr>");

            foreach (ServiceDescriptor service in services)
            {
                info.Append("<tr style=\"border: 2px solid black\">");
                info.Append($"<td style=\"border: 2px solid black\">{service.ServiceType.FullName}</td>");
                info.Append($"<td style=\"border: 2px solid black\">{service.Lifetime}</td>");
                info.Append($"<td style=\"border: 2px solid black\">{service.ImplementationType?.FullName}</td>");
                info.Append("</tr>");
            }

            info.Append("</table>");

            context.Response.ContentType = "text/html; charset=utf-8";

            await context.Response.WriteAsync(info.ToString());
        }
    }
}