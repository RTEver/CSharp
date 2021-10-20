using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Items_Cookies_Sessions.MiddlewareComponents
{
    public sealed class UsernameCookieMiddleware : Object
    {
        private readonly RequestDelegate next;

        public UsernameCookieMiddleware(RequestDelegate next)
            : base()
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var username = "guest";

            if (context.Request.Cookies.ContainsKey("username"))
            {
                username = context.Request.Cookies["username"];
            }
            else
            {
                context.Response.Cookies.Append("username", username);
            }

            context.Items.Add("username", username);

            await next.Invoke(context);
        }
    }
}