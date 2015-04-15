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
        private string ITEM_ID;

        [SetUp]
        public void Setup()
        {
            sniperListener = Substitute.For<ISniperListener>();
            auction = Substitute.For<IAuction>();
            ITEM_ID = "test item";

            sniper = new AuctionSniper(auction, sniperListener, ITEM_ID);
        }

        [Test]
        public void ReportstLostWhenAuctionClosesImmediately()
        {
            sniper.AuctionClosed();

            sniperListener.Received().SniperLost();
        }

        [Test]
        public void ReportstLostIfAuctionClosesWhenBidding()
        {
            sniper.CurrentPrice(123, 45, PriceSource.FromOtherBidder);
            
            //In the original example it uses jMock states for keeping the method call
            bool called = false;
            sniperListener.When(sl => sl.SniperBidding(Arg.Any<SniperState>())).Do(x => called = true);

            // If SniperBidding is called then SniperLost should be called.
            if (called)
                sniperListener.Received().SniperLost();
        }

        [Test]
        public void ReportstWonWhenAuctionClosesWhenWinning()
        {
            sniper.CurrentPrice(123, 45, PriceSource.FromSniper);
            sniper.AuctionClosed();

            //In the original example it uses jMock states for keeping the method call
            bool called = false;
            sniperListener.When(sl => sl.SniperWinning()).Do(x => called = true);

            // If SniperBidding is called then SniperLost should be called.
            if (called)
                sniperListener.Received().SniperWon();
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
            int bid = price + increment;

            sniper.CurrentPrice(price, increment, PriceSource.FromOtherBidder);

            auction.Received().Bid(price + increment);
            sniperListener.Received().SniperBidding(Arg.Is<SniperState>(ss => ss.ItemId == ITEM_ID && ss.LastPrice == price & ss.LastBid == bid));

        }
    }
}
