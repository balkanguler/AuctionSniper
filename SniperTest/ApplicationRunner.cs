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
        Thread thread;


        internal void StartBiddingIn(FakeAuctionServer auction)
        {
            thread = new Thread(new ParameterizedThreadStart((o) =>{

                Program.Main(new string[] {XMPP_HOSTNAME, XMPP_PORT, SNIPER_ID, SNIPER_PASSWORD, auction.ItemId});
                
            }));

            thread.Start();


            driver = new AuctionSniperDriver(1000);
            driver.showSniperStatus(Status.STATUS_JOINING);

        }

        internal void ShowSniperHasLostAuction()
        {
            driver.showSniperStatus(Status.STATUS_LOST);
        }

        internal void Stop()
        {
            if (driver != null)
                driver.dispose();

            if (thread != null && thread.ThreadState == System.Threading.ThreadState.Running)
                thread.Abort();
        }


        internal void HasShownSniperIsBidding()
        {
            driver.showSniperStatus(Status.STATUS_BIDDING);
        }

        internal void ShowSniperHasWonAuction()
        {
            driver.showSniperStatus(Status.STATUS_WON);
        }

        internal void HasShownSniperIsWinning()
        {
            driver.showSniperStatus(Status.STATUS_WINNING);
        }
    }
}
