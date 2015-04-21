using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperTest
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
            auction = new FakeAuctionServer("item-54321");
            auction2 = new FakeAuctionServer("item-65432");
            application = new ApplicationRunner();
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

        [TearDown]
        public void TearDown()
        {
            auction.Stop();
            application.Stop();
        }
    }
}


