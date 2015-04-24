using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    class ChatDisconnector : IAuctionEventListener
    {
        private AuctionMessageTranslator translator;
        private Chat chat;
        public ChatDisconnector(AuctionMessageTranslator translator, Chat chat)
        {
            this.translator = translator;
            this.chat = chat;
        }
        public void AuctionClosed()
        {
            
        }

        public void CurrentPrice(int price, int increment, PriceSource priceSource)
        {
            
        }

        public void AuctionFailed()
        {
            chat.MessageListener = null;
        }
    }
}
