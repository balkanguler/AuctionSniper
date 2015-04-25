using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public class Status
    {
        public static readonly string STATUS_LOST = "Lost";
        public static readonly string STATUS_JOINING = "Joining";
        public static readonly string STATUS_BIDDING = "Bidding";
        public static readonly string STATUS_WINNING = "Winning";
        public static readonly string STATUS_WON = "Won";
        public static readonly string STATUS_LOSING = "Losing";
        public static readonly string STATUS_FAILED = "Failed";

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
            if (state == SniperState.LOSING)
                return STATUS_LOSING;
            if (state == SniperState.FAILED)
                return STATUS_FAILED;

            return STATUS_LOST;
        }
    }
}
