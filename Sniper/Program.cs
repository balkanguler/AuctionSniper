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

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new MainWindow(new SniperTableModel(), args);
            form.AddUserRequestListener(new UserReqeuestListener());
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
