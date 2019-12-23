// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerInGame.cs" company="NetGO">
//   Part of: netgo-unity-client
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using Netgo.Client;
using Netgo.Network;

public class CubeInstantiator : MonoBehaviour
{
    public Transform mMyCubePrefab;

    public void Start()
    {
        if (!NGNetwork.IsConnected)
        {
            SceneManager.LoadScene(LobbyMenu.LobbySceneName);
            return;
        }
        var go = NGInterface.CurrentRoom().Instantiate(NGInterface.CurrentRoom().LocalPlayer.mPeerId, true, null, this.mMyCubePrefab.name, transform.position, Quaternion.identity);
        CubeController.Instance.mLocalCube = go;

    }
    public void OnLeftRoom()
    {
        Debug.LogError("OnLeftRoom (local)");

        // back to main menu
        SceneManager.LoadScene(LobbyMenu.LobbySceneName);
    }

}