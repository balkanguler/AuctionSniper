using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public class SniperPortfolio : ISniperCollector
    {
        List<IPortfolioListener> listeners = new List<IPortfolioListener>();
        public void AddPortfolioListener(IPortfolioListener listener)
        {
            listeners.Add(listener);
        }

        public void AddSniper(AuctionSniper sniper)
        {
            listeners.ForEach(l => l.SniperAdded(sniper));
        }
    }
}
