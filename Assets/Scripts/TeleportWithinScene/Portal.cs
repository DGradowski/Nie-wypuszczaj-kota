using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    public Transform player;
    public Transform blueportal, orangeportal;
    

    public void SelectPortal(int id)
    {
       switch(id)
        {
            case 0:
                TeleportPlayer(orangeportal);
             break;
            case 1:
                TeleportPlayer(blueportal);
                break;

        }
    }
    public void TeleportPlayer(Transform destination)
    {
        player.transform.position = new Vector2(destination.position.x, destination.position.y);
    }
}
    