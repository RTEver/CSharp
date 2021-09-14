using System;

using Microsoft.AspNetCore.Builder;

namespace WebApplication.Middlwares
{
    public static class IApplicationBuilderExtensions : Object
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, String pattern)
            => builder.UseMiddleware<TokenMiddleware>(pattern);
    }
}