//-------------------------------------------------
//                    TNet 3
// Copyright © 2012-2018 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using Netgo.Client;
using Netgo.Library;


/// <summary>
/// This simple script shows how to change the color of an object on all connected clients.
/// You can see it used in Example 1.
/// </summary>


[RequireComponent(typeof(Renderer))]
[ExecuteInEditMode]
public class CubeViewComponent : NGIncomingEvent, INGSerialize
{
    Material mMat;

    NGView view;
    Vector3 mTarget;

    void Awake()
    {
        mMat = GetComponent<Renderer>().material;
        view = this.gameObject.GetComponent<NGView>();
    }

    bool changed = true;
    Vector3 scale1 = new Vector3(2, 2, 2);
    Vector3 scale2 = new Vector3(1, 1, 1);
    void OnClick()
    {
        if (!view.IsMine)
        {
            Vector3 scale;
            if (changed)
            {
                scale = scale1;
            }
            else
            {
                scale = scale2;
            }
            changed = !changed;
            NGCustomEvent.SendCustomEvent(SyncEventID, new uint[] { view.PlayerID }, new NGAny[1] { scale });
        }
        else
        {
            Color color = Color.red;
            if (mMat.color == Color.red)
            {
                color = Color.green;
            }
            else if (mMat.color == Color.green)
            {
                color = Color.blue;
            }

            NGAny colorany = new NGAny(color);
            NGRPC.SendRPC(view.ViewID, "OnColor", RPCTarget.All, new NGAny[1] { colorany });
        }

    }

    /********************************************
    /* RPC sync
    *********************************************/

    [NGRPCMethod]
    public void OnColor(NGAny[] c)
    {
        mMat.color = c[0].NgColor;
    }
    /********************************************
    /* Event communicate
    ********************************************/
    const uint SyncEventID = 1;
    public override void OnCustomEvent(uint eventID, NGAny[] data)
    {
        if (!view.IsMine)
        {
            return;
        }
        switch (eventID)
        {
            case SyncEventID:
                this.transform.localScale = data[0].NgVector3;
                break;
        }
    }
    /********************************************
    /* View Sync
    *********************************************/
    private Vector3 mCorrentPosition = Vector3.zero;
    private Quaternion mCorrentRotation = Quaternion.identity;
    public void SerializeViewComponent(NGViewStream stream)
    {
        stream.Send(this.transform.position);
        stream.Send(this.transform.rotation);
    }
    public void DeserializeViewComponent(NGViewStream stream)
    {
        mCorrentPosition = (NGVector3)stream.Receive();
        mCorrentRotation = (NGQuaternion)stream.Receive();
    }
    void Update()
    {
        if (!view.IsMine)
        {
            transform.position = mCorrentPosition;//Vector3.Lerp(transform.position, mCorrentPosition, Time.deltaTime * 5);
            transform.rotation = mCorrentRotation;//Quaternion.Lerp(transform.rotation, mCorrentRotation, Time.deltaTime * 5);
        }
    }
}
