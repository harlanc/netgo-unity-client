using System;
using System.Net;
namespace Netgo.Network
{
    public interface NGISocket
    {
        void Connect(IPEndPoint endPoint);

        void Receive();
        void OnReceiveResult(IAsyncResult result);
         
        void Send(byte[] data);
        void OnSendResult(IAsyncResult result);
    }
}
