using agsXMPP;
using agsXMPP.protocol.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agsXMPPChat
{
    public class ChatManager
    {
        XmppClientConnection connection;
        List<Chat> chatList = new List<Chat>();
        List<IChatManagerListener> chatManagerListenerList = new List<IChatManagerListener>();

        public ChatManager(XmppClientConnection connection)
        {
            this.connection = connection;

            connection.OnRosterStart += new ObjectHandler(xmppCon_OnRosterStart);
            connection.OnRosterItem += new XmppClientConnection.RosterHandler(xmppCon_OnRosterItem);
            connection.OnRosterEnd += new ObjectHandler(xmppCon_OnRosterEnd);
            connection.OnPresence += new PresenceHandler(xmppCon_OnPresence);
            connection.OnMessage += new MessageHandler(xmppCon_OnMessage);
            connection.OnLogin += new ObjectHandler(xmppCon_OnLogin);
        }

        private void xmppCon_OnLogin(object sender)
        {
        }

        private void xmppCon_OnMessage(object sender, Message msg)
        {
            bool chatFound = false;

            foreach (Chat chat in chatList)
            {
                if (chat.UserName.Equals(msg.To.User) && chat.PeerUserName.Equals(msg.From.User))
                {
                    chatFound = true;
                    if (chat.MessageListener != null)
                    {
                        chat.MessageListener.ProcessMessage(chat, msg.Body);
                    }
                }
            }
            if (!chatFound)
            {
                Chat newChat = CreateChat(msg.From.User, msg.From.Server, msg.From.Resource, null);

                if (newChat.MessageListener != null)
                    newChat.MessageListener.ProcessMessage(newChat, msg.Body);
            }
        }

        private void xmppCon_OnPresence(object sender, Presence pres)
        {
        }

        private void xmppCon_OnRosterEnd(object sender)
        {
        }

        private void xmppCon_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem item)
        {
            Chat chat = new Chat(connection, item.Jid.User, item.Jid.Server, item.Jid.Resource, null);

            chatManagerListenerList.ForEach(cml => cml.ChatCreated(chat, false));

            chatList.Add(chat);

        }

        private void xmppCon_OnRosterStart(object sender)
        {
        }
        public void AddChatListener(IChatManagerListener listener)
        {
            chatManagerListenerList.Add(listener);
        }

        public Chat CreateChat(string peerUserName, string server, string resource, IMessageListener messageListener)
        {
            Chat chat = new Chat(this.connection, peerUserName, server, resource, messageListener);
            chatList.Add(chat);
            chatManagerListenerList.ForEach(cml => cml.ChatCreated(chat, true));

            return chat;
        }
    }
}
