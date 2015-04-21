using agsXMPPChat;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SniperTest
{
    class MessageListener : IMessageListener
    {
        private static readonly BlockingCollection<string> messages = new BlockingCollection<string>();

        public void ProcessMessage(Chat aChat, string message)
        {
            Console.WriteLine("Gelen Mesaj: " + message);
            messages.Add(message);
        }

        internal void receivesAMessages()
        {
            string message;
            Assert.That(messages.TryTake(out message, 5000), "Message is null ");
        }

        internal void receivesAMessagesWithText(string expectedMessage)
        {
            string message;
            Assert.That(messages.TryTake(out message, 5000), "Message is null ");
            StringAssert.AreEqualIgnoringCase(expectedMessage, message);
        }
    }
}
