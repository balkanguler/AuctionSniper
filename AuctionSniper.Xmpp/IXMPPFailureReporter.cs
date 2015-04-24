using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public interface IXMPPFailureReporter
    {
        void CannotTranslateMessage(string auctionId, string failedMessage, Exception exception);
    }
}
