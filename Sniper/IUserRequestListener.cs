using agsXMPP;
using AuctionSniper.Xmpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public interface IUserRequestListener
    {
        void JoinAuction(string sniperId, Item item);
    }
}
