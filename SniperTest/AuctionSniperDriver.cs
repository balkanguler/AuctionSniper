using NUnit.Framework;
using AuctionSniper;
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

namespace AuctionSniper.Test
{
    class AuctionSniperDriver
    {
        int timeoutMillis;
        TestStack.White.Application application;
        Window window;
        TextBox itemIdField;
        Button bidButton;
        Process process;
        public AuctionSniperDriver(int timeoutMillis, string[] args)
        {
            this.timeoutMillis = timeoutMillis;

            int processId = startSniper(args);

            application = Application.Attach(processId);

            Assert.IsNotNull(application, "application is null");

            window = application.GetWindow(Program.APPLICATION_TITLE, InitializeOption.NoCache);

            Assert.IsNotNull(window, "window is null");

            itemIdField = window.Get<TextBox>(Program.NEW_ITEM_ID_NAME);
            bidButton = window.Get<Button>(Program.JOIN_BUTTON_NAME);
        }

        private int startSniper(string[] args)
        {
            process = new Process();

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "AuctionSniper.exe";
            psi.Arguments = string.Join(" ", args);
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;

            process.StartInfo = psi;

            process.Start();

            process.OutputDataReceived += new DataReceivedEventHandler(
                (s, e) =>
                {
                    Console.WriteLine("***** " + e.Data);
                }
            );
            process.ErrorDataReceived += new DataReceivedEventHandler((s, e) => { Console.WriteLine(e.Data); });

            process.BeginOutputReadLine();

            return process.Id;

        }
        internal void ShowsSniperStatus(string itemId, int lastPrice, int lastBid, string expectedStatus)
        {
            var table = window.Get<Table>(SearchCriteria.ByAutomationId("gvSniper"));

            Assert.IsNotNull(table, "table is null");

            bool itemFound = false;
            for (var i = 0; i < table.Rows.Count; i++)
            {
                Console.WriteLine("table item id: " + table.Rows[i].Cells[(int)Column.ITEM_IDENTIFIER].Value.ToString());

                if (itemId.Equals(table.Rows[i].Cells[(int)Column.ITEM_IDENTIFIER].Value.ToString()))
                {
                    itemFound = true;
                    StringAssert.AreEqualIgnoringCase(lastPrice.ToString(), table.Rows[i].Cells[(int)Column.LAST_PRICE].Value.ToString());
                    StringAssert.AreEqualIgnoringCase(lastBid.ToString(), table.Rows[i].Cells[(int)Column.LAST_BID].Value.ToString());
                    StringAssert.AreEqualIgnoringCase(expectedStatus, table.Rows[i].Cells[(int)Column.SNIPER_STATE].Value.ToString());
                }
            }

            if (!itemFound)
                throw new InvalidOperationException("Item not found in table. Item: " + itemId);

        }

        internal void dispose()
        {
            application.Close();
            if (!process.HasExited)
                process.Kill();
        }

        internal void HasTitle(string title)
        {
            StringAssert.AreEqualIgnoringCase(title, window.Title);
        }

        internal void HasColumnTitles()
        {
            var table = window.Get<Table>(SearchCriteria.ByAutomationId("gvSniper"));

            StringAssert.AreEqualIgnoringCase("Item", table.Header.Columns[0].Name);
            StringAssert.AreEqualIgnoringCase("Last Price", table.Header.Columns[1].Name);
            StringAssert.AreEqualIgnoringCase("Last Bid", table.Header.Columns[2].Name);
            StringAssert.AreEqualIgnoringCase("State", table.Header.Columns[3].Name);
        }

        internal void StartBiddingFor(string itemId)
        {
            itemIdField.Text = itemId;
            bidButton.Click();
        }
    }
}
