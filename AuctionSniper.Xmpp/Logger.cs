using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public class Logger : ILogger
    {
        public static readonly string LOG_FILE_NAME = "auction-sniper.log";
        public void Severe(string message)
        {
            File.WriteAllText(LOG_FILE_NAME, message);
        }
    }
}
