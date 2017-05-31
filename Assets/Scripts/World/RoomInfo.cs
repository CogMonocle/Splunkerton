using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public int sceneID;
    public Rect cameraBounds;
    public Vector2 position;
    public Color cameraBG;
    public RoomInfo[] adjacentRooms;
    public GameObject scenery;
}
