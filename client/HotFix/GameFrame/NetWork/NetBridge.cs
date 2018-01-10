using System;
using System.Collections.Generic;
using Monster.Net;
using Thrift.Protocol;
using Thrift.Transport;
using UnityEngine;

namespace HotFix.GameFrame.NetWork
{
    public class NetBridge
    {
        private static NetBridge m_Instance = new NetBridge();

        public static NetBridge Instance
        {
            get { return m_Instance; }
        }

        public void Init()
        {
            
        }

        public void Dispose()
        {
            
        }

        private Dictionary<int, Action<TBase>> m_HandlerMap = new Dictionary<int, Action<TBase>>();
        public void RegisterMessageHandler(int msgNo, Action<TBase> handler)
        {
            if (!m_HandlerMap.ContainsKey(msgNo))
            {
                m_HandlerMap.Add(msgNo, handler);
            }
            NetManager.Instance.RegisterMessageHandler(msgNo, Dispatch);
        }

        public void UnRegisterMessageHandler(int msgNo)
        {
            if (m_HandlerMap.ContainsKey(msgNo))
            {
                m_HandlerMap.Remove(msgNo);
            }
            NetManager.Instance.UnRegisterMessageHandler(msgNo);
        }

        public void SendMessage(TBase msg)
        {
            var no = MsgIDDefineDic.Instance.GetMsgID(msg.GetType());
            var protocol = new Protocol()
            {
                msgNo = no,
                buffer = TMemoryBuffer.Serialize(msg),
            };
            NetManager.Instance.SendMessage(protocol);
        }

        public void Dispatch(Protocol protocol)
        {
            TBase msg = MsgIDDefineDic.Instance.CreatMsg(protocol.msgNo);

            //Debug.Log("Receive Msg:" + MsgIDDefineDic.Instance.GetMsgType(protocol.msgNo));
            //var str = "";
            //foreach (var b in protocol.buffer)
            //{
            //    str += " " + b.ToString();
            //}
            //Debug.Log(str);
            msg.Read(new TBinaryProtocol(new TMemoryBuffer(protocol.buffer)));
            Debug.LogWarning(string.Format("Reci msg:{0}", msg.GetType().Name));

            if (m_HandlerMap.ContainsKey(protocol.msgNo))
            {
                m_HandlerMap[protocol.msgNo](msg);
            }
        }
    }
}
