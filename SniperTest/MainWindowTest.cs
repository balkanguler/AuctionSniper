using NSubstitute;
using NUnit.Framework;
using AuctionSniper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionSniper.Test
{
    [TestFixture]
    public class MainWindowTest
    {
        private  SniperTableModel tableModel;
        private  MainWindow mainWindow;
        [SetUp]
        public void SetUp()
        {
            tableModel = new SniperTableModel();
            mainWindow = new MainWindow(tableModel, null);
        }

        public void MakeUserRequestsWhenJoinButtonClicked()
        {
            IUserRequestListener requestListenerMock = Substitute.For<IUserRequestListener>();

            mainWindow.AddUserRequestListener(requestListenerMock);

            mainWindow.TbItemId.Text = "an item-id";
            mainWindow.BtnJoin.PerformClick();

            requestListenerMock.Received().JoinAuction(Arg.Any<string>(), "an item-id");
        }
    }
}
