using System;

using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Services
{
    public static class IServiceCollectionExtensions : Object
    {
        public static void AddEmailMessageSender(this IServiceCollection services)
            => services.AddTransient<IMessageSender, EmailMessageSender>();

        public static void AddSmsMessageSender(this IServiceCollection services)
            => services.AddTransient<IMessageSender, SmsMessageSender>();

        public static void AddEmailMessageSenderService(this IServiceCollection services)
        {
            services.AddEmailMessageSender();

            services.AddTransient<MessageSenderService>();
        }

        public static void AddSmsMessageSenderService(this IServiceCollection services)
        {
            services.AddSmsMessageSender();

            services.AddTransient<MessageSenderService>();
        }

        public static void AddMessageSenderService(this IServiceCollection services)
        {
            services.AddTransient<IMessageSender>(provider =>
            {
                if (DateTime.Now.Hour > 12) return new EmailMessageSender();

                return new SmsMessageSender();
            });

            services.AddTransient<MessageSenderService>();
        }
    }
}