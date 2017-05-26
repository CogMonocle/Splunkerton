using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDetector : MonoBehaviour
{
    public RoomInfo scene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        
        if (p != null)
        {
            GameManagerController.instance.SetRoom(scene);
        }
    }
}
