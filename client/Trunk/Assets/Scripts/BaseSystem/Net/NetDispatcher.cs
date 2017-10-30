using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace Monster.Net
{
    public delegate void MessageHandler(object data);

    internal class NetDispatcher
    {
        private Dictionary<int, MessageHandler> mHandlerMap = new Dictionary<int, MessageHandler>();
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
            proto.stream.Position = 0;
            object msg = Serializer.NonGeneric.Deserialize(MsgIDDefineDic.Instance().GetMsgType(proto.msgNo), proto.stream);
            Debug.LogWarning(string.Format("Reci msg:{0}",msg.GetType().Name));

            if (mHandlerMap.ContainsKey(proto.msgNo))
            {
                mHandlerMap[proto.msgNo](msg);
            }
        }
    }
}
