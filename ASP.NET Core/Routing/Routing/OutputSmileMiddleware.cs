using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Routing
{
    public sealed class OutputSmileMiddleware : Object
    {
        private readonly RequestDelegate next;

        public OutputSmileMiddleware(RequestDelegate next)
            : base()
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(":)");
        }
    }
}