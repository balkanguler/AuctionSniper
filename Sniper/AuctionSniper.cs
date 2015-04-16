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
        bool isWinning = false;
        private SniperSnapshot snapShot;

        public AuctionSniper(string itemId, IAuction auction, ISniperListener sniperListener)
        {
            this.auction = auction;
            this.sniperListener = sniperListener;
            this.snapShot = SniperSnapshot.Joining(itemId);
            sniperListener.SniperStateChanged(snapShot);
        }

        public void AuctionClosed()
        {
            if (isWinning)
                sniperListener.SniperWon();
            else
                sniperListener.SniperLost();
        }


        public void CurrentPrice(int price, int increment, PriceSource priceSource)
        {
            isWinning = priceSource == PriceSource.FromSniper;
            if (isWinning)
                snapShot = snapShot.Winning(price);
            else
            {
                int bid = price + increment;
                auction.Bid(bid);
                snapShot = snapShot.Bidding(price, bid);
            }
            sniperListener.SniperStateChanged(snapShot);
        }
    }
}
