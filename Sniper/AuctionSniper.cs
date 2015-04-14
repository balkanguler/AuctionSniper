using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sniper
{
    public class AuctionSniper : IAuctionEventListener
    {
        private ISniperListener sniperListener;
        private IAuction auction;

        public AuctionSniper(IAuction auction, ISniperListener sniperListener)
        {
            // TODO: Complete member initialization
            this.auction = auction;
            this.sniperListener = sniperListener;
        }

        public void AuctionClosed()
        {
            sniperListener.SniperLost();
        }


        public void CurrentPrice(int price, int increment, PriceSource priceSource)
        {
            auction.Bid(price + increment);
            sniperListener.SniperBidding();

            switch (priceSource)
            {
                case PriceSource.FromSniper:
                    sniperListener.SniperWinning();
                    break;
                case PriceSource.FromOtherBidder:
                    auction.Bid(price + increment);
                    sniperListener.SniperBidding();
                    break;
            }
        }
    }
}
