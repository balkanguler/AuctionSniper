using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public class SniperTableModel : DataTable, IPortfolioListener
    {
        static readonly SniperSnapshot STARTING_UP = new SniperSnapshot("", 0, 0, SniperState.JOINING);
        static readonly string[] STATUS_TEXT = new string[] { Status.STATUS_JOINING, Status.STATUS_BIDDING, Status.STATUS_WINNING, Status.STATUS_LOSING,  Status.STATUS_LOST, Status.STATUS_WON, Status.STATUS_FAILED };

        List<SniperSnapshot> snapShots = new List<SniperSnapshot>();

        public SniperTableModel()
        {
            this.Columns.Add("Item");
            this.Columns.Add("Last Price");
            this.Columns.Add("Last Bid");
            this.Columns.Add("State");

           // AddSniper(STARTING_UP);
        }

        public void SniperStateChanged(SniperSnapshot newSnapshot)
        {
            int row = rowMatching(newSnapshot);
            snapShots[row] = newSnapshot;

            setDataRow(row);
        }

        private int rowMatching(SniperSnapshot snapShot)
        {

            for (int i = 0; i < snapShots.Count; i++)
            {
                if (snapShot.IsForSameItemAs(snapShots[i]))
                    return i;
            }

            throw new InvalidOperationException("Can not find match for " + snapShot);
        }

        private void setDataRow(int rowIndex)
        {

            if (this.Rows.Count == rowIndex)
            {
               DataRow row = this.NewRow();
                this.Rows.Add(row);
            }

            this.Rows[rowIndex][(int)Column.ITEM_IDENTIFIER] = snapShots[rowIndex].ItemId;
            this.Rows[rowIndex][(int)Column.LAST_PRICE] = snapShots[rowIndex].LastPrice;
            this.Rows[rowIndex][(int)Column.LAST_BID] = snapShots[rowIndex].LastBid;
            this.Rows[rowIndex][(int)Column.SNIPER_STATE] = STATUS_TEXT[(int)snapShots[rowIndex].State];
        }

        public void AddSniperSnapShot(SniperSnapshot sniperSnapshot)
        {
            snapShots.Add(sniperSnapshot);
            SniperStateChanged(sniperSnapshot);
        }

        public void SniperAdded(AuctionSniper sniper)
        {
            AddSniperSnapShot(sniper.SnapShot);
            sniper.AddSniperListener(new UIThreadSniperListener(this));
        }
    }
}
