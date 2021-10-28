using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Routing
{
    public sealed class OutputHelloMiddleware : Object
    {
        private readonly RequestDelegate next;

        public OutputHelloMiddleware(RequestDelegate next)
            : base()
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Hello, World!");
        }
    }
}