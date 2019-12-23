using System.Collections.Generic;
using System.Collections;
using System.IO;
//https://stackoverflow.com/questions/2522376/how-to-choose-between-protobuf-csharp-port-and-protobuf-net
using Google.Protobuf;
using Netgo.Library;

using Google.Protobuf.WellKnownTypes;
using UnityEngine;
using Netgo.Network;


namespace Netgo.Client
{
    public class NGMessageProcessor : MonoBehaviour
    {


        private void Start()
        {
            //mSocket = NGGameClient.mSocket;
            this.gameObject.AddComponent<NGMessageReceiver>();
            NGMessageReceiver.Instance.MessageProcessor = ProcessRecieveData;

        }
        public static T ReadMessage<T>(Stream stream) where T : IMessage, new()
        {
            T message = new T();
            message.MergeFrom(stream);
            return message;
        }

        private void ProcessRecieveData(byte[] data)
        {
            //NGLogger.LogInfo("Receive a message");
            Stream stream = new MemoryStream(data);

            ReceiveMessage message = ReadMessage<ReceiveMessage>(stream);
            switch (message.ReceiveMsgType)
            {
                case ReceiveMessageType.ResponseSocketStatus:
                    switch (message.RssMsg.SStatus)
                    {
                        case SocketStatus.Connected:
                            NGNetwork.Status = SocketStatus.Connected;
                            NGNetworkEvent.OnConnected();
                            break;
                    }
                    break;
                case ReceiveMessageType.ResponseOperation:

                    ResponseOperationMessage rmsg = message.RoMsg;
                    NGInterface.CurrentRoom().ResponseProcessor(rmsg);
                    break;

                case ReceiveMessageType.Forward:

                    ForwardMessage fmsg = message.FMsg;
                    uint peerId = fmsg.PeerId;
                    switch (fmsg.MsgType)
                    {
                        case MessageType.JoinRoom:
                            var jrParams = fmsg.JrfParams;
                            NGPlayer newplayer = new NGPlayer(false, peerId);
                            NGInterface.CurrentRoom().AddPlayer(newplayer);
                            NGEvent.OnOtherPlayerEnteredRoom(newplayer);
                            break;

                        case MessageType.JoinOrCreateRoom:
                            var jocParams = fmsg.JocrfParams;
                            NGPlayer player = new NGPlayer(false, peerId);
                            NGInterface.CurrentRoom().AddPlayer(player);
                            NGEvent.OnOtherPlayerEnteredRoom(player);
                            break;

                        case MessageType.LeaveRoom:
                            var lParams = fmsg.LrfParams;
                            NGLogger.LogDebug("LeaveRoom" + peerId);

                            var leaveplayer = NGInterface.CurrentRoom().GetNGPlayer(peerId);
                            NGEvent.OnOtherPlayerLeftRoom(leaveplayer);
                            Destroy(leaveplayer.GO);
                            NGInterface.CurrentRoom().RemovePlayer(peerId);

                            break;

                        case MessageType.Rpc:
                            var rpcparams = fmsg.RfParams;
                            NGAny[] objs = NGProtobufConverter<NGAny>.RepeatedField2Array(rpcparams.Parameters);
                            NGRPC.ExcuteRPC(rpcparams.ViewID, rpcparams.MethodName, objs);
                            break;

                        case MessageType.Instantiation:
                            var i = fmsg.IfParams;
                            var ids = NGProtobufConverter<uint>.RepeatedField2Array(i.ViewIDs);
                            NGInterface.CurrentRoom().Instantiate(peerId, false, ids, i.PrefabName, i.Position, i.Rotation);
                            break;

                        case MessageType.ViewSync:
                            var viewsyncparams = fmsg.VsfParams;
                            var viewParams = viewsyncparams.VsdParams;
                            NGViewContainer container = new NGViewContainer();
                            container.DeserializeAll(viewParams);
                            break;

                        case MessageType.CustomEvent:
                            var ceParams = fmsg.CeParams;
                            NGEvent.OnCustomEvent(ceParams.EventID, NGProtobufConverter<NGAny>.RepeatedField2Array(ceParams.CustomData));
                            break;

                    }
                    break;
            }
        }


    }
}
