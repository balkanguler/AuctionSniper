using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public class CommandFormat
    {
        public static readonly string JOIN_COMMAND_FORMAT = "SOLVersion: 1.1; Event: JOIN;";
        public static readonly string REPORT_PRICE_COMMAND_FORMAT = "SOLVersion: 1.1; Event: PRICE; CurrentPrice: {0}; Increment: {1}; Bidder: {2};";
        public static readonly string BID_COMMAND_FORMAT = "SOLVersion: 1.1; Event: BID; Price: {0};";
        public static readonly string CLOSE_COMMAND_FORMAT = "SOLVersion: 1.1; Event: CLOSE;";
    }
}
