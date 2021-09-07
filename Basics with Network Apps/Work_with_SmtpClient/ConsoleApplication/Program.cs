using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NetConsoleApp
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var from = new MailAddress("username@gmail.com", "User");

            var to = new MailAddress("username@gmail.com");

            var message = new MailMessage(from, to);

            message.Subject = "Some text";
            
            message.Body = "<h2>Uhuuuuuu...</h2>";

            message.IsBodyHtml = true;

            message.Attachments.Add(new Attachment("somefile.txt"));

            SendGmailAsync(from, to, message, "gmail", 574, "password").GetAwaiter().GetResult();

            Console.Read();
        }

        private static async Task SendGmailAsync(MailAddress from, MailAddress to, MailMessage message, String mailPostfix, Int32 port, String password)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            if (mailPostfix == null)
            {
                throw new ArgumentNullException("mailPostfix");
            }

            if (port < 0 || 65535 < port)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var smtpClient = new SmtpClient($"smtp.{mailPostfix}.com", port);

            smtpClient.Credentials = new NetworkCredential(from.Address, password);

            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(message);

            Console.WriteLine("Message was sent.");
        }
    }
}