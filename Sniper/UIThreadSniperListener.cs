using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionSniper
{
    class UIThreadSniperListener : ISniperListener
    {
        SniperTableModel snipers;
        public UIThreadSniperListener(SniperTableModel snipers)
        {
            this.snipers = snipers;
        }
        public void SniperLost()
        {
           
        }

        public void SniperBidding(SniperSnapshot newSnapShot)
        {
            SniperStateChanged(newSnapShot);
        }

        public void SniperWon()
        {
           
        }

        public void SniperStateChanged(SniperSnapshot newSnapShot)
        {
            snipers.SniperStateChanged(newSnapShot);         
        }
    }
}
