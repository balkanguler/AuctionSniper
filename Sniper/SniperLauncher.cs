﻿using agsXMPPChat;
using AuctionSniper.Xmpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public class SniperLauncher : IUserRequestListener
    {
        List<Chat> chats = new List<Chat>();
        ISniperCollector collector;
        IAuctionHouse auctionHouse;
        public SniperLauncher(IAuctionHouse auctionHouse, ISniperCollector collector)
        {
            this.auctionHouse = auctionHouse;
            this.collector = collector;
        }

        public void JoinAuction(string sniperId, Item item)
        {
            List<IAuctionEventListener> eventListeners = new List<IAuctionEventListener>();
            IAuction auction = auctionHouse.AuctionFor(item);

            AuctionSniper sniper = new AuctionSniper(item, auction);
            auction.AddAuctionEventListener(sniper);
            eventListeners.Add(sniper);

            collector.AddSniper(sniper);
            auction.Join();
        }
    }
}
