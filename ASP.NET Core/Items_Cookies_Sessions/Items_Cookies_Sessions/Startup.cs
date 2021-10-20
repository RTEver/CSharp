using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Items_Cookies_Sessions.MiddlewareComponents;

namespace Items_Cookies_Sessions
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;

                options.IdleTimeout = TimeSpan.FromSeconds(30);

                options.Cookie.Name = ".MyApplication.Session";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseMiddleware<PasswordSessionMiddleware>();

            app.UseMiddleware<AddColorToContextItemsMiddleware>("#FF0000");

            app.UseMiddleware<UsernameCookieMiddleware>();

            app.Run(async context =>
            {
                var color = context.Items["color"].ToString();

                var username = context.Items["username"].ToString();

                var password = context.Items["password"].ToString();

                await context.Response.WriteAsync($"<h1 style=\"color: {color};\">Hello, {username}!\nYour password: {password} :)</h1>");
            });
        }
    }
}
