using NSubstitute;
using NUnit.Framework;
using Sniper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperTest
{
    [TestFixture]
    public class AuctionSniperTest
    {
        ISniperListener sniperListener;
        AuctionSniper sniper;
        IAuction auction;

        [SetUp]
        public void Setup()
        {
            sniperListener = Substitute.For<ISniperListener>();
            auction = Substitute.For<IAuction>();

            sniper = new AuctionSniper(auction, sniperListener);
        }

        [Test]
        public void ReportstLostWhenAuctionClosesImmediately()
        {
            sniper.AuctionClosed();

            sniperListener.Received().SniperLost();
        }

        [Test]
        public void ReportstLostWhenAuctionClosesWhenBidding()
        {
            sniper.CurrentPrice(123, 45, PriceSource.FromOtherBidder);

            sniperListener.Received().SniperLost();
        }

        [Test]
        public void ReportsIsWinningWhenCurrentPriceComesFromSniper()
        {
            sniper.CurrentPrice(123, 45, PriceSource.FromSniper);

            sniperListener.Received().SniperWinning();
        }

        [Test]
        public void BidsHigherAndReportsBiddingWhenNewPriceArrives()
        {
            int price = 1001;
            int increment = 25;

            sniper.CurrentPrice(price, increment, PriceSource.FromOtherBidder);

            auction.Received().Bid(price + increment);
            sniperListener.Received().SniperBidding();

        }
    }
}
