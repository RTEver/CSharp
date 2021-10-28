using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Routing
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapMiddlewareGet("{controller}/{action}", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("MapMiddlewareGet");
                });
            });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            // Routing
            /*
            var routeHandler = new RouteHandler(async context =>
            {
                await context.Response.WriteAsync("This is route handler.");
            });

            var routeBuilder = new RouteBuilder(app, routeHandler);

            routeBuilder.MapRoute("default", "{controller}/{action}");

            app.UseRouter(routeBuilder.Build());

            app.Run(async context =>
            {
                await context.Response.WriteAsync("This is run.");
            });
            */

            // EndpointRouting
            /*
             * app.UseRouting();
             *
             * app.Use(async (context, next) =>
             * {
             *     var endpoint = context.GetEndpoint();
             * 
             *     if (endpoint != null)
             *     {
             *         var routePattern = (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern?.RawText;
             *
             *         Debug.WriteLine($"Endpoint Name: {endpoint.DisplayName}");
             *         Debug.WriteLine($"Route Pattern: {routePattern}");
             *
             *         await next.Invoke();
             *     }
             *     else
             *     {
             *         Debug.WriteLine("Endpoint: null");
             *
             *         await context.Response.WriteAsync("Endpoint is not defined");
             *     }
             * });
             *
             * app.UseEndpoints(endpoints =>
             * {
             *     endpoints.MapGet("/index", async context =>
             *     {
             *        await context.Response.WriteAsync("Hello Index!");
             *     });
             * 
             *     endpoints.MapGet("/", async context =>
             *     {
             *         await context.Response.WriteAsync("Hello World!");
             *     });
             * });
             */
        }
    }
}
