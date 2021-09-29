using System;

using Microsoft.AspNetCore.Builder;

namespace WebApplication.Services
{
    public static class IApplicationBuilderExtensions : Object
    {
        public static void UseMessageSender(this IApplicationBuilder app)
            => app.UseMiddleware<MessageSenderMiddleware>();
    }
}