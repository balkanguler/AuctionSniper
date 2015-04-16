﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper
{
    public class SniperSnapshot
    {
        public readonly string ItemId;
        public readonly int LastPrice;
        public readonly int LastBid;
        public readonly SniperState State;
        public SniperSnapshot(string itemId, int lastPrice, int lastBid, SniperState state)
        {
            this.ItemId = itemId;
            this.LastPrice = lastPrice;
            this.LastBid = lastBid;
            this.State = state;
        }

        internal static SniperSnapshot Joining(string itemId)
        {
            return new SniperSnapshot(itemId, 0, 0, SniperState.JOINING);
        }

        internal SniperSnapshot Winning(int newLastPrice)
        {
            return new SniperSnapshot(ItemId, newLastPrice, LastBid, SniperState.WINNING);
        }

        internal SniperSnapshot Bidding(int newLastPrice, int newLastBid)
        {
            return new SniperSnapshot(ItemId, newLastPrice, newLastBid, SniperState.BIDDING);
        }
    }
}
