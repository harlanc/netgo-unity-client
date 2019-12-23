using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using Netgo.Library;
using UnityEngine;
namespace Netgo.Network
{
    //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-client-socket-example?view=netframework-4.8
    public class NGUDPSocket : NGISocket
    {
        UdpClient mUdpClient;

        private Queue<NGBuffer> mBufferQueue = new Queue<NGBuffer>();

        public Queue<NGBuffer> MBufferQueue { get => mBufferQueue; set => mBufferQueue = value; }

        public void Connect(IPEndPoint endPoint)
        {
            mUdpClient.Connect(endPoint);
        }

        public void Receive()
        {
            mUdpClient.BeginReceive(new AsyncCallback(OnReceiveResult),mUdpClient);
        }

        public void OnReceiveResult(IAsyncResult result)
        {
            try
            {
                UdpClient client = (UdpClient)result.AsyncState;
                IPEndPoint point = new IPEndPoint(IPAddress.Any, 8000);

                byte [] bytesRead = client.EndReceive(result,ref point);
                if (bytesRead.Length > 0)
                {
                    NGBuffer buffer = new NGBuffer(bytesRead, bytesRead.Length);
                    lock (mBufferQueue)
                    {
                        mBufferQueue.Enqueue(buffer);
                    }

                    client.BeginReceive(new AsyncCallback(OnReceiveResult), mUdpClient);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    //if (state.sb.Length > 1)
                    //{
                    //    receivestr = state.sb.ToString();
                    //}
                    // Signal that all bytes have been received.  
                    //receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());

            }
        }


        public void Send(byte[] data)
        {
            mUdpClient.BeginSend(data, data.Length, new AsyncCallback(OnSendResult), mUdpClient);

        }

        public void OnSendResult(IAsyncResult result)
        {
            try
            {
                // Retrieve the socket from the state object.  
                UdpClient client = (UdpClient)result.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(result);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
              //  sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }





    }
}
