using agsXMPPChat;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper;
using Rhino.Mocks;
using NSubstitute;

namespace SniperTest
{
    [TestFixture]
    class AuctionMessageTranslatorTest
    {
        public static readonly Chat UNUSED_CHAT;
        IAuctionEventListener listenerMock;
        AuctionMessageTranslator translator;
        private static string SNIPER_ID = "sniper";

        [SetUp]
        public void Setup()
        {
            listenerMock = Substitute.For<IAuctionEventListener>();
            translator = new AuctionMessageTranslator(SNIPER_ID, listenerMock);
       }

        [Test]
        public void NotifiesAuctionClosedWhenCloseMessageReceived()
        {
            listenerMock.Expect(l => l.AuctionClosed());

            string message = "SOLVersion: 1.1; Event: CLOSE;";

            translator.ProcessMessage(UNUSED_CHAT, message);

            listenerMock.VerifyAllExpectations();
        }

        [Test]
        public void NotifiesBidDetailsWhenCurrentPriceMessageReceivedFromOtherBidder()
        {
            listenerMock.Expect(l => l.CurrentPrice(192, 7, PriceSource.FromOtherBidder)).Repeat.Once();

            string message = "SOLVersion: 1.1; Event: PRICE; CurrentPrice: 192; Increment: 7; Bidder: Someone else;";

            translator.ProcessMessage(UNUSED_CHAT, message);

            listenerMock.VerifyAllExpectations();
        }

        [Test]
        public void NotifiesBidDetailsWhenCurrentPriceMessageReceivedFromSniper()
        {
            listenerMock.Expect(l => l.CurrentPrice(234, 5, PriceSource.FromSniper)).Repeat.Once();

            string message = string.Format("SOLVersion: 1.1; Event: PRICE; CurrentPrice: 234; Increment: 5; Bidder: {0};", SNIPER_ID);

            translator.ProcessMessage(UNUSED_CHAT, message);

            listenerMock.VerifyAllExpectations();
        }
    }
}
