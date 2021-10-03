using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Configuration
{
    public sealed class UserInfoMiddleware : Object
    {
        private RequestDelegate next;

        private User user;

        public UserInfoMiddleware(RequestDelegate next, IOptions<User> options)
            : base()
        {
            this.next = next;

            user = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"{user.Name} - {user.Age}");
        }
    }
}