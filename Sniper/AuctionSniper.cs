using AuctionSniper.Xmpp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AuctionSniper
{
    public class AuctionSniper : IAuctionEventListener
    {
        private ISniperListener sniperListener;
        private IAuction auction;
        private SniperSnapshot snapShot;

        public AuctionSniper(string itemId, IAuction auction, ISniperListener sniperListener)
        {
            this.auction = auction;
            this.sniperListener = sniperListener;
            this.snapShot = SniperSnapshot.Joining(itemId);
            notifyChange();
        }

        public void AuctionClosed()
        {
            snapShot = snapShot.Closed();

            Console.Write("auction closed: state: " + snapShot.State.ToString());
            notifyChange();
        }



        public void CurrentPrice(int price, int increment, PriceSource priceSource)
        {
            switch (priceSource)
            {
                case PriceSource.FromSniper:
                    snapShot = snapShot.Winning(price);
                    break;
                case PriceSource.FromOtherBidder:
                    int bid = price + increment;
                    auction.Bid(bid);
                    snapShot = snapShot.Bidding(price, bid);
                    break;
            }

            notifyChange();
        }

        private void notifyChange()
        {
            sniperListener.SniperStateChanged(snapShot);
        }

    }
}
