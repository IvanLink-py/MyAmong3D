// using Mirror;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class AmongNetworkManager : NetworkManager
// {
//     public Transform playerCamera;
//     public override void OnClientConnect(NetworkConnection conn)
//     {
//         base.OnClientConnect(conn);
//         ActivatePlayer(conn);
//     }
//     
//     void ActivatePlayer(NetworkConnection conn)
//     {
//         GameObject player = Instantiate(playerPrefab);
//         PlayerControll pc = player.GetComponent<PlayerControll>();
//         pc.myCamera = playerCamera;
//         pc.hasCamera = true;
//         player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
//         NetworkServer.AddPlayerForConnection(conn, player);
//     }
//     
// }

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmongNetworkManager : NetworkManager
{
    bool playerSpawned;
    NetworkConnection connection;
    bool playerConnected;
    public Transform playerCamera;

    public void OnCreateCharacter(NetworkConnection conn, PosMessage message)
    {
        GameObject go = Instantiate(playerPrefab, message.vector3, Quaternion.identity); //локально на сервере создаем gameObject
        NetworkServer.AddPlayerForConnection(conn, go); //присоеднияем gameObject к пулу сетевых объектов и отправляем информацию об этом остальным игрокам
        var go_pc = go.GetComponent<PlayerControll>();
        playerCamera.SetParent(go.transform);
        playerCamera.position = Vector3.zero;
        playerCamera.rotation = Quaternion.identity;
        go_pc.myCamera = playerCamera;
        go_pc.hasCamera = true;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PosMessage>(OnCreateCharacter); //указываем, какой struct должен прийти на сервер, чтобы выполнился свапн
    }

    public void ActivatePlayerSpawn()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        pos = Vector3.zero;

        PosMessage m = new PosMessage() { vector3 = pos }; //создаем struct определенного типа, чтобы сервер понял к чему эти данные относятся
        connection.Send(m); //отправка сообщения на сервер с координатами спавна
        playerSpawned = true;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        connection = conn;
        playerConnected = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !playerSpawned && playerConnected)
        {
            ActivatePlayerSpawn();
        }
    }
}

public struct PosMessage : NetworkMessage //наследуемся от интерфейса NetworkMessage, чтобы система поняла какие данные упаковывать
{
    public Vector3 vector3; //нельзя использовать Property
}