using agsXMPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public interface IUserRequestListener
    {
        void JoinAuction(XmppClientConnection connection, SniperTableModel snipers, string itemId);
    }
}
