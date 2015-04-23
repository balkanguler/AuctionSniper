using AuctionSniper.Xmpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionSniper
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static readonly string APPLICATION_TITLE = "Auction Sniper";
        public static readonly string NEW_ITEM_ID_NAME = "item id";
        public static readonly string JOIN_BUTTON_NAME = "Join";

        static MainWindow form;
        
        private static readonly int ARG_HOSTNAME = 0;
        private static readonly int ARG_PORT = 1;
        private static readonly int ARG_USERNAME = 2;
        private static readonly int ARG_PASSWORD = 3;        
        

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SniperTableModel tableModel = new SniperTableModel();

            form = new MainWindow(tableModel, args[ARG_USERNAME]);
            XMPPAuctionHouse auctionHouse = XMPPAuctionHouse.Connect(args[ARG_HOSTNAME], args[ARG_PORT], args[ARG_USERNAME], args[ARG_PASSWORD]);
            form.AddUserRequestListener(new SniperLauncher(auctionHouse, tableModel));
            Application.Run(form);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Appdomain unhandled exception.");

            Console.WriteLine(e.ToString());
        }

    }
}
