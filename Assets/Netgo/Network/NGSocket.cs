namespace Netgo.Network
{

    using System.Net.Sockets;
    using System.Net;
    using System;
    using System.Threading;
    using UnityEngine;
    using System.Text;
    using System.Collections.Generic;


    using Netgo.Library;


    //// State object for receiving data from remote device.  
    //public class StateObject
    //{
    //    // Client socket.  
    //    public Socket workSocket = null;
    //    // Size of receive buffer.  
    //    public const int BufferSize = 256;
    //    // Receive buffer.  
    //    public byte[] buffer = new byte[BufferSize];
    //    // Received data string.  
    //    public StringBuilder sb = new StringBuilder();
    //}

    //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-client-socket-example?view=netframework-4.8

    //https://blog.csdn.net/treepulse/article/details/53465137
    public class NGSocket : NGISocket
    {

        public enum OpCode
        {
            JoinLobby = 0,
            LeaveLobby = 1,
            JoinRoom = 2,
            LeaveRoom = 3,
            CreateRoom = 4,
            LaunchEvent = 5,
            SubscribeMsgChannels = 6,
            UnSubscribeMsgChannels = 7
        }


        private Socket mClientSocket;

        private static int mBufferSize = 1024;
        private byte[] mBuffer = new byte[mBufferSize];

        private Queue<NGBuffer> mBufferQueue = new Queue<NGBuffer>();

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private string receivestr;

        public Queue<NGBuffer> MBufferQueue { get => mBufferQueue; set => mBufferQueue = value; }

        public NGSocket(ProtocolType type)
        {
            mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, type);

        }

        public void Connect(IPEndPoint serverIP)
        {
            mClientSocket.BeginConnect(serverIP, OnConnectResult, mClientSocket);
        }

        public void OnConnectResult(IAsyncResult result)
        {
            try
            {
                Socket client = (Socket)result.AsyncState;
                client.EndConnect(result);
                connectDone.Set();

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public void Receive()
        {
            //StateObject state = new StateObject();
            //state.workSocket = socket;
            mClientSocket.BeginReceive(mBuffer, 0, mBufferSize, 0, OnReceiveResult, mClientSocket);
        }
        public void OnReceiveResult(IAsyncResult result)
        {
            try
            {
                Socket socket = (Socket)result.AsyncState;
                int bytesRead = socket.EndReceive(result);

                //NGLogger.LogDebug("OnReceiveResult" + bytesRead);

                if (bytesRead > 0)
                {

                    NGBuffer buffer = new NGBuffer(mBuffer, bytesRead);
                    lock (mBufferQueue)
                    {
                        mBufferQueue.Enqueue(buffer);
                    }

                    socket.BeginReceive(mBuffer, 0, mBufferSize, 0, new AsyncCallback(OnReceiveResult), socket);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    //if (state.sb.Length > 1)
                    //{
                    //    receivestr = state.sb.ToString();
                    //}
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());

            }
        }

        public void Send(byte[] data)
        {
            // Begin sending the data to the remote device.  
            mClientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(OnSendResult), mClientSocket);
        }

        public void OnSendResult(IAsyncResult result)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket socket = (Socket)result.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = socket.EndSend(result);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Close()
        {
            mClientSocket.Close();
        }

    }
}
