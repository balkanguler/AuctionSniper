using AuctionSniper.Xmpp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Test
{
    [TestFixture]
    public class AuctionSniperEndToEndTest
    {
        FakeAuctionServer auction;
        FakeAuctionServer auction2;
        ApplicationRunner application;

        [SetUp]
        public void Setup()
        {
            auction = new FakeAuctionServer( new Item("item-54321", Int32.MaxValue));
            auction2 = new FakeAuctionServer(new Item("item-65432", Int32.MaxValue));
            application = new ApplicationRunner();
            MessageListener.ClearQueue();
        }

        [Test]
        public void SniperJoinsAuctionUntilAuctionCloses()
        {
            auction.StartSellingItem();

            application.StartBiddingIn(auction);
            auction.HasReceivedJoinRequestFromSniper(ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1000, 98, "other bidder");
            application.HasShownSniperIsBidding(auction, 1000, 1098); //last price, last bid

            auction.HasReceivedBid(1098, ApplicationRunner.SNIPER_XMPP_ID);
            
            auction.ReportPrice(1098, 97, ApplicationRunner.SNIPER_XMPP_ID);
            application.HasShownSniperIsWinning(auction, 1098); //winning bid
            
            auction.AnnounceClosed();
            application.ShowSniperHasWonAuction(auction, 1098); //last price

            auction.ReceivesEventsFromAuctionServerAfterJoining();
        }

        [Test]
        public void SniperBidForMultipleItems()
        {
            auction.StartSellingItem();
            auction2.StartSellingItem();

            application.StartBiddingIn(auction, auction2);
            auction.HasReceivedJoinRequestFromSniper(ApplicationRunner.SNIPER_XMPP_ID);
            auction.HasReceivedJoinRequestFromSniper(ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1000, 98, "other bidder");
            auction.HasReceivedBid(1098, ApplicationRunner.SNIPER_XMPP_ID);

            auction2.ReportPrice(500, 21, "other bidder");
            auction2.HasReceivedBid(521, ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1098, 97, ApplicationRunner.SNIPER_XMPP_ID);
            auction2.ReportPrice(521, 22, ApplicationRunner.SNIPER_XMPP_ID);

            application.HasShownSniperIsWinning(auction, 1098);
            application.HasShownSniperIsWinning(auction2, 521);

            auction.AnnounceClosed();
            auction2.AnnounceClosed();

            application.ShowSniperHasWonAuction(auction, 1098);
            application.ShowSniperHasWonAuction(auction2, 521);

        }

        [Test]
        public void SniperLosesAnAuctionWhenThePriceIsTooHigh()
        {
            auction = new FakeAuctionServer(new Item("item-54321", 1100));
            auction.StartSellingItem();            
            application.StartBiddingWithStopPrice(1100, auction);
            auction.HasReceivedJoinRequestFromSniper(ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1000, 98, "other bidder");
            application.HasShownSniperIsBidding(auction, 1000, 1098);

            auction.HasReceivedBid(1098, ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1197, 10, "third party");
            application.HasShownSniperIsLosing(auction, 1197, 1098);

            auction.ReportPrice(1207, 10, "fourth party");
            application.HasShownSniperIsLosing(auction, 1207, 1098);

            auction.AnnounceClosed();
            application.ShowsSniperHasLostAuction(auction, 1207, 1098);
        }

       

        [Test]
        public void SniperMakesAHigherBidButLoses()
        {
            auction.StartSellingItem();
            application.StartBiddingIn(auction);
            auction.HasReceivedJoinRequestFromSniper(ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1000, 98, "other bidder");
            application.HasShownSniperIsBidding(auction, 1000, 1098);

            auction.HasReceivedBid(1098, ApplicationRunner.SNIPER_XMPP_ID);

            auction.AnnounceClosed();
            application.ShowsSniperHasLostAuction(auction, 1000, 1098);
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Tearing Down...");
            auction.Stop();
            application.Stop();
        }
    }
}


