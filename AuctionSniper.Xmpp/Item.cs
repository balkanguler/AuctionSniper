using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public class Item
    {
        readonly string identifier;
        readonly int stopPrice;

        public string Identifier
        {
            get { return identifier; }
        }
        public int StopPrice
        {
            get { return stopPrice; }
        } 
        public Item(string identifier, int stopPrice)
        {
            this.identifier = identifier;
            this.stopPrice = stopPrice;
        }

        public override bool Equals(object obj)
        {
            Item item = obj as Item;

            if (item == null)
                return false;

            return identifier.Equals(item.identifier) && stopPrice == item.StopPrice;
        }

        public bool AllowsBid(int bid)
        {
            return bid <= stopPrice;
        }
    }
}
