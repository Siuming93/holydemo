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
    }

    public class NetManager
    {
        private static NetManager _instance = new NetManager();
        public static NetManager Instance { get { return _instance; } }

        public ConnectState connectState;

        private TcpClient mClient;
        private NetworkStream mStream;

        private bool mRunningShread;
        private Thread mReciveThread;
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

        public void TryConnect(string server, int port)
        {
            try
            {
                mClient = new TcpClient();
                mClient.BeginConnect(server, port, ConnectCallback, null);
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
                mClient.Close();
            if (mStream != null)
                mStream.Close();
        }

        public void StartRun()
        {
            mRunningShread = true;
            mReciveThread = new Thread(ReceiveFunc);
            mReciveThread.Start();

            mSendThread = new Thread(SendFunc);
            mSendThread.Start();
        }

        #region self action
        private void ReceiveFunc()
        {
            byte[] bytes = new byte[BUFFER_SIZE];

            while (mRunningShread)
            {
                int len = mStream.Read(bytes, 0, bytes.Length);
                if (len > 6)
                {

                    int protoLen = bytes[0] << 8 + bytes[1];
                    int msgNo = bytes[2] << 24 + bytes[3] << 16 + bytes[4] << 8 + bytes[5];
                    MemoryStream stream = new MemoryStream();
                    stream.Write(bytes, 6, len - 6);
                    Protocol proto = new Protocol() { msgNo = msgNo, stream = stream };
                    mReceiveBuffer.Enqueue(proto);
                }
            }
        }

        private void SendFunc()
        {
            byte[] bytes = new byte[BUFFER_SIZE];

            while (mRunningShread)
            {
                if (mSendBuffer.Count > 0)
                {
                    Protocol proto = mSendBuffer.Dequeue();
                    int len = (int)proto.stream.Length;
                    if (len > 0)
                    {
                        bytes[0] = (byte)(len >> 8);
                        bytes[1] = (byte)(len);
                        proto.stream.Read(bytes, 6, len);
                        bytes[2] = (byte)(proto.msgNo >> 24);
                        bytes[3] = (byte)(proto.msgNo >> 16);
                        bytes[4] = (byte)(proto.msgNo >> 8);
                        bytes[5] = (byte)(proto.msgNo);
                        mStream.Write(bytes, 0, len + 4);
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
            }
        }
        #endregion
        public void SendMessage(global::ProtoBuf.IExtensible msg)
        {
            var no = MsgIDDefine.GetMsgIDByName(msg.GetType().Name);
            var stream = new MemoryStream();
            Serializer.NonGeneric.Serialize(stream, msg);
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
    }
}