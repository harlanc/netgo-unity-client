using System;
using System.Collections.Generic;
using UnityEngine;
using Netgo.Library;


namespace Netgo.Client
{
    public class NGViewStream
    {
        public List<NGAny> mSendStream;
        public List<NGAny> mReceiveStream;
        public NGVector3 ngv;
        public NGQuaternion ngq;

        public int Intvalue;

        public NGViewStream()
        {
            mSendStream = new List<NGAny>();
            mReceiveStream = new List<NGAny>();

            ngv = new NGVector3();
            ngq = new NGQuaternion();
         
        }

        public void Send(object any)
        {
            if (any is UInt32)
            {
                mSendStream.Add(new NGAny((UInt32)any));
            }
            else if (any is Vector3)
            {
                mSendStream.Add(new NGAny((Vector3)any));
            }
            else if (any is Quaternion)
            {
                mSendStream.Add(new NGAny((Quaternion)any));
            }
        }

        public object Receive()
        {
            if (mReceiveStream.Count > 0)
            {
                NGAny ngany = mReceiveStream[0];
                mReceiveStream.RemoveAt(0);
                switch (ngany.NgTypeCase)

                {
                    case NGAny.NgTypeOneofCase.NgUint32:
                        return ngany.NgUint32;
                    case NGAny.NgTypeOneofCase.NgVector3:
                        return ngany.NgVector3;
                    case NGAny.NgTypeOneofCase.NgQuaternion:
                        return ngany.NgQuaternion;
                }
            }

            return null;
        }
    }
}
