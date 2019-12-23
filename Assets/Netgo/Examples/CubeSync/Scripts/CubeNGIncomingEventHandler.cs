using Netgo.Library;
using UnityEngine.SceneManagement;
using Netgo.Client;
public class CubeNGIncomingEventHandler : NGIncomingEvent
{
    public override void OnJoinedRoom()
    {
        NGLogger.LogInfo("OnJoinedRoom");
        SceneManager.LoadScene(LobbyMenu.CubeSceneName);
    }

    public override void OnGreatedRoom()
    {
        NGLogger.LogInfo("OnCreatedRoom");
        SceneManager.LoadScene(LobbyMenu.CubeSceneName);
    }

    public override void OnLeftRoom()
    {
        NGLogger.LogInfo("onLeftroom");
    }

    public override void OnOtherPlayerEnteredRoom(NGPlayer player)
    {
        NGLogger.LogInfo("OnOtherPlayerEnteredRoom" + player.mPeerId);
    }

    public override void OnOtherPlayerLeftRoom(NGPlayer player)
    {
        NGLogger.LogInfo("OnOtherPlayerLeftRoom" + player.mPeerId);
    }

     

}