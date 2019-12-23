//-------------------------------------------------
//                    TNet 3
// Copyright © 2012-2018 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using Netgo.Client;
using Netgo.Library;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class CubeController : MonoBehaviour
{

    public GameObject mLocalCube;

    public static CubeController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        mCurY = 0.55f;
    }
    public static double mMapLeftX = -3f;
    public static double mMapRightX = 3f;

    public static double mMapLeftZ = -5f;
    public static double mMapRightZ = 5f;

    public static float mCurX, mCurY, mCurZ;

    public double GetRandomNumberInRange(double minNumber, double maxNumber)
    {
        return new System.Random().NextDouble() * (maxNumber - minNumber) + minNumber;
    }
    public int GetRandomRotationY()
    {
        return new System.Random().Next(0, 360);
    }
    public void UpdatePosition()
    {
        mCurX = (float)GetRandomNumberInRange(mMapLeftX, mMapRightX);
        mCurZ = (float)GetRandomNumberInRange(mMapLeftZ, mMapRightZ);

        if (mLocalCube != null)
            mLocalCube.transform.position = new Vector3(mCurX, mCurY, mCurZ);
    }
    public void UpdateRotation()
    {
        Vector3 rotation = new Vector3(0, GetRandomRotationY(), 0);

        if (mLocalCube != null)
            mLocalCube.transform.localEulerAngles = new Vector3(0, GetRandomRotationY(), 0);
    }
    public bool mNeedMove = false;
    public IEnumerator Move()
    {
        while (mNeedMove)
        {
            yield return new WaitForSeconds(2);
            UpdatePosition();
            yield return new WaitForSeconds(2);
            UpdateRotation();
        }
    }
    public void OnGUI()
    {
        float rectwidth = 200f;
        float rectheight = 300f;

        Rect rect = new Rect(Screen.width * 0.02f, Screen.height * 0.02f, rectwidth, rectheight);


        GUI.color = Color.black;
        GUI.Box(GUIUtils.ProcessRect(rect, 8f), "");
        GUI.color = Color.white;

        float buttonwidth = rectwidth;
        float buttonheight = 50;
        GUILayoutOption[] buttonoptions = { GUILayout.Width(buttonwidth), GUILayout.Height(buttonheight) };

        float buttonspace = 15;

        GUILayout.BeginArea(rect);
        {
            GUILayout.Space(5);

            if (GUILayout.Button("Back to Lobby", buttonoptions))
            {
                NGInterface.LeaveRoom();
                SceneManager.LoadScene(LobbyMenu.LobbySceneName);
            }

            GUILayout.Space(buttonspace);

            if (GUILayout.Button("Move", buttonoptions))
            {
                mNeedMove = true;
                StartCoroutine(Move());

            }

            GUILayout.Space(buttonspace);

            if (GUILayout.Button("Stop", buttonoptions))
            {
                mNeedMove = false;
            }

            GUILayout.Space(buttonspace);

            if (GUILayout.Button("Jump", buttonoptions))
            {
                if (mLocalCube != null)
                {
                    var curposition = mLocalCube.transform.position;
                    mLocalCube.transform.position = new Vector3(curposition.x, curposition.y * 10, curposition.z);
                }
            }
        }
        GUILayout.EndArea();
    }
}
