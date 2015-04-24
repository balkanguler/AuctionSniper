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
            
            Item item = new Item("item 123", Int32.MaxValue);

            IAuctionHouse auctionHouse = Substitute.For<IAuctionHouse>();
            IAuction auction = Substitute.For<IAuction>();
            ISniperCollector collector = Substitute.For<ISniperCollector>();

            auctionHouse.AuctionFor(item).Returns(auction);

            auction.When(a => a.Join()).Do(a =>
            {
                auction.Received(1).AddAuctionEventListener(Arg.Is<AuctionSniper>(s => s.Item.Equals(item)));
                collector.Received(1).AddSniper(Arg.Is<AuctionSniper>(s => s.Item.Equals(item)));
            });

            SniperLauncher launcher = new SniperLauncher(auctionHouse, collector);
            launcher.JoinAuction(ApplicationRunner.SNIPER_ID, item);

            auction.Received(1).Join();


        }
    }
}
