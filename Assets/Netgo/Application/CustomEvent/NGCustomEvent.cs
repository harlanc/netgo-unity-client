using Netgo.Library;

using Google.Protobuf;
using Netgo.Network;

namespace Netgo.Client
{
    public class NGCustomEvent
    {
        public static void SendCustomEvent(uint eventid, uint[] targetpeerids, NGAny[] customdata)
        {
            SendMessage ce = new SendMessage();
            ce.MsgType = MessageType.CustomEvent;

            ce.CeParams = new CustomEventParams();
            ce.CeParams.EventID = eventid;

            ce.CeParams.TargetPeerIds.AddRange(NGProtobufConverter<uint>.Array2RepeatedField(targetpeerids));
            ce.CeParams.CustomData.AddRange(NGProtobufConverter<NGAny>.Array2RepeatedField(customdata));

            var buf = NGMessageCodec.Encode(ce.ToByteArray());
            NGNetwork.Socket.Send(buf);
        }

    }
}