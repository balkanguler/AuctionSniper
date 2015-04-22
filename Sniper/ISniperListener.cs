using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public interface ISniperListener
    {
        void SniperLost();

        void SniperBidding(SniperSnapshot newState);

        void SniperWon();

        void SniperStateChanged(SniperSnapshot sniperSnapshot);
    }
}
