using System;
using System.Collections.Generic;
using UnityEngine;
using Netgo.Library;

namespace Netgo.Client
{
    public interface INGSerialize
    {
        void SerializeViewComponent(NGViewStream stream);
        void DeserializeViewComponent(NGViewStream stream);
    }

    public class NGView : MonoBehaviour
    {
        public List<Component> ViewComponents;
        public UInt32 ViewID;
        public uint PlayerID;
        public void Start()
        {
            //IsMine = true;
            // NGViewContainer.RegisterView(this);
        }

        public bool IsMine
        {
            get
            {
                bool rv = (PlayerID == NGInterface.CurrentRoom().LocalPlayer.mPeerId);
                Locally = rv;
                return rv;
            }

        }

        public bool Locally;

        public ViewSyncDataParams Serialize()
        {
            NGViewStream stream = new NGViewStream();
            foreach (var component in ViewComponents)
            {
                var synccomponent = component as INGSerialize;
                if (synccomponent != null)
                {
                    synccomponent.SerializeViewComponent(stream);
                }

            }

            ViewSyncDataParams param = new ViewSyncDataParams();
            param.ViewID = ViewID;
            param.ViewSyncData.AddRange(stream.mSendStream);

            return param;
        }

        public void Deserialization(NGViewStream stream)
        {
            foreach (var component in ViewComponents)
            {
                var synccomponent = component as INGSerialize;
                if (synccomponent != null)
                {
                    synccomponent.DeserializeViewComponent(stream);
                }

            }
        }

        private void OnDestroy() {
            NGViewContainer.UnRegisterView(ViewID);
        }


    }
}
