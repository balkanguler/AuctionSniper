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
        FakeAuctionServer auction = new FakeAuctionServer("item-54321");
        ApplicationRunner application = new ApplicationRunner();
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void SniperJoinsAuctionUntilAuctionCloses()
        {
            auction.StartSellingItem();

            application.StartBiddingIn(auction);
            auction.HasReceivedJoinRequestFromSniper(ApplicationRunner.SNIPER_XMPP_ID);

            auction.ReportPrice(1000, 98, "other bidder");
            application.HasShownSniperIsBidding(1000, 1098); //last price, last bid

            auction.HasReceivedBid(1098, ApplicationRunner.SNIPER_XMPP_ID);
            
            auction.ReportPrice(1098, 97, ApplicationRunner.SNIPER_XMPP_ID);
            application.HasShownSniperIsWinning(1098); //winning bid
            
            auction.AnnounceClosed();
            application.ShowSniperHasWonAuction(1098); //last price
            
        }

        [TearDown]
        public void TearDown()
        {
            auction.Stop();
            application.Stop();
        }
    }
}
