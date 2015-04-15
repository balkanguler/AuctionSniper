using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sniper
{
    class SniperStateDisplayer : ISniperListener
    {
        Form1 form;
        public SniperStateDisplayer(Form1 form)
        {
            this.form = form;
        }
        public void SniperLost()
        {
            showStatus(Status.STATUS_LOST);
        }

        public void SniperBidding()
        {
            showStatus(Status.STATUS_BIDDING);
        }

        public void SniperWinning()
        {
            showStatus(Status.STATUS_WINNING);
        }

        private void showStatus(string status)
        {
            form.ShowStatus(status);
        }


        public void SniperWon()
        {
            showStatus(Status.STATUS_WON);
        }
    }
}
