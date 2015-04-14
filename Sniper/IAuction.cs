using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sniper
{
    public interface IAuction
    {
        Chat Chat { get; set; }
        void Bid(int amount);
        void Join();
    }
}
