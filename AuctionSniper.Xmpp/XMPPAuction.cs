using agsXMPP;
using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public class XMPPAuction : IAuction
    {
        public static readonly string ITEM_ID_AS_LOGIN = "auction-{0}";
        public static readonly string AUCTION_RESOURCE = "Auction";
        public static readonly string AUCTION_ID_FORMAT = ITEM_ID_AS_LOGIN + "{0}/" + AUCTION_RESOURCE;
        private readonly List<IAuctionEventListener> auctionEventListeners = new List<IAuctionEventListener>();

        public Chat Chat { get; set; }


        public XMPPAuction(XmppClientConnection connection, string itemId)
        {
            ChatManager chatManager = new ChatManager(connection);
            Chat = chatManager.CreateChat(string.Format(ITEM_ID_AS_LOGIN, itemId), connection.Server, AUCTION_RESOURCE,
                new AuctionMessageTranslator(connection.Username, auctionEventListeners));
        }

        public void Bid(int amount)
        {
            SendMessage(string.Format(CommandFormat.BID_COMMAND_FORMAT, amount));
        }

        public void Join()
        {
            Console.WriteLine("Join message sending");
            SendMessage(string.Format(CommandFormat.JOIN_COMMAND_FORMAT));
        }

        private void SendMessage(string message)
        {
            if (Chat != null)
                Chat.SendMessage(message);
        }


        public void AddAuctionEventListener(IAuctionEventListener auctionEventListener)
        {
            auctionEventListeners.Add(auctionEventListener);
        }
    }
}
