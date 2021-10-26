using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Logging
{
    public sealed class Startup : Object
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // Another way to get logger:
        // var loggerFactory = LoggerFactory.Create(builder =>
        // {
        //     builder.AddConsole(); // => Output in Console
        //     builder.AddDebug();   // => Output in VS Console
        // });
        //
        // var logger = loggerFactory.CreateLogger<Startup>();
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var pathToLoggerFile = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");

            loggerFactory.AddFile(pathToLoggerFile);

            var logger = loggerFactory.CreateLogger("FileLogger");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    logger.LogTrace("Processing request {0}", context.Request.Path);
                    logger.LogInformation("Processing request {0}", context.Request.Path);
                    logger.LogDebug("Processing request {0}", context.Request.Path);
                    logger.LogWarning("Processing request {0}", context.Request.Path);
                    logger.LogError("Processing request {0}", context.Request.Path);

                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}