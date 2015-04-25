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
        Item item;

        [SetUp]
        public void Setup()
        {
            sniperListener = Substitute.For<ISniperListener>();
            auction = Substitute.For<IAuction>();
            item = new Item("test item", Int32.MaxValue);

            sniper = new AuctionSniper(item, auction);
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
        public void ReportsWonIfAuctionClosesWhenWinning()
        {
            sniper.CurrentPrice(123, 45, PriceSource.FromSniper);
            sniper.AuctionClosed();

            //In the original example it uses jMock states for keeping the method call
            bool called = false;
            sniperListener.When(sl => sl.SniperStateChanged(Arg.Is<SniperSnapshot>(ss => ss.State == SniperState.WINNING))).Do(x => called = true);

            // If SniperWinning is called then SniperWon should be called.
            if (called)
                sniperListener.Received().SniperWon();
        }

        [Test]
        public void ReportsIsWinningWhenCurrentPriceComesFromSniper()
        {
            sniper.CurrentPrice(123, 12, PriceSource.FromOtherBidder);
            sniper.CurrentPrice(135, 45, PriceSource.FromSniper);

            bool called = false;
            sniperListener.When(sl => sl.SniperStateChanged(Arg.Is<SniperSnapshot>(ss => ss.State == SniperState.BIDDING))).Do(x => called = true);

            if (called)
                sniperListener.Received().SniperStateChanged(new SniperSnapshot(item.Identifier, 135, 135, SniperState.WINNING));
        }

        [Test]
        public void BidsHigherAndReportsBiddingWhenNewPriceArrives()
        {
            int price = 1001;
            int increment = 25;
            int bid = price + increment;

            sniper.CurrentPrice(price, increment, PriceSource.FromOtherBidder);

            auction.Received().Bid(price + increment);

            sniperListener.Received().SniperStateChanged(new SniperSnapshot(item.Identifier, price, bid, SniperState.BIDDING));
        }

        private void createSniperWithStopPrice(int stopPrice)
        {
            item = new Item("test item", stopPrice);

            sniper = new AuctionSniper(item, auction);
            sniper.AddSniperListener(sniperListener);
        }

        [Test]
        public void DoesNotBidAndReportsLosingIfSubsequentPriceIsAboveStopPrice()
        {
            createSniperWithStopPrice(1234);

            bool bidded = false;

            sniperListener.When(s => s.SniperStateChanged(new SniperSnapshot(item.Identifier, 123, 168, SniperState.BIDDING))).Do(x => bidded = true);

            sniper.CurrentPrice(123, 45, PriceSource.FromOtherBidder);
            sniper.CurrentPrice(2345, 25, PriceSource.FromOtherBidder);

            if (bidded)
                sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 2345, 168, SniperState.LOSING));
        }

        [Test]
        public void DoesNotBidReportsLosingIfFirstPriceIsAboveStopPrice()
        {
            createSniperWithStopPrice(1234);

            sniper.CurrentPrice(2345, 45, PriceSource.FromOtherBidder);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 2345, 0, SniperState.LOSING));
            sniperListener.DidNotReceive().SniperStateChanged(new SniperSnapshot(item.Identifier, 2345, 2345, SniperState.BIDDING));
        }

        [Test]
        public void ReporstLostIfAuctionClosesWhenLosing()
        {
            createSniperWithStopPrice(1234);

            sniper.CurrentPrice(2345, 45, PriceSource.FromOtherBidder);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 2345, 0, SniperState.LOSING));

            sniper.AuctionClosed();

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 2345, 0, SniperState.LOST));
        }

        [Test]
        public void ContinuesToBeLosingOnceStopPriceHasBeenReached()
        {
            createSniperWithStopPrice(1234);

            sniper.CurrentPrice(2345, 45, PriceSource.FromOtherBidder);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 2345, 0, SniperState.LOSING));

            sniper.CurrentPrice(2390, 45, PriceSource.FromOtherBidder);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 2390, 0, SniperState.LOSING));
        }

        [Test]
        public void DoesNotBidAndReportsLosingIfPriceAfterWinningIsAboveStopPrice()
        {
            createSniperWithStopPrice(200);

            sniper.CurrentPrice(100, 45, PriceSource.FromOtherBidder);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 100, 145, SniperState.BIDDING));

            sniper.CurrentPrice(145, 45, PriceSource.FromSniper);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 145, 145, SniperState.WINNING));

            sniper.CurrentPrice(210, 45, PriceSource.FromOtherBidder);

            sniperListener.Received(1).SniperStateChanged(new SniperSnapshot(item.Identifier, 210, 145, SniperState.LOSING));
            sniperListener.DidNotReceive().SniperStateChanged(new SniperSnapshot(item.Identifier, 210, 255, SniperState.BIDDING));
        }

        [Test]
        public void ReportsFailedIfAuctionFailsWhenBidding()
        {
            int price = 1001;
            int increment = 25;
            int bid = price + increment;

            sniper.CurrentPrice(price, increment, PriceSource.FromOtherBidder);
            sniperListener.Received().SniperStateChanged(new SniperSnapshot(item.Identifier, price, bid, SniperState.BIDDING));

            sniper.CurrentPrice(bid, increment, PriceSource.FromOtherBidder);
            sniper.AuctionFailed();
            sniperListener.Received().SniperStateChanged(new SniperSnapshot(item.Identifier, 0, 0, SniperState.FAILED));
        }
    }
}

