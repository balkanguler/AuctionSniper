using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public class LoggingXMPPFailureReporter : IXMPPFailureReporter
    {
        private ILogger logger;
        public LoggingXMPPFailureReporter(ILogger logger)
        {
            this.logger = logger;
        }
        public void CannotTranslateMessage(string auctionId, string failedMessage, Exception exception)
        {
            logger.Severe(string.Format("<{0}> Could not translate message {1} because of the exception {2}", auctionId, failedMessage, exception.ToString()));
        }
    }
}
