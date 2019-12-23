
namespace Netgo.Client
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Net.Sockets;
    using Netgo.Library;
    using Google.Protobuf;
    using System.Net;
    using Google.Protobuf.WellKnownTypes;

    using Netgo.Network;


    public class NGInterface
    {
        public NGNetwork mNetwork;
        private NGMessageReceiver mReceiver;
        private NGRPC mRPC;
        private static NGRoom mRoom = new NGRoom();

        public enum State
        {
            NetworkConnected,
            NetworkDisconnected,
        }

        public static NGRoom CurrentRoom()
        {
            return mRoom;
        }



        // public NGGameClient(IPEndPoint serverAddress, ProtocolType type)
        // {
        //     mSocket = new NGSocket(mNetwork.Type);
        //     mSocket.Connect(serverAddress);
        //     mRPC = new NGRPC(mSocket);
        // }
        //https://github.com/protocolbuffers/protobuf/issues/993

        public static void JoinOrCreateRoom(string roomid,uint maxnumber)
        {
            mRoom.JoinOrCreateRoom(roomid,maxnumber);
        }

        public static void CreateRoom(string roomid, uint maxnumber)
        {
            mRoom.CreateRoom(roomid, maxnumber);
        }

        public static void JoinRoom(string roomid)
        {
            mRoom.JoinRoom(roomid);
        }

        public static void LeaveRoom()
        {
            mRoom.LeaveRoom();
        }

        public static void SubscribeMsgChannels(uint[] channelids)
        {
            mRoom.SubscribeMsgChannels(channelids);
        }

        public static void UnsubscribeMsgChannels(uint[] channelids)
        {
            mRoom.UnsubscribeMsgChannels(channelids);
        }

        public static void Instantiate(string prefabname, Vector3 position, Quaternion rotation, uint[] viewids)
        {
            mRoom.Instantiate(prefabname, position, rotation, viewids);
        }
    }
}

