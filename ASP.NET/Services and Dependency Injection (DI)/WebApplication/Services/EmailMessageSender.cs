using System;

namespace WebApplication.Services
{
    public sealed class EmailMessageSender : IMessageSender
    {
        public EmailMessageSender()
            : base()
        { }

        public String Send() => $"Sent by Email.";
    }
}