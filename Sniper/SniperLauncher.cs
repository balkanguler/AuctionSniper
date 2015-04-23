using agsXMPPChat;
using AuctionSniper.Xmpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper
{
    public class SniperLauncher : IUserRequestListener
    {
        List<Chat> chats = new List<Chat>();
        private ISniperCollector collector;
        IAuctionHouse auctionHouse;
        public SniperLauncher(IAuctionHouse auctionHouse, ISniperCollector collector)
        {
            this.auctionHouse = auctionHouse;
            this.collector = collector;
        }


        public void JoinAuction(string sniperId, string itemId)
        {
            List<IAuctionEventListener> eventListeners = new List<IAuctionEventListener>();
            IAuction auction = auctionHouse.AuctionFor(itemId);

            AuctionSniper sniper = new AuctionSniper(itemId, auction);
            auction.AddAuctionEventListener(sniper);
            eventListeners.Add(sniper);

            //auction.Chat.MessageListener = new AuctionMessageTranslator(sniperId, eventListeners);

            //eventListeners.Add(new AuctionSniper(itemId, auction, new UIThreadSniperListener(snipers)));
            collector.AddSniper(sniper);
            auction.Join();
        }
    }
}
