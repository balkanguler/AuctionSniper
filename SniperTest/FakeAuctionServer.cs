using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPPChat;
using AuctionSniper.Xmpp;
using NSubstitute;
using NUnit.Framework;
using AuctionSniper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace AuctionSniper.Test
{
    class FakeAuctionServer
    {
        public static readonly string ITEM_ID_AS_LOGIN = "auction-{0}";
        public static readonly string AUCTION_RESOURCE = "Auction";
        public static readonly string XMPP_HOSTNAME = "ZT0804N01";
        public static readonly int XMPP_PORT = 5222;

        private static readonly string AUCTION_PASSWORD = "sniper";
        private static readonly MessageListener messageListener = new MessageListener();

        private readonly Item item;
        private readonly XmppClientConnection connection;

        private Chat currentChat;

        public FakeAuctionServer(Item item)
        {
            this.item = item;
            connection = new XmppClientConnection(XMPP_HOSTNAME, XMPP_PORT);
        }

        internal void StartSellingItem()
        {
            Thread.Sleep(2000);
            Jid jid = new Jid(string.Format(ITEM_ID_AS_LOGIN, item.Identifier), XMPP_HOSTNAME, AUCTION_RESOURCE);

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

        public Item Item { get { return item; } }

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

        public void ReceivesEventsFromAuctionServerAfterJoining()
        {
            IAuctionEventListener eventListenerMock = Substitute.For<IAuctionEventListener>();

            IAuction auction = new XMPPAuction(connection, item);
            auction.AddAuctionEventListener(eventListenerMock);

            auction.Join();
            HasReceivedJoinRequestFromSniper(string.Format(ITEM_ID_AS_LOGIN, item.Identifier));
            AnnounceClosed();

            //Sometimes test finishes before the client receives the Close message. So we set buffer for client to receive and process the message.
            Thread.Sleep(1000);           

            eventListenerMock.Received(1).AuctionClosed();
            
        }
    }
}
