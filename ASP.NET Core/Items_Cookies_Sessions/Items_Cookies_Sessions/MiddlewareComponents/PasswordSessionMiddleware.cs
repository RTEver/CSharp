using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Items_Cookies_Sessions.MiddlewareComponents
{
    public sealed class PasswordSessionMiddleware : Object
    {
        private readonly RequestDelegate next;

        public PasswordSessionMiddleware(RequestDelegate next)
            : base()
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Session.Keys.Contains("password"))
            {
                context.Session.SetString("password", "1234567890");

                await context.Response.WriteAsync("Reload a page.");
            }
            else
            {
                context.Items.Add("password", context.Session.GetString("password"));

                await next.Invoke(context);
            }
        }
    }
}