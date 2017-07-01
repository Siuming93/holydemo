using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

public delegate void MessageHandler(object msg);

public class NetManager : Singleton<NetManager>
{
    public enum ConnectState
    {
        TryConnecting,
        NonConnect,
        Connected,
        ConnectingOutTime,
    }

    private TcpClient mClient;
    private NetworkStream mStream;

    private bool mRunningShread;
    private Thread mReciveThread;
    private Thread mSendThread;

    private const int BUFFER_SIZE = 1024;
    private List<string> mReceiveBuffer = new List<string>(BUFFER_SIZE);
    private Queue<string> mSendBuffer = new Queue<string>(BUFFER_SIZE); 

    public ConnectState connectState;

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
        mClient.Close();
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
            if (len > 4)
            {
                mReceiveBuffer.Add(bytes.ToString());
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
                string msg = mSendBuffer.Dequeue();
                int length = msg.Length + 4;
                for (int i = 0; i < 4; i++)
                {
                    bytes[i] = (byte) (length >> (24 - i*8));
                }
                for (int i = 0; i < msg.Length; i++)
                {
                    bytes[i + 4] = (byte)msg[i];
                }
                mStream.Write(bytes, 0, length);
            }
        }
    }

    private void ConnectCallback(object obj)
    {
        connectState = mClient.Connected ? ConnectState.Connected : ConnectState.ConnectingOutTime;
        if (mClient.Connected)
        {
            mStream = mClient.GetStream();
        }
    }
    #endregion
    public void SendMessage(string msg)
    {
        mSendBuffer.Enqueue(msg);
    }

    public void RegisterMessageHandler(string singture, MessageHandler handler)
    {
                
    }
}