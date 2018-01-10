using System;
using System.Collections.Generic;

namespace Monster.Net
{
    public delegate void MessageHandler(object data);

    internal class NetDispatcher
    {
        private Dictionary<int, Action<Protocol>> mHandlerMap = new Dictionary<int, Action<Protocol>>();
        public void RegisterMessageHandler(int msgNo, Action<Protocol> handler)
        {
            if(!mHandlerMap.ContainsKey(msgNo))
            {
                mHandlerMap.Add(msgNo, handler);
            }
        }

        public void UnRegisterMessageHandler(int msgNo)
        {
            if (mHandlerMap.ContainsKey(msgNo))
            {
                mHandlerMap.Remove(msgNo);
            }
        }

        public void Dispatch(Protocol proto)
        {
            if (mHandlerMap.ContainsKey(proto.msgNo))
            {
                mHandlerMap[proto.msgNo](proto);
            }
        }
    }
}
