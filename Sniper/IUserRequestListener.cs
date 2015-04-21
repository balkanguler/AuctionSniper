using agsXMPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper
{
    public interface IUserRequestListener
    {
        void JoinAuction(XmppClientConnection connection, string sniperId, string xmppHostname, SniperTableModel snipers, string itemId);
    }
}
