// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerMenu.cs" company="NetGO">
//   Part of: netgo-unity-client
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Netgo.Client;
using Netgo.Library;

[ExecuteInEditMode]
public class LobbyMenu : MonoBehaviour
{
    public GUISkin Skin;
    public Vector2 WidthAndHeight = new Vector2(600, 400);
    private string roomName = "myRoom";

    private Vector2 scrollPos = Vector2.zero;

    private bool connectFailed = false;

    public static readonly string LobbySceneName = "DemoLobbyScene";

    public static readonly string CubeSceneName = "DemoCubeScene";

    private string errorDialog;
    private double timeToClearDialog;

    public string ErrorDialog
    {
        get { return this.errorDialog; }
        private set
        {
            this.errorDialog = value;
            if (!string.IsNullOrEmpty(value))
            {
                this.timeToClearDialog = Time.time + 4.0f;
            }
        }
    }
    public void OnGUI()
    {
        DrawLobbyMenu();
    }


    string mRoomName = "MyRoomName";
    uint mRoomCapacity = 10;
    string mMessage = "";



    void DrawLobbyMenu()
    {
        float rectwidth = 500f;
        float rectheight = 900f;

        Rect rect = new Rect(Screen.width / 2 - rectwidth / 2,
            Screen.height / 2 - 100f, rectwidth, rectheight);

        // Show a half-transparent box around the upcoming UI
        GUI.color = new Color(0f, 0f, 0f, 1f);
        GUI.Box(GUIUtils.ProcessRect(rect, 8f), "");
        GUI.color = Color.white;


        float buttonwidth = rectwidth;
        float buttonheight = 80;
        GUILayoutOption[] buttonoptions = { GUILayout.Width(buttonwidth), GUILayout.Height(buttonheight) };

        float textwidth = 270;
        float textheight = 60;
        GUILayoutOption[] textoptions = { GUILayout.Width(textwidth), GUILayout.Height(textheight) };


        GUILayout.BeginArea(rect);
        {
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Room Name:");
            mRoomName = GUILayout.TextField(mRoomName, textoptions);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Room Capacity:");
            string roomcapacity = "10";
            roomcapacity = GUILayout.TextField(roomcapacity, textoptions);
            GUILayout.EndHorizontal();

            GUILayout.Space(30);

            if (!uint.TryParse(roomcapacity, out mRoomCapacity))
            {
                NGLogger.LogError("The Room Capacity cannot be parsed to uint");
            }
            if (GUILayout.Button("Join Or Create Room", buttonoptions))
            {
                NGInterface.JoinOrCreateRoom(mRoomName, mRoomCapacity);
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Create Room", buttonoptions))
            {
                NGInterface.CreateRoom(mRoomName, mRoomCapacity);
            }
            GUILayout.Space(5);

            // GUILayout.BeginHorizontal();
            // GUILayout.Space((rectwidth - buttonwidth) / 2);
            if (GUILayout.Button("Join Room", buttonoptions))
            {
                NGInterface.JoinRoom(mRoomName);
            }
            //GUILayout.EndHorizontal();
            
            GUI.backgroundColor = Color.white;

            if (!string.IsNullOrEmpty(mMessage)) GUILayout.Label(mMessage);
        }
        GUILayout.EndArea();


    }







}
