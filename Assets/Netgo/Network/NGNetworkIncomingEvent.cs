using System;
using UnityEngine;

namespace Netgo.Network
{
    public class NGNetworkEvent
    {
        public delegate void DelegateOnConnected();
        public static DelegateOnConnected OnConnected;
    }
    public interface NGISocketStatusEvent
    {
        void onConnected();
    }
    public class NGNetworkIncomingEvent : MonoBehaviour, NGISocketStatusEvent
    {
        public virtual void onConnected() { }
        public void OnEnable()
        {
            NGNetworkEvent.OnConnected += this.onConnected;
        }

        public void OnDisable()
        {
            NGNetworkEvent.OnConnected -= this.onConnected;
        }
    }
}
