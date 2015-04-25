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
        private Item item;

        public Item Item
        {
            get
            {
                return item;
            }
        }
        public SniperSnapshot SnapShot
        {
            get
            {
                return snapShot;
            }
        }

        public AuctionSniper(Item item, IAuction auction)
        {
            this.item = item;
            this.auction = auction;
            this.snapShot = SniperSnapshot.Joining(item.Identifier);
        }

        public void AuctionClosed()
        {
            snapShot = snapShot.Closed();
            notifyChange();
        }

        public void AddSniperListener(ISniperListener sniperListener)
        {
            this.sniperListener = sniperListener;
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
                    if (item.AllowsBid(bid))
                    {
                        auction.Bid(bid);
                        snapShot = snapShot.Bidding(price, bid);
                    }
                    else
                    {
                        snapShot = snapShot.Losing(price);
                    }
                    break;
            }

            notifyChange();
        }

        private void notifyChange()
        {
            if (sniperListener != null)
                sniperListener.SniperStateChanged(snapShot);
        }
        
        public void AuctionFailed()
        {
            snapShot = snapShot.Failed();
            notifyChange();
        }
    }
}
