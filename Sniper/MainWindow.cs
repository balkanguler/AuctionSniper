using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sniper
{
    public partial class MainWindow : Form
    {
        private string XMPP_HOSTNAME;
        private string XMPP_PORT;
        private string SNIPER_ID;
        private string SNIPER_PASSWORD;

        public static readonly string AUCTION_RESOURCE = "Auction";
        SniperTableModel snipers;
        readonly List<IUserRequestListener> userRequests = new List<IUserRequestListener>();

        private XmppClientConnection connection;

        public MainWindow(SniperTableModel tableModel, string[] args)
        {
            InitializeComponent();
            this.Text = Program.APPLICATION_TITLE;
            tbItemId.Name = Program.NEW_ITEM_ID_NAME;
            btnJoin.Name = Program.JOIN_BUTTON_NAME;
            snipers = tableModel;
            gvSniper.DataSource = snipers;

            if (args != null && args.Length > 0)
                connect(args);
        }

        public void AddUserRequestListener(IUserRequestListener userRequestListener)
        {
            userRequests.Add(userRequestListener);
        }
        private void connect(string[] args)
        {
            // TODO: Complete member initialization
            this.XMPP_HOSTNAME = args[0];
            this.XMPP_PORT = args[1];
            this.SNIPER_ID = args[2];
            this.SNIPER_PASSWORD = args[3];

            connection = new XmppClientConnection(XMPP_HOSTNAME, Convert.ToInt32(XMPP_PORT));

            Jid jid = new Jid(SNIPER_ID, XMPP_HOSTNAME, AUCTION_RESOURCE);

            connection.Password = SNIPER_PASSWORD;
            connection.Username = jid.User;

            Console.WriteLine("Client Connection User: " + jid.User + " Password: " + SNIPER_PASSWORD);
            connection.AutoAgents = false;
            connection.AutoPresence = true;
            connection.AutoRoster = true;
            connection.AutoResolveConnectServer = true;
            connection.Open();

            connection.OnLogin += (object sender) =>
            {
                Console.WriteLine("Connected ");
            };
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            userRequests.ForEach(r => r.JoinAuction(connection, SNIPER_ID, XMPP_HOSTNAME, snipers, tbItemId.Text));
        }
    }
}
