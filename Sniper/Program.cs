using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sniper
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static Form1 form;

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            if (args != null && args.Length > 0)
                form.Start(args[0], args[1], args[2], args[3], args[4]);

            Application.Run(form);

          //  form.Start(args[0], args[1], args[2], args[3], args[4]);
            
        }

        public static void Start(string XMPP_HOSTNAME, string XMPP_PORT, string SNIPER_ID, string SNIPER_PASSWORD, string itemId)
        {   
           form.Start(XMPP_HOSTNAME, XMPP_PORT, SNIPER_ID, SNIPER_PASSWORD, itemId);
        }
    }
}
