using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace WebApplication.Services
{
    public sealed class MessageSenderMiddleware : Object
    {
        private readonly RequestDelegate next;

        public MessageSenderMiddleware(RequestDelegate next)
            : base()
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMessageSender messageSender)
        {
            context.Response.ContentType = "text/html; charset=utf-8";

            await context.Response.WriteAsync(messageSender.Send());
        }
    }
}