using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
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

        public static SniperSnapshot Joining(string itemId)
        {
            return new SniperSnapshot(itemId, 0, 0, SniperState.JOINING);
        }

        public SniperSnapshot Winning(int newLastPrice)
        {
            return new SniperSnapshot(ItemId, newLastPrice, LastBid, SniperState.WINNING);
        }

        public SniperSnapshot Bidding(int newLastPrice, int newLastBid)
        {
            return new SniperSnapshot(ItemId, newLastPrice, newLastBid, SniperState.BIDDING);
        }

        public SniperSnapshot Closed()
        {
            return new SniperSnapshot(ItemId, LastPrice, LastBid, whenAuctionClosed());

        }

        private SniperState whenAuctionClosed()
        {
            Console.WriteLine("whenAuctionClosed. State: " + State.ToString());
            if (State == SniperState.JOINING)
                return SniperState.LOST;
            if (State == SniperState.BIDDING)
                return SniperState.LOST;
            if (State == SniperState.WINNING)
                return SniperState.WON;
            if (State == SniperState.LOSING)
                return SniperState.LOST;

            //Lost, WON
            throw new InvalidOperationException("Auction is already closed");
        }

        internal bool IsForSameItemAs(SniperSnapshot sniperSnapshot)
        {
            return ItemId.Equals(sniperSnapshot.ItemId);
        }

        public override bool Equals(object obj)
        {
            SniperSnapshot snapShot = obj as SniperSnapshot;

            if (snapShot == null)
                return false;

            return this.ItemId.Equals(snapShot.ItemId) && this.LastBid == snapShot.LastBid && this.LastPrice == snapShot.LastPrice && this.State == snapShot.State;
        }


        public SniperSnapshot Losing(int price)
        {
            return new SniperSnapshot(ItemId, price, LastBid, SniperState.LOSING);
        }

        internal SniperSnapshot Failed()
        {
            return new SniperSnapshot(ItemId, 0, 0, SniperState.FAILED);
        }
    }
}
