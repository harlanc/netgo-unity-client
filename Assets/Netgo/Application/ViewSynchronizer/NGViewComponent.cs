using System;
using UnityEngine;
using Netgo.Library;

namespace Netgo.Client
{
    public class NGViewComponent : MonoBehaviour, INGSerialize
    {
        public bool IsSyncPosition;
        public bool IsSyncRotation;
        public void SerializeViewComponent(NGViewStream stream)
        {
            if (IsSyncPosition)
            {
                stream.Send(this.transform.position);
            }
            if (IsSyncRotation)
            {
                stream.Send(this.transform.rotation);
            }

        }
        public void DeserializeViewComponent(NGViewStream stream)
        {
            if (IsSyncPosition)
            {
                NGLogger.LogInfo("position");
                this.transform.position = (NGVector3)stream.Receive();
            }

            if (IsSyncRotation)
            {
                NGLogger.LogInfo("IsSyncRotation");
                this.transform.rotation = (NGQuaternion)stream.Receive();
            }


        }
    }
}
