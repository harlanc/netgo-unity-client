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
        void OnConnected();
    }
    public class NGNetworkIncomingEvent : MonoBehaviour, NGISocketStatusEvent
    {
        public virtual void OnConnected() { }
        public void OnEnable()
        {
            NGNetworkEvent.OnConnected += this.OnConnected;
        }

        public void OnDisable()
        {
            NGNetworkEvent.OnConnected -= this.OnConnected;
        }
    }
}
