using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Routing
{
    public sealed class OutputGodOfEverythingMiddleware : Object
    {
        private readonly RequestDelegate next;

        public OutputGodOfEverythingMiddleware(RequestDelegate next)
            : base()
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("God");

            await next.Invoke(context);
        }
    }
}