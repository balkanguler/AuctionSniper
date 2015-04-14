using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPPChat;
using NUnit.Framework;
using Sniper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SniperTest
{
    class FakeAuctionServer
    {
        public static readonly string ITEM_ID_AS_LOGIN = "auction-{0}";
        public static readonly string AUCTION_RESOURCE = "Auction";
        public static readonly string XMPP_HOSTNAME = "ZT0804N01";
        public static readonly int XMPP_PORT = 5222;

        private static readonly string AUCTION_PASSWORD = "sniper";
        private static readonly MessageListener messageListener = new MessageListener();

        private readonly string itemId;
        private readonly XmppClientConnection connection;

        private Chat currentChat;

        public FakeAuctionServer(string itemId)
        {
            this.itemId = itemId;
            connection = new XmppClientConnection(XMPP_HOSTNAME, XMPP_PORT);
        }

        internal void StartSellingItem()
        {
            Thread.Sleep(2000);
            Jid jid = new Jid(string.Format(ITEM_ID_AS_LOGIN, itemId), XMPP_HOSTNAME, AUCTION_RESOURCE);

            connection.Password = AUCTION_PASSWORD;
            connection.Username = jid.User;
            connection.AutoAgents = false;
            connection.AutoPresence = true;
            connection.AutoRoster = true;
            connection.AutoResolveConnectServer = true;

            ChatManager chatManager = new ChatManager(connection);

            chatManager.AddChatListener(new ChatManagerListener
            {
                OnChatCreated = (Chat chat) =>
                 {
                     currentChat = chat;

                     chat.MessageListener = messageListener;
                 }
            });

            connection.Open();
        }

        internal void HasReceivedJoinRequestFromSniper(string sniperId)
        {
            receivesAMessageMatching(sniperId, CommandFormat.JOIN_COMMAND_FORMAT);
        }

        internal void AnnounceClosed()
        {
            Debug.WriteLine("announceClosed: " + CommandFormat.CLOSE_COMMAND_FORMAT);
            currentChat.SendMessage(CommandFormat.CLOSE_COMMAND_FORMAT);
        }

        internal void Stop()
        {
            connection.Close();
        }

        public string ItemId { get { return itemId; } }

        internal void ReportPrice(int price, int incerement, string bidder)
        {
            currentChat.SendMessage(string.Format(CommandFormat.REPORT_PRICE_COMMAND_FORMAT, price, incerement, bidder));
        }

        internal void HasReceivedBid(int bid, string sniperId)
        {
            StringAssert.AreEqualIgnoringCase(currentChat.Peer, sniperId);

            receivesAMessageMatching(sniperId, string.Format(CommandFormat.BID_COMMAND_FORMAT, bid));
        }

        private void receivesAMessageMatching(string sniperId, string expectedMessage)
        {
            messageListener.receivesAMessagesWithText(expectedMessage);
            StringAssert.AreEqualIgnoringCase(sniperId, currentChat.Peer);
        }
    }
}
