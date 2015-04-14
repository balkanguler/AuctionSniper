using NUnit.Framework;
using Rhino.Mocks;
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
            sniperListener = MockRepository.GenerateMock<ISniperListener>();
            auction = MockRepository.GenerateMock<IAuction>();

            sniper = new AuctionSniper(auction, sniperListener);
        }

        [Test]
        public void ReportstLostWhenAuctionClosesImmediately()
        {
            sniperListener.Expect(sl => sl.SniperLost());

            sniper.AuctionClosed();

            sniperListener.VerifyAllExpectations();
        }

        [Test]
        public void ReportstLostWhenAuctionClosesWhenBidding()
        {
            sniperListener.Expect(sl => sl.SniperLost());

            sniper.CurrentPrice(123, 45, PriceSource.FromOtherBidder);

            sniperListener.VerifyAllExpectations();
        }

        [Test]
        public void ReportsIsWinningWhenCurrentPriceComesFromSniper()
        {
            sniperListener.Expect(sl => sl.SniperWinning());

            sniper.CurrentPrice(123, 45, PriceSource.FromSniper);

            sniperListener.VerifyAllExpectations();
        }

        [Test]
        public void BidsHigherAndReportsBiddingWhenNewPriceArrives()
        {
            int price = 1001;
            int increment = 25;

            auction.Expect(a => a.Bid(price + increment));
            sniperListener.Expect(sl => sl.SniperBidding());

            sniper.CurrentPrice(price, increment, PriceSource.FromOtherBidder);

            auction.VerifyAllExpectations();
            sniperListener.VerifyAllExpectations();

        }
    }
}
