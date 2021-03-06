﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Monster.Net
{
    public class Protocol
    {
        public int msgNo;
        public byte[] buffer;
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

            while (IsSocketConnected(mClient.Client))
            {
                int len = mStream.Read(bytes, 0, bytes.Length);

                if (len == 0)
                    continue;

                WriteBuffer(bytes, len);
            }

            Debug.LogError("接收  结  结束了~~");
        }

        private void WriteBuffer(byte[] newBytes, int len)
        {
            //Debug.Log("Start Receive Msg len "+ len);

             for (int i = 0; i < len; i++)
            {
                curBytes[curLen] = newBytes[i];
                curLen++;
            }
            UnPackage();
        }
        private int curLen = 0;
        private string curtime = "";
        private byte[] curBytes = new byte[BUFFER_SIZE * 2];
        private void UnPackage()
        {
            if (curLen < 6)
                return;

            int needLen = (curBytes[0] << 8) + (curBytes[1]) + 2;

            if (curLen < needLen)
                return;

            int msgNo = (curBytes[2] << 24) + (curBytes[3] << 16) + (curBytes[4] << 8) + (curBytes[5]);

            byte[] bytes = new byte[needLen - 6];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = curBytes[i + 6];
            }

            Protocol proto = new Protocol() { msgNo = msgNo, buffer = bytes };
            mReceiveBuffer.Enqueue(proto);

            curLen = curLen - needLen;
            for (int i = 0; i < curLen; i++)
            {
                curBytes[i] = curBytes[i + needLen];
            }

            //string str = "";
            //foreach (var b in bytes)
            //{
            //    str +=" "+ b;
            //}
            //Debug.LogWarning(str);

            UnPackage();
        }

        private void SendFunc()
        {
            byte[] bytes = new byte[BUFFER_SIZE];

            while (mRunningShread)
            {
                if (mSendBuffer.Count > 0)
                {
                    Protocol proto = mSendBuffer.Dequeue();
                    int len = (int) proto.buffer.Length + 4;

                    if (len > 4)
                    {
                        bytes[0] = (byte)(len >> 8);
                        bytes[1] = (byte)(len);
                        bytes[2] = (byte)(proto.msgNo >> 24);
                        bytes[3] = (byte)(proto.msgNo >> 16);
                        bytes[4] = (byte)(proto.msgNo >> 8);
                        bytes[5] = (byte)(proto.msgNo);

                        for (int i = 0; i < len -4; i++)
                        {
                            bytes[6 + i] = proto.buffer[i];
                        }

                        //Debug.Log(string.Format("msg NO:{0} len{1} buffer:{2}", proto.msgNo, len - 4, str));
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

        private int count = 0;
        public void SendMessage(Protocol protocol)
        {
            mSendBuffer.Enqueue(protocol);
        }
        public void RegisterMessageHandler(int msgNo, Action<Protocol> handler)
        {
            mDispatcher.RegisterMessageHandler(msgNo, handler);
        }
        public void UnRegisterMessageHandler(int msgNo)
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
                        if (!isConnected)
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