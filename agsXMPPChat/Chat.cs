using agsXMPP;
using agsXMPP.protocol.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace agsXMPPChat
{
    public class Chat
    {
        XmppClientConnection connection;
        string peer;

        public string Peer
        {
            get { return peer; }
            set { peer = value; }
        }
        string server;

        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        string resource;

        public string Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        IMessageListener messageListener;

        public IMessageListener MessageListener
        {
            get { return messageListener; }
            set { messageListener = value; }
        }

        public Chat(XmppClientConnection connection, string peer, string server, string resource, IMessageListener messageListener)
        {
            this.connection = connection;
            this.peer = peer;
            this.server = server;
            this.resource = resource;

            this.messageListener = messageListener;
        }

        public void SendMessage(string message)
        {
            connection.Send(new Message(new Jid(peer + "@" + server + "/" + resource), MessageType.chat, message));
        }
    }
}
