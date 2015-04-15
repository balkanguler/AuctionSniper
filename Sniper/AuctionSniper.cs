﻿using System;
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

        public AuctionSniper(IAuction auction, ISniperListener sniperListener)
        {
            // TODO: Complete member initialization
            this.auction = auction;
            this.sniperListener = sniperListener;
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
                sniperListener.SniperWinning();
            else
            {
                auction.Bid(price + increment);
                sniperListener.SniperBidding();
            }

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
