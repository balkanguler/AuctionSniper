using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper
{
    class XMPPAuction : IAuction
    {
        public Chat Chat { get; set; }


        public XMPPAuction()
        {
        }

        public void Bid(int amount)
        {
            SendMessage(string.Format(CommandFormat.BID_COMMAND_FORMAT, amount));
        }

        public void Join()
        {
            SendMessage(string.Format(CommandFormat.JOIN_COMMAND_FORMAT));
        }

        private void SendMessage(string message)
        {
            if (Chat != null)
                Chat.SendMessage(message);
        }
    }
}
