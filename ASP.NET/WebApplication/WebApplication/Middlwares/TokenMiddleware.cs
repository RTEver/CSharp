using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace WebApplication.Middlwares
{
    public sealed class TokenMiddleware : Object
    {
        private readonly RequestDelegate next;
        private readonly String pattern;

        public TokenMiddleware(RequestDelegate next, String pattern)
            : base()
        {
            this.next = next;
            this.pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Query.ContainsKey("token"))
            {
                await context.Response.WriteAsync("Request has no token.");

                return;
            }

            var token = context.Request.Query["token"];

            if (token != pattern)
            {
                context.Response.StatusCode = 403;

                await context.Response.WriteAsync("Token is invalid.");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}