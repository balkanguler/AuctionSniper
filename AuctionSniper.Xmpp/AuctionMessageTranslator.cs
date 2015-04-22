using agsXMPPChat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AuctionSniper.Xmpp
{
    public class AuctionMessageTranslator : MessageListener
    {
        private readonly List<IAuctionEventListener> auctionEventListeners;
        private string sniperId;

        public AuctionMessageTranslator(string sniperId, List<IAuctionEventListener> auctionEventListeners)
        {
            // TODO: Complete member initialization
            this.sniperId = sniperId;
            this.auctionEventListeners = auctionEventListeners;
        }
        public override void ProcessMessage(agsXMPPChat.Chat UNUSED_CHAT, string message)
        {
            var eventList = unpackEventFrom(message);

            AuctionEvent aEvent = AuctionEvent.From(message);

            string type = aEvent.Type;
            if ("CLOSE".Equals(type))           
                auctionEventListeners.ForEach(l => l.AuctionClosed());             
            else if ("PRICE".Equals(type))
                auctionEventListeners.ForEach(l => l.CurrentPrice(aEvent.CurrentPrice, aEvent.Increment, aEvent.IsFrom(sniperId)));

        }


        private Dictionary<string, string> unpackEventFrom(string message)
        {
            var eventList = new Dictionary<string, string>();

            try
            {
                foreach (string element in message.Split(new char[] { ';' }))
                {

                    if (!string.IsNullOrWhiteSpace(element))
                    {
                        string[] pair = element.Split(new char[] { ':' });
                        eventList.Add(pair[0].Trim(), pair[1].Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in unpackEventFrom.");
                Debug.WriteLine(ex.ToString());
                throw;
            }
            return eventList;
        }
    }

    class AuctionEvent
    {
        private readonly Dictionary<string, string> fields = new Dictionary<string, string>();
        public string Type { get { return get("Event"); } }
        public int CurrentPrice { get { return getInt("CurrentPrice"); } }
        public int Increment { get { return getInt("Increment"); } }


        private int getInt(string fieldName)
        {
            return Convert.ToInt32(get(fieldName));
        }
        private string get(string fieldName)
        {
            return fields[fieldName];
        }

        private void addField(string field)
        {
            if (!string.IsNullOrWhiteSpace(field))
            {
                string[] pair = field.Split(':');
                fields.Add(pair[0].Trim(), pair[1].Trim());
            }
        }

        public static AuctionEvent From(string message)
        {
            AuctionEvent aEvent = new AuctionEvent();

            foreach (string field in fieldsIn(message)){
                aEvent.addField(field);
            }

            return aEvent;
        }

        private static string[] fieldsIn(string message)
        {
            return message.Split(';');
        }

        public PriceSource IsFrom(string sniperId)
        {
            return sniperId.Equals(bidder()) ? PriceSource.FromSniper : PriceSource.FromOtherBidder;
        }

        private string bidder()
        {
            return get("Bidder");
        }
    }
}
