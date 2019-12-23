//-------------------------------------------------
//                    TNet 3
// Copyright © 2012-2018 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using Netgo.Library;


/// <summary>
/// Very simple event manager script that sends out basic touch and mouse-based notifications using NGUI's syntax.
/// </summary>

[RequireComponent(typeof(Camera))]
public class TouchEventCatcher : MonoBehaviour
{
    public LayerMask eventReceiverMask = -1;

    Camera mCam;
    void Awake() { mCam = GetComponent<Camera>(); }
    void Update()
    {
        // Touch notifications
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began &&
                touch.phase != TouchPhase.Moved &&
                touch.phase != TouchPhase.Stationary)
            {
                SendOnClick(touch.position);
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                NGLogger.LogDebug("on Click");
                SendOnClick(Input.mousePosition);
            }
        }
    }
    void SendOnClick(Vector2 pos)
    {
        GameObject go = GetGameObjectShootedByRay(pos);
        if (go != null)
            go.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);

    }
    GameObject GetGameObjectShootedByRay(Vector3 pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(mCam.ScreenPointToRay(pos), out hit, 300f, eventReceiverMask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
