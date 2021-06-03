using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class AutoConnection : MonoBehaviour
{
    [SerializeField]
    private bool isServer;
    [SerializeField]
    private bool isAutoConnect;
    [SerializeField]
    private string autoConnectAdress;
    [SerializeField]
    private NetworkManager networkManager;

    void Start()
    {
        JoinGame();
    }

    //If isAutoConnect is true then it join or create game
    private void JoinGame()
    {
        if (isAutoConnect && networkManager != null)
        {
            networkManager.networkAddress = autoConnectAdress;
            if (isServer)
            {
                networkManager.StartHost();
            }
            else
            {
                networkManager.StartClient();
            }
        }
    }
}
