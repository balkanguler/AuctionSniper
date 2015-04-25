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
        XmppClientConnection connection;
        IXMPPFailureReporter failureReporter;

        public static readonly string AUCTION_RESOURCE = "Auction";        

        public XMPPAuctionHouse(XmppClientConnection connection)
        {
            this.connection = connection;
            this.failureReporter = new LoggingXMPPFailureReporter(new Logger());
        }
        public IAuction AuctionFor(Item item)
        {
            return new XMPPAuction(connection, item, failureReporter);
        }

        public static XMPPAuctionHouse Connect(string hostname, string port, string username, string password)
        {
            XmppClientConnection connection = new XmppClientConnection(hostname, Convert.ToInt32(port));

            Jid jid = new Jid(username, hostname, AUCTION_RESOURCE);

            connection.Password = password;
            connection.Username = jid.User;
            connection.AutoAgents = false;
            connection.AutoPresence = true;
            connection.AutoRoster = true;
            connection.AutoResolveConnectServer = true;
            connection.Open();

            connection.OnLogin += (object sender) =>
            {
            };

            return new XMPPAuctionHouse(connection);
        }
    }
}
