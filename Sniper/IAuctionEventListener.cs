using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sniper
{
    public interface IAuctionEventListener
    {       
        void AuctionClosed();

        void CurrentPrice(int price, int increment, PriceSource priceSource);
    }
}
