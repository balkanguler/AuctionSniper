using agsXMPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public class XMPPAuctionHouse: IAuctionHouse
    {
        public static readonly string AUCTION_RESOURCE = "Auction";        
        private XmppClientConnection connection;

        public XMPPAuctionHouse(XmppClientConnection connection)
        {
            this.connection = connection;
        }
        public IAuction AuctionFor(string itemId)
        {
            return new XMPPAuction(connection, itemId);
        }

        public static XMPPAuctionHouse Connect(string hostname, string port, string username, string password)
        {
            XmppClientConnection connection = new XmppClientConnection(hostname, Convert.ToInt32(port));

            Jid jid = new Jid(username, hostname, AUCTION_RESOURCE);

            connection.Password = password;
            connection.Username = jid.User;

            Console.WriteLine("Client Connection User: " + jid.User + " Password: " + password);
            connection.AutoAgents = false;
            connection.AutoPresence = true;
            connection.AutoRoster = true;
            connection.AutoResolveConnectServer = true;
            connection.Open();

            connection.OnLogin += (object sender) =>
            {
                Console.WriteLine("Connected ");
            };

            return new XMPPAuctionHouse(connection);
        }
    }
}
