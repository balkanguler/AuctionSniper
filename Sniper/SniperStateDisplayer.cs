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

        public void SniperBidding(SniperSnapshot newSnapShot)
        {
            SniperStateChanged(newSnapShot);
        }
        private void showStatus(string status)
        {
            form.ShowStatus(status);
        }

        public void SniperWon()
        {
            showStatus(Status.STATUS_WON);
        }

        public void SniperStateChanged(SniperSnapshot newSnapShot)
        {
            form.SniperStatusChanged(newSnapShot);
        }
    }
}
