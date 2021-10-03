using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Configuration.Text_configuration_provider;

namespace Configuration
{
    public sealed class Startup : Object
    {
        public IConfiguration AppConfiguration { get; private set; }

        public Startup(IConfiguration configuration)
            : base()
        {
            var args = new[] { "age=21" };

            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new Dictionary<String, String>()
                {
                    { "surname"   , "Talataj"                   },
                    { "name"      , "Vitaly"                    },
                    { "patronymic", "Andreevich"                },
                    { "color"     , "red"                       },
                    { "text"      , "Hello my Lovely World! :)" },
                })
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .AddConfiguration(configuration)
                .AddJsonFile("conf.json")
                .AddJsonFile("conf2.json")
                .AddTextFile("config.txt");

            AppConfiguration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(provider => AppConfiguration);

            services.Configure<User>(AppConfiguration);
            services.Configure<User>(user => user.Age = 999);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<ConfigMiddleware>();

            //app.UseMiddleware<UserInfoMiddleware>();

            app.UseRouting();
            //app.Use(async (context, next) =>
            //{
            //    await next.Invoke();

            //    await context.Response.WriteAsync("End");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var stringBuilder = new StringBuilder();

                    foreach (KeyValuePair<String, String> configString in AppConfiguration.AsEnumerable())
                    {
                        stringBuilder.AppendLine($"{configString.Key} - {configString.Value}");
                    }

                    var color = AppConfiguration["color"];
                    var text = AppConfiguration["text"];
                    var text2 = AppConfiguration["texts"];
                    var someName = AppConfiguration["some name"];

                    var user = AppConfiguration.Get<User>();

                    //await context.Response.WriteAsync($"{user.Name} - {user.Age}");
                    //await context.Response.WriteAsync($"<p style='color: {color}'>{text2}</p><p>{someName}</p>");
                    await context.Response.WriteAsync($"{stringBuilder}");
                });
            });
        }
    }
}