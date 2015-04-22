using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionSniper.Test
{
    class ChatManagerListener : IChatManagerListener
    {
        public void ChatCreated(Chat chat, bool createdlocally)
        {
            Console.WriteLine("Chat Created");

            if (OnChatCreated != null)
            {
                OnChatCreated(chat);
            }
        }

        public Action<Chat> OnChatCreated;
    }
}
