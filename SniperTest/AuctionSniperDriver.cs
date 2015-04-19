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

        private readonly string WINDOW_TITLE;

        public AuctionSniperDriver(string windowTitle, int timeoutMillis, string[] args)
        {
            this.timeoutMillis = timeoutMillis;
            WINDOW_TITLE = windowTitle;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "Sniper.exe";
            psi.Arguments = string.Join(" ", args);
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;



            application = Application.Launch(psi);

            application.Process.OutputDataReceived += new DataReceivedEventHandler(
            (s, e) =>
            {
                Console.WriteLine("***** " + e.Data);
            }
        );
            application.Process.ErrorDataReceived += new DataReceivedEventHandler((s, e) => { Console.WriteLine(e.Data); });

            application.Process.BeginOutputReadLine();


        }

        internal void ShowsSniperStatus(string itemId, int lastPrice, int lastBid, string expectedStatus)
        {
            Assert.IsNotNull(application, "application is null");

            List<Window> windows = application.GetWindows();

            Window window = application.GetWindow(WINDOW_TITLE, InitializeOption.NoCache);

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

        internal void HasTitle(string title)
        {
            Window window = application.GetWindow(WINDOW_TITLE, InitializeOption.NoCache);

            StringAssert.AreEqualIgnoringCase(title, window.Title);
        }

        internal void HasColumnTitles()
        {
            Window window = application.GetWindow(WINDOW_TITLE, InitializeOption.NoCache);

            Assert.IsNotNull(window, "window is null");

            System.Diagnostics.Debug.WriteLine("--- items ---");
            window.Items.ForEach(i => System.Diagnostics.Debug.WriteLine(i.Id.ToString() + "  " + i.GetType()));

            var table = window.Get<Table>(SearchCriteria.ByAutomationId("gvSniper"));

            StringAssert.AreEqualIgnoringCase("Item", table.Header.Columns[0].Name);
            StringAssert.AreEqualIgnoringCase("Last Price", table.Header.Columns[1].Name);
            StringAssert.AreEqualIgnoringCase("Last Bid", table.Header.Columns[2].Name);
            StringAssert.AreEqualIgnoringCase("State", table.Header.Columns[3].Name);
        }
    }
}
