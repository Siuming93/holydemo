using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Thrift.Protocol;
using Thrift.Transport;

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
            TBase msg = MsgIDDefineDic.Instance.CreatMsg(proto.msgNo);
            msg.Read(new TBinaryProtocol(new TMemoryBuffer(proto.buffer)));
            //Debug.LogWarning(string.Format("Reci msg:{0}",msg.GetType().Name));

            if (mHandlerMap.ContainsKey(proto.msgNo))
            {
                mHandlerMap[proto.msgNo](msg);
            }
        }
    }
}
