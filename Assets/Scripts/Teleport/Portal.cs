using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    public Transform player;

    public void TeleportPlayer()
    {
        player.transform.position = new Vector2(destination.position.x, destination.position.y);
    }
}
    