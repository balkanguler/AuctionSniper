﻿using AuctionSniper.Xmpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSniper.Xmpp
{
    public interface IAuctionHouse
    {
        IAuction AuctionFor(Item item);
    }
}
