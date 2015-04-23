﻿using agsXMPP;
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

namespace AuctionSniper
{
    public partial class MainWindow : Form
    {
        private readonly string sniperId;
     
        public static readonly string AUCTION_RESOURCE = "Auction";
        SniperTableModel snipers;
        readonly List<IUserRequestListener> sniperLaunchers = new List<IUserRequestListener>();

        public MainWindow(SniperTableModel tableModel, string sniperId)
        {
            InitializeComponent();
            this.Text = Program.APPLICATION_TITLE;
            tbItemId.Name = Program.NEW_ITEM_ID_NAME;
            btnJoin.Name = Program.JOIN_BUTTON_NAME;
            snipers = tableModel;
            gvSniper.DataSource = snipers;
            this.sniperId = sniperId;
        }

        public void AddUserRequestListener(IUserRequestListener userRequestListener)
        {
            sniperLaunchers.Add(userRequestListener);
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            sniperLaunchers.ForEach(r => r.JoinAuction(sniperId, tbItemId.Text));
        }
    }
}
