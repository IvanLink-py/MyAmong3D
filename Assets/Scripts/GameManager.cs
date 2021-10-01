using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform camera;

    public void CameraToPlayer(Transform player)
    {
        camera.SetParent(player);
        camera.position = player.position + new Vector3(0, 0.7f, 0);
        Debug.Log(camera.position);
        camera.rotation = Quaternion.identity;
        player.GetComponent<PlayerControll>().myCamera = camera;
    }
    public void CameraFromPlayer(Transform player)
    {
        Destroy(player.GetChild(0));
        player.DetachChildren();
        camera.position = transform.position;
        camera.rotation = transform.rotation;
    }
}
