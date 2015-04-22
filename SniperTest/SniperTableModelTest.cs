using NUnit.Framework;
using AuctionSniper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Test
{
    [TestFixture]
    public class SniperTableModelTest
    {
        private SniperTableModel model;

        [SetUp]
        public void SetUp() {
            model = new SniperTableModel();
        }
        [Test]
        public void HasEnoughColumns()
        {
            Assert.AreEqual(Enum.GetNames(typeof(Column)).Length, model.Columns.Count);
        }

        [Test]
        public void SetSniperValuesInColumns()
        {
            SniperSnapshot joining = SniperSnapshot.Joining("item id");
            SniperSnapshot bidding = joining.Bidding(555, 666);
            model.AddSniper(joining);

            model.SniperStateChanged(bidding);

            assertRowMatchesSnapShot(0, bidding);  
        }

        [Test]
        public void HoldsSnipersInAdditionOrder()
        {
            model.AddSniper(SniperSnapshot.Joining("item 0"));
            model.AddSniper(SniperSnapshot.Joining("item 1"));

            StringAssert.AreEqualIgnoringCase("item 0", cellValue(0, Column.ITEM_IDENTIFIER).ToString());
            StringAssert.AreEqualIgnoringCase("item 1", cellValue(1, Column.ITEM_IDENTIFIER).ToString());
        }

        private object cellValue(int rowIndex, Column column)
        {
            return model.Rows[rowIndex][(int)column];
        }

        private void assertColumnEquals(Column column, int rowIndex, object expected)
        {
            int columnIndex = (int)column;

            Assert.AreEqual(expected, model.Rows[rowIndex][columnIndex]);
        }

        [Test]
        public void NotifiesListenersWhenAddingASniper()
        {
            SniperSnapshot joining = SniperSnapshot.Joining("item123");

            Assert.AreEqual(0, model.Rows.Count);

            model.AddSniper(joining);

            Assert.AreEqual(1, model.Rows.Count);

            assertRowMatchesSnapShot(0, joining);
        }

        private void assertRowMatchesSnapShot(int rowIndex, SniperSnapshot snapShot)
        {
            assertColumnEquals(Column.ITEM_IDENTIFIER, rowIndex, snapShot.ItemId);
            assertColumnEquals(Column.LAST_PRICE, rowIndex, snapShot.LastPrice.ToString());
            assertColumnEquals(Column.LAST_BID, rowIndex, snapShot.LastBid.ToString());
            assertColumnEquals(Column.SNIPER_STATE, rowIndex, Status.GetStateText(snapShot.State));
        }
    }
}
