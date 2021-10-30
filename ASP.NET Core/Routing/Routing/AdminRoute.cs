using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Routing
{
    public class AdminRoute : Object, IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            var url = context.HttpContext.Request.Path.Value.TrimEnd('/');

            if (url.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    ctx.Response.ContentType = "text/html;charset=utf-8";

                    await ctx.Response.WriteAsync("Hello, Admin!");
                };
            }

            await Task.CompletedTask;
        }
    }
}