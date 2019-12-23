using System;
using UnityEngine;
using Netgo.Library;

namespace Netgo.Client
{
    public class NGEvent
    {


        public delegate void onGreatedRoom();
        public static onGreatedRoom OnCreatedRoom;

        public delegate void onGreateRoomFailed(string errmsg);
        public static onGreateRoomFailed OnCreateRoomFailed;

        public delegate void onJoinedRoom();
        public static onJoinedRoom OnJoinedRoom;

        public delegate void onJoinRoomFailed();
        public static onJoinRoomFailed OnJoinRoomFailed;

        public delegate void onLeftRoom();
        public static onLeftRoom OnLeftRoom;

        public delegate void onOtherPlayerEnteredRoom(NGPlayer player);
        public static onOtherPlayerLeftRoom OnOtherPlayerEnteredRoom;

        public delegate void onOtherPlayerLeftRoom(NGPlayer player);
        public static onOtherPlayerLeftRoom OnOtherPlayerLeftRoom;

        public delegate void onCustomEvent(uint eventID, NGAny[] data);
        public static onCustomEvent OnCustomEvent;
    }


    public interface NGIRoomEvent
    {
        void OnGreatedRoom();
        void OnGreateRoomFailed(string errmsg);
        void OnJoinedRoom();
        void OnJoinRoomFailed();
        void OnLeftRoom();
    }

    public interface NGIPlayerEvent
    {
        void OnOtherPlayerEnteredRoom(NGPlayer player);
        void OnOtherPlayerLeftRoom(NGPlayer player);
    }

    public interface NGICustomEvent
    {
        void OnCustomEvent(uint eventID, NGAny[] data);
    }

    public class NGIncomingEvent : MonoBehaviour, NGIRoomEvent, NGIPlayerEvent, NGICustomEvent
    {
        public virtual void OnGreatedRoom() { }
        public virtual void OnGreateRoomFailed(string errmsg) { NGLogger.LogDebug("Create room failed " + errmsg); }
        public virtual void OnJoinedRoom() { NGLogger.LogDebug("On Joined Room. "); }
        public virtual void OnJoinRoomFailed() { NGLogger.LogDebug("Join room failed "); }
        public virtual void OnLeftRoom() { }

        public virtual void OnOtherPlayerEnteredRoom(NGPlayer player) { }
        public virtual void OnOtherPlayerLeftRoom(NGPlayer player) { }

        public virtual void OnCustomEvent(uint eventID, NGAny[] data) { }

        public void OnEnable()
        {
            NGEvent.OnCreatedRoom += this.OnGreatedRoom;
            NGEvent.OnCreateRoomFailed += this.OnGreateRoomFailed;
            NGEvent.OnJoinedRoom += this.OnJoinedRoom;
            NGEvent.OnJoinRoomFailed += this.OnJoinRoomFailed;
            NGEvent.OnLeftRoom += this.OnLeftRoom;

            NGEvent.OnOtherPlayerEnteredRoom += this.OnOtherPlayerEnteredRoom;
            NGEvent.OnOtherPlayerLeftRoom += this.OnOtherPlayerLeftRoom;

            NGEvent.OnCustomEvent += this.OnCustomEvent;
        }

        public void OnDisable()
        {
            NGEvent.OnCreatedRoom -= this.OnGreatedRoom;
            NGEvent.OnCreateRoomFailed -= this.OnGreateRoomFailed;
            NGEvent.OnJoinedRoom -= this.OnJoinedRoom;
            NGEvent.OnJoinRoomFailed -= this.OnJoinRoomFailed;
            NGEvent.OnLeftRoom -= this.OnLeftRoom;

            NGEvent.OnOtherPlayerEnteredRoom -= this.OnOtherPlayerEnteredRoom;
            NGEvent.OnOtherPlayerLeftRoom -= this.OnOtherPlayerLeftRoom;

            NGEvent.OnCustomEvent -= this.OnCustomEvent;

        }
    }
}
