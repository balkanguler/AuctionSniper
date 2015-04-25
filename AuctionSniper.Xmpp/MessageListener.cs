using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AuctionSniper.Xmpp
{
    public class MessageListener : IMessageListener
    {
        public Action<Chat, string> OnMessageReceived;

        public virtual void ProcessMessage(Chat aChat, string message)
        {
            if (OnMessageReceived != null)
            {
                OnMessageReceived(aChat, message);
            }
        }
    }
}
