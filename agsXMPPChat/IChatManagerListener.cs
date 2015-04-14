using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace agsXMPPChat
{
    public interface IChatManagerListener
    {
        void ChatCreated(Chat chat, bool createdlocally);
    }
}
