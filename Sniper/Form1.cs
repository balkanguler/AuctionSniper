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
    public partial class Form1 : Form
    {
        private string XMPP_HOSTNAME;
        private string XMPP_PORT;
        private string SNIPER_ID;
        private string SNIPER_PASSWORD;
        private string itemId;

        IAuction auction;
        ChatManager chatManager;
        Chat chat;

        SniperTableModel snipers = new SniperTableModel();

        

        public static readonly string ITEM_ID_AS_LOGIN = "auction-{0}";
        public static readonly string AUCTION_RESOURCE = "Auction";
        public static readonly string AUCTION_ID_FORMAT = ITEM_ID_AS_LOGIN + "{0}/" + AUCTION_RESOURCE;

        private XmppClientConnection connection;

        public Form1()
        {
            InitializeComponent();
            gvSniper.DataSource = snipers;
        }

        public void Start(string XMPP_HOSTNAME, string XMPP_PORT, string SNIPER_ID, string SNIPER_PASSWORD, string itemId)
        {
            // TODO: Complete member initialization
            this.XMPP_HOSTNAME = XMPP_HOSTNAME;
            this.XMPP_PORT = XMPP_PORT;
            this.SNIPER_ID = SNIPER_ID;
            this.SNIPER_PASSWORD = SNIPER_PASSWORD;
            this.itemId = itemId;

            connection = new XmppClientConnection(XMPP_HOSTNAME, Convert.ToInt32(XMPP_PORT));

            Jid jid = new Jid(SNIPER_ID, XMPP_HOSTNAME, AUCTION_RESOURCE);

            connection.Password = SNIPER_PASSWORD;
            connection.Username = jid.User;
            connection.AutoAgents = false;
            connection.AutoPresence = true;
            connection.AutoRoster = true;
            connection.AutoResolveConnectServer = true;

            auction = new XMPPAuction();
            chatManager = new ChatManager(connection);
            chat = chatManager.CreateChat(string.Format(ITEM_ID_AS_LOGIN, itemId), XMPP_HOSTNAME, AUCTION_RESOURCE,
                new AuctionMessageTranslator(SNIPER_ID, new AuctionSniper(auction, new SniperStateDisplayer(this), itemId)));
            auction.Chat = chat;
            connection.Open();

            connection.OnLogin += (object sender) =>
            {
                auction.Join();
            };
        }

        public void ShowStatus(string status)
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() =>
                {
                    ShowStatus(status);
                }));

            else
                snipers.SetStatusText(status);
        }

        public void SniperStatusChanged(SniperState newState, string status)
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() =>
                {
                    SniperStatusChanged(newState, status);
                }));

            else
            {
                snipers.SniperStatusChanged(newState, status);
                gvSniper.Refresh();
            }
        }
    }
}
