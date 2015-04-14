using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace agsXMPPChat
{
    public interface IMessageListener
    {
        void ProcessMessage(Chat aChat, string message);
    }
}
