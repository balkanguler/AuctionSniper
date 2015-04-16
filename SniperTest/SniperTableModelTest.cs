using NUnit.Framework;
using Sniper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperTest
{
    [TestFixture]
    public class SniperTableModelTest
    {
        private SniperTableModel model = new SniperTableModel();

        [Test]
        public void HasEnoughColumns()
        {
            Assert.AreEqual(Enum.GetNames(typeof(Column)).Length, model.Columns.Count);
        }

        [Test]
        public void SetSniperValuesInColumns()
        {
            model.SniperStatusChanged(new SniperSnapshot("item id", 555, 666, SniperState.BIDDING));

            assertColumnEquals(Column.ITEM_IDENTIFIER, "item id");
            assertColumnEquals(Column.LAST_PRICE, 555.ToString());
            assertColumnEquals(Column.LAST_BID, 666.ToString());
            assertColumnEquals(Column.SNIPER_STATE, Status.STATUS_BIDDING);
        }

        private void assertColumnEquals(Column column, object expected)
        {
            int rowIndex = 0;
            int columnIndex = (int)column;

            Assert.AreEqual(expected, model.Rows[rowIndex][columnIndex]);
        }

    }
}
