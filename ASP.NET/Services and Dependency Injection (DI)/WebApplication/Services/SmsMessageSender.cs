using System;

namespace WebApplication.Services
{
    public sealed class SmsMessageSender : IMessageSender
    {
        public SmsMessageSender()
            : base()
        { }

        public String Send() => "Sent by SMS.";
    }
}