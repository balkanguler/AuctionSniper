using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper
{
    public interface ISniperListener
    {
        void SniperLost();

        void SniperBidding();

        void SniperWinning();
    }
}
