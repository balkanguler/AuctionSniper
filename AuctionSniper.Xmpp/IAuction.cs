using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionSniper.Xmpp
{
    public interface IAuction
    {
        Chat Chat { get; set; }
        void Bid(int amount);
        void Join();
        void AddAuctionEventListener(IAuctionEventListener auctionEventListener);
    }
}
