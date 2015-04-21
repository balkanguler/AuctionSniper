using Sniper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SniperTest
{
    class ApplicationRunner
    {
        public static readonly string SNIPER_ID = "sniper";
        public static readonly string SNIPER_PASSWORD = "sniper";
        public static readonly string XMPP_HOSTNAME = "ZT0804N01";
        public static readonly string XMPP_PORT = "5222";
        public static readonly string SNIPER_XMPP_ID = SNIPER_ID;

        private AuctionSniperDriver driver;
       

        internal void StartBiddingIn(params FakeAuctionServer[] auctions)
        {
            driver = new AuctionSniperDriver(1000, constructArguments(auctions));

            driver.HasTitle(Program.APPLICATION_TITLE);
            driver.HasColumnTitles();


            foreach (var auction in auctions)
            {
                driver.StartBiddingFor(auction.ItemId);
                driver.ShowsSniperStatus(auction.ItemId, 0, 0, Status.STATUS_JOINING);
            }
        }

        
        private static string[] constructArguments(FakeAuctionServer[] auctions)
        {
            List<string> args = new List<string>();

            args.AddRange(new string[] {XMPP_HOSTNAME, XMPP_PORT, SNIPER_ID, SNIPER_PASSWORD});
            foreach (var auction in auctions)
                args.Add(auction.ItemId);

            return args.ToArray();
        }
        internal void Stop()
        {
            if (driver != null)
                driver.dispose();            
        }

        internal void HasShownSniperIsBidding(FakeAuctionServer auction, int lastPrice, int lastBid)
        {
            driver.ShowsSniperStatus(auction.ItemId, lastPrice, lastBid, Status.STATUS_BIDDING);
        }

        internal void ShowSniperHasWonAuction(FakeAuctionServer auction, int lastPrice)
        {
            driver.ShowsSniperStatus(auction.ItemId, lastPrice, lastPrice, Status.STATUS_WON);
        }

        internal void HasShownSniperIsWinning(FakeAuctionServer auction, int winningBid)
        {
            driver.ShowsSniperStatus(auction.ItemId, winningBid, winningBid, Status.STATUS_WINNING);
        }
    }
}
