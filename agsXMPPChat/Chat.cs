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
        string peerUserName;
        string userName;
        string server;
        string resource;
        IMessageListener messageListener;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string PeerUserName
        {
            get { return peerUserName; }
            set { peerUserName = value; }
        }
        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        public string Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        public IMessageListener MessageListener
        {
            get { return messageListener; }
            set { messageListener = value; }
        }
        public Chat(XmppClientConnection connection, string peer, string server, string resource, IMessageListener messageListener)
        {
            this.connection = connection;
            this.userName = connection.Username;
            this.peerUserName = peer;
            this.server = server;
            this.resource = resource;

            this.messageListener = messageListener;
        }

        public void SendMessage(string message)
        {
            connection.Send(new Message(new Jid(peerUserName + "@" + server + "/" + resource), MessageType.chat, message));
        }
    }
}
