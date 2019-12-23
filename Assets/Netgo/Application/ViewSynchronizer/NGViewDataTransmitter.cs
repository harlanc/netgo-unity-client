using System;
using Netgo.Library;
using UnityEngine;
using Google.Protobuf;

using Google.Protobuf.WellKnownTypes;
using Netgo.Network;
namespace Netgo.Client
{
    public class NGViewDataTransmitter : MonoBehaviour
    {
        public static NGViewDataTransmitter Instance;
        //NGSocket mSocket;
        NGViewContainer _container;

        //send how many times in one second.
        private float mTransimitFrequency = 10;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mCurTime = 0;
            _container = new NGViewContainer();
            //mSocket = NGGameClient.mSocket;
        }

        // millonseconds
        public float TransmitInterval
        {
            get
            {
                return 1 / mTransimitFrequency;
            }
        }
        public float mCurTime;

        //https://www.cnblogs.com/hobinly/p/7157481.html
        //https://developers.google.com/protocol-buffers/docs/techniques?csw=1#streaming    
        //https://stackoverflow.com/questions/48558451/using-protobuf-codedinputstream-to-read-from-byte
        private void LateUpdate()
        {
            if (!NGNetwork.IsConnected)
            {
                return;
            }
            mCurTime += Time.deltaTime;
            if (mCurTime > TransmitInterval)
            {
                mCurTime = 0;
                ViewSyncParams paramss = _container.SerializeAll();

                if (paramss.VsdParams.Count == 0)
                {
                    return;
                }

                SendMessage viewsyncmsg = new SendMessage();
                viewsyncmsg.MsgType = MessageType.ViewSync;
                viewsyncmsg.VsParams = paramss;
                
                var buf = NGMessageCodec.Encode(viewsyncmsg.ToByteArray());


                NGNetwork.Socket.Send(buf);
            }
        }
    }
}
