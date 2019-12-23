using System;
using System.Collections.Generic;
using Netgo.Library;

using Google.Protobuf.Collections;

namespace Netgo.Client
{
    public class NGViewContainer
    {
        public static Dictionary<UInt32, NGView> ID2Views = new Dictionary<uint, NGView>();

        public ViewSyncParams SerializeAll()
        {
            ViewSyncParams paramss = new ViewSyncParams();
            foreach (var view in ID2Views.Values)
            {
                ViewSyncDataParams param = view.Serialize();
                paramss.VsdParams.Add(param);
            }

            return paramss;
        }

        public void DeserializeAll(RepeatedField<ViewSyncDataParams> vsdparams)
        {
            if (ID2Views.Count == 0)
                return;
            foreach (var val in vsdparams)
            {
                NGViewStream stream = new NGViewStream();
                stream.mReceiveStream = NGProtobufConverter<NGAny>.RepeatedField2List(val.ViewSyncData);
                //NGLogger.LogDebug(val.ViewID.ToString());
                ID2Views[val.ViewID].Deserialization(stream);
            }

        }

        public static void RegisterView(NGView newview)
        {
            if (!ID2Views.ContainsKey(newview.ViewID))
            {
                ID2Views.Add(newview.ViewID, newview);
            }

        }

        public static void UnRegisterView(UInt32 viewid)
        {
            ID2Views.Remove(viewid);
        }

        public NGView GetViewByID(uint viewid)
        {
            return ID2Views[viewid];
        }
    }
}
