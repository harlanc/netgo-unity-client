using System;
using System.Collections.Generic;

using Netgo.Library;
using UnityEngine;
using Netgo.Network;
using Google.Protobuf;
namespace Netgo.Client
{
    public class NGRoom
    {
        enum ResponseMessageInfo
        {
            Success = 1,
            //CreateRoomSuccess status 
            CreateRoomSuccess = 2,
            //CreateRoomAlreadyExist the room is exist already. 
            CreateRoomAlreadyExist = 3,
            //JoinRoomSuccess status 4
            JoinRoomSuccess = 4,
            //JoinRoomNotExist the room is not exist. 
            JoinRoomNotExist = 5,
            //JoinRoomAlreadyInRoom already in the room. 
            JoinRoomAlreadyInRoom = 6,
            //JoinRoomFull the room is full already. 
            JoinRoomFull = 7,
            //LeaveRoomSuccess leave a room success. 
            LeaveRoomSuccess = 8
        }



        public NGRoom()
        {
        }

        public Dictionary<uint, NGPlayer> ID2OtherPlayers = new Dictionary<uint, NGPlayer>();
        public string RoomName;
        public NGPlayer LocalPlayer = null;

        public NGPlayer GetNGPlayer(uint peerid)
        {
            NGPlayer rv = null;

            if (!ID2OtherPlayers.TryGetValue(peerid, out rv))
            {
                NGLogger.LogWarning("NGPlayer with ID" + peerid.ToString() + " cannot be found.");
            }

            return rv;
        }

        public void AddPlayer(NGPlayer player)
        {
            ID2OtherPlayers.Add(player.mPeerId, player);
        }

        public void RemovePlayer(uint peerid)
        {
            ID2OtherPlayers.Remove(peerid);
        }

        public void ClearPlayers()
        {
            ID2OtherPlayers.Clear();
        }

        public GameObject Instantiate(uint peerid, bool isLocal, UInt32[] viewids, string prefabname, Vector3 position = default, Quaternion rotation = default)
        {
            NGResourceManager manager = new NGResourceManager();
            var go = manager.InstantiatePrefab(prefabname, position, rotation);
            NGView[] views = NGViewUtils.GetViews(go);
            //NGLogger.LogDebug(views.Length.ToString());
            if (isLocal)
            {
                uint i = 1;
                uint[] ids = new uint[views.Length];
                foreach (var var in views)
                {
                    var.PlayerID = peerid;
                    var.ViewID = peerid * 1000 + i;
                    ids[i - 1] = var.ViewID;
                    i++;
                    NGViewContainer.RegisterView(var);
                }
                LocalPlayer.GO = go;
                Instantiate(prefabname, position, rotation, ids);
            }
            else
            {
                for (int i = 0; i < views.Length; i++)
                {
                    //NGLogger.LogDebug("Print View ID" + views[i].ViewID);
                    views[i].ViewID = viewids[i];
                    views[i].PlayerID = peerid;
                    NGViewContainer.RegisterView(views[i]);
                }
                var player = NGInterface.CurrentRoom().GetNGPlayer(peerid);
                player.GO = go;

            }

            return go;
        }

        public void ResponseProcessor(ResponseOperationMessage rmsg)
        {
            switch (rmsg.MsgType)
            {
                case MessageType.CreateRoom:
                    var crParams = rmsg.CrrParams;
                    switch (crParams.ReturnValue)
                    {
                        case (uint)ResponseMessageInfo.CreateRoomSuccess:
                            LocalPlayer = new NGPlayer(true, crParams.PeerId);
                            NGEvent.OnCreatedRoom();
                            break;
                        case (uint)ResponseMessageInfo.CreateRoomAlreadyExist:
                            NGEvent.OnCreateRoomFailed(crParams.Message);
                            break;
                    }
                    break;

                case MessageType.JoinRoom:

                    var jrrParams = rmsg.JrrParams;

                    switch (jrrParams.ReturnValue)
                    {
                        case (uint)ResponseMessageInfo.JoinRoomSuccess:
                            NGLogger.LogDebug("JoinRoomSuccess responsee");
                            LocalPlayer = new NGPlayer(true, jrrParams.PeerId);
                            NGEvent.OnJoinedRoom();
                            break;
                        case (uint)ResponseMessageInfo.JoinRoomAlreadyInRoom:
                            NGEvent.OnJoinRoomFailed();
                            break;
                        case (uint)ResponseMessageInfo.JoinRoomFull:
                            NGEvent.OnJoinRoomFailed();
                            break;
                        default:
                            NGLogger.LogError("The return value from JoinRoom or JoinOrCreateRoom" + jrrParams.ReturnValue + " cannot be identified.");
                            break;
                    }
                    break;
                case MessageType.LeaveRoom:
                    var lrParams = rmsg.LrrParams;
                    switch (lrParams.ReturnValue)
                    {
                        case (uint)ResponseMessageInfo.LeaveRoomSuccess:

                            NGEvent.OnLeftRoom();
                            break;
                    }
                    break;
            }
        }

        public void JoinOrCreateRoom(string roomid, uint maxnumber)
        {
            SendMessage creatroom = new SendMessage();
            creatroom.MsgType = MessageType.JoinOrCreateRoom;

            creatroom.JocrParams = new JoinOrCreateRoomParams();
            creatroom.JocrParams.RoomId = roomid;
            creatroom.JocrParams.MaxNumber = maxnumber;

            var buf = NGMessageCodec.Encode(creatroom.ToByteArray());
            NGNetwork.Socket.Send(buf);

            this.RoomName = roomid;
        }
        public void CreateRoom(string roomid, uint maxnumber)
        {
            SendMessage creatroom = new SendMessage();
            creatroom.MsgType = MessageType.CreateRoom;
            creatroom.CrParams = new CreateRoomParams();
            creatroom.CrParams.RoomId = roomid;
            creatroom.CrParams.MaxNumber = maxnumber;

            var buf = NGMessageCodec.Encode(creatroom.ToByteArray());
            NGNetwork.Socket.Send(buf);

            this.RoomName = roomid;
        }

        public void JoinRoom(string roomid)
        {
            SendMessage joinroom = new SendMessage();
            joinroom.MsgType = MessageType.JoinRoom;
            joinroom.JrParams = new JoinRoomParams();
            joinroom.JrParams.RoomId = roomid;

            var buf = NGMessageCodec.Encode(joinroom.ToByteArray());
            NGNetwork.Socket.Send(buf);

            this.RoomName = roomid;
        }

        public void LeaveRoom()
        {
            SendMessage leaveroom = new SendMessage();
            leaveroom.MsgType = MessageType.LeaveRoom;

            var buf = NGMessageCodec.Encode(leaveroom.ToByteArray());
            NGNetwork.Socket.Send(buf);

            NGInterface.CurrentRoom().ClearPlayers();
        }

        public void SubscribeMsgChannels(uint[] channelids)
        {
            SendMessage submsg = new SendMessage();
            submsg.MsgType = MessageType.SubscribeMsgChannels;
            submsg.SmcParams.Channelids.AddRange(channelids);

            var buf = NGMessageCodec.Encode(submsg.ToByteArray());
            NGNetwork.Socket.Send(buf);
        }

        public void UnsubscribeMsgChannels(uint[] channelids)
        {
            SendMessage unsubmsg = new SendMessage();
            unsubmsg.MsgType = MessageType.UnSubscribeMsgChannels;
            unsubmsg.UsmcParams.Channelids.AddRange(channelids);

            var buf = NGMessageCodec.Encode(unsubmsg.ToByteArray());
            NGNetwork.Socket.Send(buf);
        }

        public void Instantiate(string prefabname, Vector3 position, Quaternion rotation, uint[] viewids)
        {
            SendMessage i = new SendMessage();
            i.MsgType = MessageType.Instantiation;

            i.IParams = new InstantiationParams();
            i.IParams.PrefabName = prefabname;
            i.IParams.Position = position;
            i.IParams.Rotation = rotation;
            i.IParams.ViewIDs.AddRange(NGProtobufConverter<uint>.Array2RepeatedField(viewids));

            var buf = NGMessageCodec.Encode(i.ToByteArray());

            NGNetwork.Socket.Send(buf);
        }


    }
}
