using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper
{
    public class Status
    {
        public static readonly string STATUS_LOST = "Lost";
        public static readonly string STATUS_JOINING = "Joining";
        public static readonly string STATUS_BIDDING = "Bidding";
        public static readonly string STATUS_WINNING = "Winning";
        public static readonly string STATUS_WON = "Won";

        public static string GetStateText(SniperState state)
        {
            if (state == SniperState.JOINING)
                return STATUS_JOINING;
            if (state == SniperState.BIDDING)
                return STATUS_BIDDING;
            if (state == SniperState.WINNING)
                return STATUS_WINNING;
            if (state == SniperState.WON)
                return STATUS_WON;

            return STATUS_LOST;

        }
    }
}
