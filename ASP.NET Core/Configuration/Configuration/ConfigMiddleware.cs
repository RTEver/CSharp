using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public sealed class ConfigMiddleware : Object
    {
        private readonly RequestDelegate next;

        public IConfiguration AppConfiguration { get; set; }

        public ConfigMiddleware(RequestDelegate next, IConfiguration configuration)
            : base()
        {
            this.next = next;

            AppConfiguration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"Name: {AppConfiguration["name"]}\nAge: {AppConfiguration["age"]}");
        }
    }
}