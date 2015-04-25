using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionSniper.Test
{
    class ChatManagerListener : IChatManagerListener
    {
        public Action<Chat> OnChatCreated;

        public void ChatCreated(Chat chat, bool createdlocally)
        {
            if (OnChatCreated != null)
            {
                OnChatCreated(chat);
            }
        }
    }
}
