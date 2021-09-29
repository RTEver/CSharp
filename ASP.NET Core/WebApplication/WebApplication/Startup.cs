using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using WebApplication.Middlwares;
using System.IO;

namespace WebApplication
{
    public class Startup : Object
    {
        private readonly IWebHostEnvironment env;

        public Startup(IWebHostEnvironment env)
            : base()
        {
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;

                options.HttpsPort = 44367;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("us.example.com");
                options.ExcludedHosts.Add("www.example.com");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        var info = new StringBuilder();

            //        var type = env.GetType();

            //        var properties = type.GetProperties();

            //        foreach (PropertyInfo property in properties)
            //        {
            //            info.AppendFormat("{1}: {2}{0}", Environment.NewLine, property.Name, property.GetValue(env) ?? "no value");
            //        }

            //        await context.Response.WriteAsync(info.ToString());
            //    });
            //});

            ////var x = default(Int32);

            ////app.Use(async (context, next) =>
            ////{
            ////    x += 2;

            ////    await next.Invoke();

            ////    x += 2;

            ////    await context.Response.WriteAsync($"x = {x}.");
            ////});

            ////app.Run(async context =>
            ////{
            ////    x += 2;

            ////    await Task.FromResult(0);
            ////});

            //app.Map("/home", HomePage);

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Page not found.");
            //});

            /*new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),

                RequestPath = "/pages"
            }*/

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),

            //    RequestPath = "/pages"
            //});

            //app.UseDefaultFiles();

            //app.Map("/home", HomePage);

            //app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Some folder")),

            //    RequestPath = "/pages",
            //});

            //app.UseToken("12121");

            int i = 0;

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

            app.Map("/error", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync($"Err: {context.Request.Query["code"]}");
            }));

            app.Map("/hello", ap => ap.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello ASP.NET Core | i = {i++}");
            }));

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});
        }

        private static void HomePage(IApplicationBuilder app)
        {
            app.MapWhen(context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5", HandleId);

            app.Map("/index", HomeIndexPage);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("/home");
            });
        }

        private static void HomeIndexPage(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("/home/index");
            });
        }

        private static void HandleId(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var info = new StringBuilder();

                var type = context.Request.GetType();

                var properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    var value = default(String);

                    try
                    {
                        value = property.GetValue(context.Request).ToString();
                    }
                    catch
                    {
                        value = "no value";
                    }

                    info.AppendFormat("{1}: {2}{0}", Environment.NewLine, property.Name, value);
                }

                info.AppendLine("Queries:");

                foreach (var query in context.Request.Query)
                {
                    info.AppendFormat($"{query.Key} {query.Value}\n");
                }

                await context.Response.WriteAsync(info.ToString());
            });
        }
    }
}