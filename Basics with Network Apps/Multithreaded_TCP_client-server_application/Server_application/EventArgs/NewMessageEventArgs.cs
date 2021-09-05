using System;

namespace Server_application.EventArgs
{
    internal sealed class NewMessageEventArgs : System.EventArgs
    {
        private readonly Client client;
        private readonly String message;

        internal NewMessageEventArgs(Client client, String message)
            : base()
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            this.client = client;
            this.message = message;
        }

        internal Client Client => client;

        internal String Message => message;
    }
}