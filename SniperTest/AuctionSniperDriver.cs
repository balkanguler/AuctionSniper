using NUnit.Framework;
using Sniper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;

namespace SniperTest
{
    class AuctionSniperDriver
    {
        private int timeoutMillis;
        TestStack.White.Application application;

        public AuctionSniperDriver(int timeoutMillis)
        {
            this.timeoutMillis = timeoutMillis;

            Thread.Sleep(1000);

            application = Application.Attach(Process.GetCurrentProcess().Id);
            //application = Application.Attach(12392);
        }

        public AuctionSniperDriver(int timeoutMillis, string[] args)
        {
            this.timeoutMillis = timeoutMillis;
            
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "Sniper.exe";
            psi.Arguments = string.Join(" ", args);

            application = Application.Launch(psi);
        }

        internal void showSniperStatus(string itemId, int lastPrice, int lastBid, string expectedStatus)
        {
            Assert.IsNotNull(application, "application is null");

            List<Window> windows = application.GetWindows();

            Window window = application.GetWindow("Form1", InitializeOption.NoCache);

            Assert.IsNotNull(window, "window is null");

            System.Diagnostics.Debug.WriteLine("--- items ---");
            window.Items.ForEach(i => System.Diagnostics.Debug.WriteLine(i.Id.ToString() + "  " + i.GetType()));

            var table = window.Get<Table>(SearchCriteria.ByAutomationId("gvSniper"));            
            
            Assert.IsNotNull(table, "table is null");

            StringAssert.AreEqualIgnoringCase(itemId, table.Rows[0].Cells[(int)Column.ITEM_IDENTIFIER].Value.ToString());
            StringAssert.AreEqualIgnoringCase(lastPrice.ToString(), table.Rows[0].Cells[(int)Column.LAST_PRICE].Value.ToString());
            StringAssert.AreEqualIgnoringCase(lastBid.ToString(), table.Rows[0].Cells[(int)Column.LAST_BID].Value.ToString());
            StringAssert.AreEqualIgnoringCase(expectedStatus, table.Rows[0].Cells[(int)Column.SNIPER_STATE].Value.ToString());
        }

     

        internal void dispose()
        {
            application.Close();
        }
    }
}
