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
        }

        internal void showSniperStatus(string expectedStatus)
        {
            Assert.IsNotNull(application, "application is null");

            List<Window> windows = application.GetWindows();

            Window window = application.GetWindow("Form1", InitializeOption.NoCache);

            Assert.IsNotNull(window, "window is null");


            Label statusLabel = window.Get<Label>("lblStatus");

            Assert.IsNotNull(statusLabel, "statusLabel is null");

            StringAssert.AreEqualIgnoringCase(expectedStatus, statusLabel.Text);
        }

        internal void dispose()
        {
            //application.Close();
        }
    }
}
