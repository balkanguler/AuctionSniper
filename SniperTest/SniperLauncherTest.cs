using AuctionSniper.Xmpp;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Test
{
    [TestFixture]
    public class SniperLauncherTest
    {
        [Test]
        public void AddsNewSniperToCollectorAndThenJoinsAuction()
        {
            string itemId = "item 123";
            IAuctionHouse auctionHouse = Substitute.For<IAuctionHouse>();
            IAuction auction = Substitute.For<IAuction>();
            ISniperCollector collector = Substitute.For<ISniperCollector>();

            auctionHouse.AuctionFor(itemId).Returns(auction);

            auction.When(a => a.Join()).Do(a =>
            {
                auction.Received(1).AddAuctionEventListener(Arg.Is<AuctionSniper>(s => s.ItemId.Equals(itemId)));
                collector.Received(1).AddSniper(Arg.Is<AuctionSniper>(s => s.ItemId.Equals(itemId)));
            });

            SniperLauncher launcher = new SniperLauncher(auctionHouse, collector);
            launcher.JoinAuction(ApplicationRunner.SNIPER_ID, itemId);

            auction.Received(1).Join();


        }
    }
}
