
namespace Netgo.Network
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Net.Sockets;
    using System.Net;
    using Netgo.Library;


    [RequireComponent(typeof(NGNetworkIncomingEvent))]
    public class NGNetwork : NGNetworkIncomingEvent
    {
        public string Address;
        public int Port;
        public ProtocolType Type;
        public static SocketStatus Status = SocketStatus.Disconnected;

        public static bool IsConnected { get { return Status == SocketStatus.Connected; } }

        public static NGNetwork Instance = null;

        public static NGSocket Socket;

        private void Awake()
        {
  
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            
            
            if (Status != SocketStatus.Connected)
            {
                IPAddress address = IPAddress.Parse(Address);
                IPEndPoint serverAddress = new IPEndPoint(address, Port);
                Socket = new NGSocket(Type);
                Socket.Connect(serverAddress);

            }
        }
        public override void onConnected()
        {
            Status = SocketStatus.Connected;
            NGLogger.LogDebug("On connected");
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                NGLogger.LogDebug("Socket is Closed.");
                Socket.Close();
            }

        }
    }
}
