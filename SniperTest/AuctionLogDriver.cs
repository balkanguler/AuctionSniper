using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Test
{
    public class AuctionLogDriver
    {
        public static readonly string LOG_FILE_NAME = "auction-sniper.log";

        public void HasEntry(string expectedEntry)
        {
            Assert.IsTrue(File.ReadAllText(LOG_FILE_NAME).Contains(expectedEntry));
        }

        public void ClearLog()
        {
            File.Delete(LOG_FILE_NAME);
        }
    }
}
