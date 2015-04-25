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
        readonly List<IAuctionEventListener> auctionEventListeners = new List<IAuctionEventListener>();
        IXMPPFailureReporter failureReporter;

        public static readonly string ITEM_ID_AS_LOGIN = "auction-{0}";
        public static readonly string AUCTION_RESOURCE = "Auction";
        public static readonly string AUCTION_ID_FORMAT = ITEM_ID_AS_LOGIN + "{0}/" + AUCTION_RESOURCE;

        public Chat Chat { get; set; }

        public XMPPAuction(XmppClientConnection connection, Item item, IXMPPFailureReporter failureReporter)
        {
            this.failureReporter = failureReporter;
            AuctionMessageTranslator translator = translatorFor(connection);
            ChatManager chatManager = new ChatManager(connection);
            Chat = chatManager.CreateChat(string.Format(ITEM_ID_AS_LOGIN, item.Identifier), connection.Server, AUCTION_RESOURCE,
                translator);
            auctionEventListeners.Add(chatDisconnectorFor(translator, Chat));
        }

        private IAuctionEventListener chatDisconnectorFor(AuctionMessageTranslator translator, Chat chat)
        {
            return new ChatDisconnector(translator, chat);
        }
        private AuctionMessageTranslator translatorFor(XmppClientConnection connection)
        {
            return new AuctionMessageTranslator(connection.Username, auctionEventListeners, failureReporter);
        }

        public void Bid(int amount)
        {
            SendMessage(string.Format(CommandFormat.BID_COMMAND_FORMAT, amount));
        }

        public void Join()
        {
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
