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
        private string itemId;

        internal void StartBiddingIn(FakeAuctionServer auction)
        {
            itemId = auction.ItemId;

            driver = new AuctionSniperDriver(Program.APPLICATION_TITLE, 1000, new string[] { XMPP_HOSTNAME, XMPP_PORT, SNIPER_ID, SNIPER_PASSWORD, auction.ItemId });
            driver.ShowsSniperStatus(itemId, 0, 0, Status.STATUS_JOINING);
            driver.HasTitle(Program.APPLICATION_TITLE);
            driver.HasColumnTitles();

        }
        internal void Stop()
        {
            if (driver != null)
                driver.dispose();
        }

        internal void HasShownSniperIsBidding(int lastPrice, int lastBid)
        {
            driver.ShowsSniperStatus(itemId, lastPrice, lastBid, Status.STATUS_BIDDING);
        }

        internal void ShowSniperHasWonAuction(int lastPrice)
        {
            driver.ShowsSniperStatus(itemId, lastPrice, lastPrice, Status.STATUS_WON);
        }

        internal void HasShownSniperIsWinning(int winningBid)
        {
            driver.ShowsSniperStatus(itemId, winningBid, winningBid, Status.STATUS_WINNING);
        }
    }
}
