using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public interface IPortfolioListener
    {
        void SniperAdded(AuctionSniper sniper);
    }
}
