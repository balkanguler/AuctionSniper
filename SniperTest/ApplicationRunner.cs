using AuctionSniper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace AuctionSniper.Test
{
    class ApplicationRunner
    {
        AuctionLogDriver logDriver = new AuctionLogDriver();
        AuctionSniperDriver driver;

        public static readonly string SNIPER_ID = "sniper";
        public static readonly string SNIPER_PASSWORD = "sniper";
        public static readonly string XMPP_HOSTNAME = "SERVER";
        public static readonly string XMPP_PORT = "5222";
        public static readonly string SNIPER_XMPP_ID = SNIPER_ID;

        internal void StartBiddingIn(params FakeAuctionServer[] auctions)
        {
            logDriver.ClearLog();
            driver = new AuctionSniperDriver(1000, constructArguments(auctions, Int32.MaxValue));

            driver.HasTitle(Program.APPLICATION_TITLE);
            driver.HasColumnTitles();

            foreach (var auction in auctions)
            {
                driver.StartBiddingFor(auction.Item);
                driver.ShowsSniperStatus(auction.Item, 0, 0, Status.STATUS_JOINING);
            }
        }

        internal void StartBiddingWithStopPrice(int stopPrice, params FakeAuctionServer[] auctions)
        {
            logDriver.ClearLog();

            driver = new AuctionSniperDriver(1000, constructArguments(auctions, stopPrice));

            driver.HasTitle(Program.APPLICATION_TITLE);
            driver.HasColumnTitles();

            foreach (var auction in auctions)
            {
                driver.StartBiddingFor(auction.Item);
                driver.ShowsSniperStatus(auction.Item, 0, 0, Status.STATUS_JOINING);
            }
        }
                
        private static string[] constructArguments(FakeAuctionServer[] auctions, int stopPrice)
        {
            List<string> args = new List<string>();

            args.AddRange(new string[] {XMPP_HOSTNAME, XMPP_PORT, SNIPER_ID, SNIPER_PASSWORD});
            foreach (var auction in auctions)
                args.Add(auction.Item.Identifier);

            args.Add(stopPrice.ToString());
            return args.ToArray();
        }
        internal void Stop()
        {
            if (driver != null)
                driver.dispose();            
        }
        internal void HasShownSniperIsBidding(FakeAuctionServer auction, int lastPrice, int lastBid)
        {
            driver.ShowsSniperStatus(auction.Item, lastPrice, lastBid, Status.STATUS_BIDDING);
        }

        internal void ShowSniperHasWonAuction(FakeAuctionServer auction, int lastPrice)
        {
            driver.ShowsSniperStatus(auction.Item, lastPrice, lastPrice, Status.STATUS_WON);
        }

        internal void HasShownSniperIsWinning(FakeAuctionServer auction, int winningBid)
        {
            driver.ShowsSniperStatus(auction.Item, winningBid, winningBid, Status.STATUS_WINNING);
        }        
        internal void HasShownSniperIsLosing(FakeAuctionServer auction, int lastPrice, int lastBid)
        {
            driver.ShowsSniperStatus(auction.Item, lastPrice, lastBid, Status.STATUS_LOSING);
        }

        internal void ShowsSniperHasLostAuction(FakeAuctionServer auction, int lastPrice, int lastBid)
        {
            driver.ShowsSniperStatus(auction.Item, lastPrice, lastBid, Status.STATUS_LOST);
        }

        internal void ReportsInvalidMessage(FakeAuctionServer auction, string brokenMessage)
        {
            logDriver.HasEntry(brokenMessage);
        }

        internal void ShowsSniperHasFailed(FakeAuctionServer auction)
        {
            driver.ShowsSniperStatus(auction.Item, 0, 0, Status.STATUS_FAILED);
        }
    }
}
