using System.Collections.Generic;
using System.Collections;
using System.IO;
//https://stackoverflow.com/questions/2522376/how-to-choose-between-protobuf-csharp-port-and-protobuf-net
using Google.Protobuf;
using Netgo.Library;

using Google.Protobuf.WellKnownTypes;
using UnityEngine;
using Netgo.Network;


namespace Netgo.Network
{

    public class NGMessageReceiver : MonoBehaviour
    {
      
        public List<byte> mReceiveCache;

        public delegate void NGMessageProcessor(byte [] data);
        public NGMessageProcessor MessageProcessor;

        public static NGMessageReceiver Instance;

     

        private void Awake()
        {
             DontDestroyOnLoad(this.gameObject);
            if(Instance == null)
            {
                Instance = this;
            }
            //mSocket = NGGameClient.mSocket;
            mReceiveCache = new List<byte>();
            StartReceive();
        }


        public void StartReceive()
        {
            NGNetwork.Socket.Receive();
        }
        private void Update()
        {
            StartCoroutine(Process());
        }

        public IEnumerator Process()
        {
            lock (NGNetwork.Socket.MBufferQueue)
            {
                // NGLogger.LogDebug("Lock BufferQueue");
                while (NGNetwork.Socket.MBufferQueue.Count > 0)
                {
                    //NGLogger.LogDebug("Proceess BufferQueue");
                    NGBuffer buffer = NGNetwork.Socket.MBufferQueue.Dequeue();
                    mReceiveCache.AddRange(buffer.Bytes);
                    byte[] currBytes;
                    while ((currBytes = NGMessageCodec.Decode(ref mReceiveCache)) != null)
                    {
                        //NGLogger.LogDebug("Decode message from BufferQueue");
                        //mReceiveCache.Clear();
                        //ProcessRecieveData(currBytes);
                        MessageProcessor(currBytes);
                    }
                }
            }

            yield return new WaitForSeconds(2);
        }
    }
}
