using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using ProtoBuf;

namespace Monster.Net
{

    internal class Protocol
    {
        public int msgNo;
        public MemoryStream stream;
    }

    public enum ConnectState
    {
        NonConnect = 0,
        TryConnecting,
        Connected,
        ConnectingOutTime,
        ConnectedFailed,
        ConnectedBreak,
    }

    public class NetManager
    {
        private static NetManager _instance = new NetManager();
        public static NetManager Instance { get { return _instance; } }
        public ConnectState connectState;

        private TcpClient mClient;
        private NetworkStream mStream;
        private bool mRunningShread;
        private Thread mReceiveThread;
        private Thread mSendThread;
        private const int BUFFER_SIZE = 1024;
        private Queue<Protocol> mReceiveBuffer;
        private Queue<Protocol> mSendBuffer;

        private NetDispatcher mDispatcher;

        private NetManager()
        {
            mReceiveBuffer = new Queue<Protocol>(BUFFER_SIZE);
            mSendBuffer = new Queue<Protocol>(BUFFER_SIZE);
            mDispatcher = new NetDispatcher();
        }
        public void Init()
        {
        }
        public void TryConnect(string server)
        {
            try
            {
                int port = 8888;
                mClient = new TcpClient();
                mClient.BeginConnect(server, port, ConnectCallback, null);
                mClient.ReceiveTimeout = 30000;
                mClient.SendTimeout = 30000;
                mClient.NoDelay = true;
                connectState = ConnectState.TryConnecting;
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        public void Close()
        {
            connectState = ConnectState.NonConnect;
            mRunningShread = false;
            if (mClient != null)
            {
                mClient.Close();
            }
            if (mStream != null)
                mStream.Close();
        }
        public void StartRun()
        {
            mRunningShread = true;
            mReceiveThread = new Thread(ReceiveFunc);
            mReceiveThread.Start();

            mSendThread = new Thread(SendFunc);
            mSendThread.Start();
        }

        #region self action
        private void ReceiveFunc()
        {
            byte[] bytes = new byte[BUFFER_SIZE];
            byte[] cacheBytes = new byte[BUFFER_SIZE];
            int len = 0;


            while (IsSocketConnected(mClient.Client))
            {
                int lenCached = mStream.Read(cacheBytes, 0, cacheBytes.Length);

                if(lenCached == 0)
                    continue;

                //可以解决两包相连的问题
                //int protoStartLen = 0;
                //if (len < 6)
                //{
                //    int headLen = 6-len;
                //    Array.Copy(cacheBytes, 0, bytes, len, headLen);
                //    protoStartLen = 6 - len;
                //    len += headLen;
                //    if (len < 6)
                //        continue;
                //}

                //int protoLen = (bytes[0] << 8) + (bytes[1]) + 2;
                //int msgNo = (bytes[2] << 24) + (bytes[3] << 16) + (bytes[4] << 8) + (bytes[5]);

                //int extraLen = 0;
                //if (len < protoLen)
                //{
                //    int copyLen = protoLen - len;
                //    Array.Copy(cacheBytes, protoStartLen, bytes, len, copyLen);
                //    len += copyLen;
                //    protoStartLen = copyLen;
                //    extraLen = lenCached - copyLen;
                //    if (len < protoLen)
                //        continue;
                //}
                //len = 0;

                int protoLen = (cacheBytes[0] << 8) + (cacheBytes[1]) + 2;
                int msgNo = (cacheBytes[2] << 24) + (cacheBytes[3] << 16) + (cacheBytes[4] << 8) + (cacheBytes[5]);

                Debug.Log(protoLen + "msgNo" + msgNo);
                MemoryStream stream = new MemoryStream();
                //stream.Write(bytes, 6, lenCached - 6);
                stream.Write(cacheBytes, 6, lenCached - 6);
                Protocol proto = new Protocol() { msgNo = msgNo, stream = stream };
                mReceiveBuffer.Enqueue(proto);

                //if (extraLen > 0)
                //{
                //    len = extraLen;
                //    Array.Copy(cacheBytes, protoStartLen, bytes, len, extraLen);
                //}
            }

            Debug.LogError("接收  结  结束了~~");
        }
        private void SendFunc()
        {
            byte[] bytes = new byte[BUFFER_SIZE];

            while (mRunningShread)
            {
                if (mSendBuffer.Count > 0)
                {
                    Protocol proto = mSendBuffer.Dequeue();
                    int len = (int)proto.stream.Length + 4;
                    if (len > 4)
                    {
                        bytes[0] = (byte)(len >> 8);
                        bytes[1] = (byte)(len);
                        bytes[2] = (byte)(proto.msgNo >> 24);
                        bytes[3] = (byte)(proto.msgNo >> 16);
                        bytes[4] = (byte)(proto.msgNo >> 8);
                        bytes[5] = (byte)(proto.msgNo);
                        proto.stream.Read(bytes, 6, len);

                        mStream.Write(bytes, 0, len + 2);
                    }
                }
            }
        }
        private void ConnectCallback(object obj)
        {
            connectState = mClient.Connected ? ConnectState.Connected : ConnectState.ConnectedFailed;
            if (mClient.Connected)
            {
                mStream = mClient.GetStream();
                StartRun();
            }
        }

        private bool IsSocketConnected(Socket socket)
        {
            return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
        }
        #endregion
        public void SendMessage(global::ProtoBuf.IExtensible msg)
        {
            var no = MsgIDDefineDic.Instance().GetMsgID(msg.GetType());
            var stream = new MemoryStream();
            Serializer.NonGeneric.Serialize(stream, msg);
            stream.Position = 0;

            var s = Serializer.NonGeneric.Deserialize(msg.GetType(),stream);
            Debug.Log(s);
            stream.Position = 0;

            mSendBuffer.Enqueue(new Protocol { msgNo = no, stream = stream });
        }
        public void RegisterMessageHandler(int msgNo, MessageHandler handler)
        {
            mDispatcher.RegisterMessageHandler(msgNo, handler);
        }
        public void UnRegistreMessageHandler(int msgNo)
        {
            mDispatcher.UnRegisterMessageHandler(msgNo);
        }
        public void Dispatch()
        {
            while (mReceiveBuffer.Count > 0)
            {
                mDispatcher.Dispatch(mReceiveBuffer.Dequeue());
            }
        }

        private bool mLastConneted;
        public void Update()
        {
            //链接未成功之前不需要监测
            if (mClient == null || mStream == null)
                return;
            //监测链接状态变化
            bool isConnected = IsSocketConnected(mClient.Client);
            if (mLastConneted != isConnected)
            {
                switch (connectState)
                {
                    case ConnectState.NonConnect:
                        break;
                    case ConnectState.TryConnecting:
                        break;
                    case ConnectState.Connected:
                        if(!isConnected)
                            connectState = ConnectState.ConnectedBreak;
                        break;
                    case ConnectState.ConnectingOutTime:
                        break;
                    case ConnectState.ConnectedFailed:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                mLastConneted = isConnected;
            }
            Dispatch();
        }
    }
}