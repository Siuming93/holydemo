using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace Monster.Net
{
    public delegate void MessageHandler(object msg);

    internal class NetDispatcher
    {
        private Dictionary<int, MessageHandler> mHandlerMap;
        public void RegisterMessageHandler(int msgNo, MessageHandler handler)
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
            if(mHandlerMap.ContainsKey(proto.msgNo))
            {
                object msg = Serializer.NonGeneric.Deserialize(MsgIDDef.Instance().GetMsgType(proto.msgNo), proto.stream);
                mHandlerMap[proto.msgNo](msg);
            }
        }
    }
}
