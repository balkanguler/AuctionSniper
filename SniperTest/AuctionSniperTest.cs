using AuctionSniper.Xmpp;
using NSubstitute;
using NUnit.Framework;
using AuctionSniper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Test
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

            sniper = new AuctionSniper(ITEM_ID, auction);
            sniper.AddSniperListener(sniperListener);
        }

        [Test]
        public void ReportstLostWhenAuctionClosesImmediately()
        {
            sniper.AuctionClosed();

            sniperListener.Received().SniperStateChanged(Arg.Is<SniperSnapshot>
                    (ss => ss.State == SniperState.LOST));
        }

        [Test]
        public void ReportstLostIfAuctionClosesWhenBidding()
        {
            sniper.CurrentPrice(123, 45, PriceSource.FromOtherBidder);
            
            //In the original example it uses jMock states for keeping the method call
            bool called = false;
            sniperListener.When(sl => sl.SniperStateChanged(Arg.Is<SniperSnapshot>(ss => ss.State == SniperState.BIDDING))).Do(x => called = true);

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
            //bool called = false;
            //sniperListener.When(sl => sl.SniperWinning()).Do(x => called = true);

            //// If SniperBidding is called then SniperLost should be called.
            //if (called)
            //    sniperListener.Received().SniperWon();
        }

        [Test]
        public void ReportsIsWinningWhenCurrentPriceComesFromSniper()
        {
            sniper.CurrentPrice(123, 12, PriceSource.FromOtherBidder);
            sniper.CurrentPrice(135, 45, PriceSource.FromSniper);

            bool called = false;
            sniperListener.When(sl => sl.SniperStateChanged(Arg.Is<SniperSnapshot>(ss => ss.State == SniperState.BIDDING))).Do(x => called = true);

            if (called)                
                sniperListener.Received().SniperStateChanged(Arg.Is<SniperSnapshot>
                    (ss => ss.ItemId == ITEM_ID && ss.LastPrice == 135 & ss.LastBid == 135 &&  ss.State == SniperState.WINNING));
            
        }

        [Test]
        public void BidsHigherAndReportsBiddingWhenNewPriceArrives()
        {
            int price = 1001;
            int increment = 25;
            int bid = price + increment;

            sniper.CurrentPrice(price, increment, PriceSource.FromOtherBidder);

            auction.Received().Bid(price + increment);

            sniperListener.Received().SniperStateChanged(Arg.Is<SniperSnapshot>
                   (ss => ss.ItemId == ITEM_ID && ss.LastPrice == price & ss.LastBid == bid && ss.State == SniperState.BIDDING));

        }
    }
}
