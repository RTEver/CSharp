using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Items_Cookies_Sessions.MiddlewareComponents
{
    public sealed class AddColorToContextItemsMiddleware : Object
    {
        private readonly RequestDelegate next;

        private String defaultColor;

        public AddColorToContextItemsMiddleware(RequestDelegate next, String defaultColor)
            : base()
        {
            this.next = next;

            this.defaultColor = defaultColor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var color = defaultColor;

            if (context.Request.Query.ContainsKey("color"))
            {
                color = context.Request.Query["color"];
            }

            context.Items.Add("color", color);

            await next.Invoke(context);
        }
    }
}