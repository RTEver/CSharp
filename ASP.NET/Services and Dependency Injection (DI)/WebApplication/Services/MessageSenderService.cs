using System;

namespace WebApplication.Services
{
    public sealed class MessageSenderService : Object
    {
        private readonly IMessageSender messageSender;

        public MessageSenderService(IMessageSender messageSender)
            : base()
        {
            this.messageSender = messageSender;
        }

        public String Send() => messageSender.Send();
    }
}