using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class JoinGame : MonoBehaviour
{
    public PlayerSO player;
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
        if (isAutoConnect)
        {
            networkManager.networkAddress = autoConnectAdress;
            if (isServer)
            {
                networkManager.StartHost();
            } else
            {
                networkManager.StartClient();
            }
        }
    }

    public void JoinGameAsClient()
    {
        player.name = GameObject.Find("PlayerNameText").GetComponent<Text>().text;
    }
}
