using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmongNetworkManager : NetworkManager
{
    public GameManager gameManager;
    public override void OnClientConnect(NetworkConnection conn)
    {
        if (!clientLoadedScene)
        {

            if (!NetworkClient.ready) NetworkClient.Ready();
            NetworkClient.AddPlayer();
            // NetworkClient.localPlayer.GetComponent<PlayerControll>().gameManager = gameManager;
        }
        

        base.OnClientConnect(conn);
    }

    // public override void OnClientDisconnect(NetworkConnection conn)
    // {
    //     gameManager.CameraFromPlayer(NetworkClient.localPlayer.transform);
    //     base.OnClientDisconnect(conn);
    // }
}