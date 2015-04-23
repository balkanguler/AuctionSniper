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
        private string itemId;

        public string ItemId
        {
            get { return itemId; }
        }

        public AuctionSniper(string itemId, IAuction auction)
        {
            this.itemId = itemId;
            this.auction = auction;
            this.snapShot = SniperSnapshot.Joining(itemId);
        }

        public void AuctionClosed()
        {
            snapShot = snapShot.Closed();

            Console.Write("auction closed: state: " + snapShot.State.ToString());
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
                    auction.Bid(bid);
                    snapShot = snapShot.Bidding(price, bid);
                    break;
            }

            notifyChange();
        }

        private void notifyChange()
        {
            if (sniperListener != null)
                sniperListener.SniperStateChanged(snapShot);
        }


        public SniperSnapshot SnapShot { get { return snapShot; } }
    }
}
