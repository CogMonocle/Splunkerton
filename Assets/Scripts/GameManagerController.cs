using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController instance;

    private RoomInfo[] currentRooms;
    private Vector2 playerLoc;
    private Vector3[] roomPositions;

    public RoomInfo initialRoom;
    public Vector2 roomSpacing;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        roomPositions = new Vector3[4];
        roomPositions[0] = Vector3.right * roomSpacing.x;
        roomPositions[1] = Vector3.up * roomSpacing.y;
        roomPositions[2] = Vector3.left * roomSpacing.x;
        roomPositions[3] = Vector3.down * roomSpacing.y;
    }

    void Start()
    {
        //playerLoc = new Vector2(0, 6);
        //PlayerController.instance.transform.position = playerLoc;
        currentRooms = new RoomInfo[5];
        SetRoom(initialRoom);
    }

    public void SetRoom(RoomInfo room)
    {
        if (currentRooms[0] != null)
        {
            if (room == currentRooms[0])
                return;

            foreach (EnemySpawn e in currentRooms[0].GetComponentsInChildren<EnemySpawn>())
            {
                e.Despawn();
            }
            DropHandler.instance.DespawnDrops();
        }

        int roomDirection = -1;

        for(int i = 0; i < 4; i++)
        {
            if(currentRooms[i + 1] == room)
            {
                roomDirection = i;
            }
            else
            {
                if(currentRooms[i + 1] != null)
                {
                    Destroy(currentRooms[i + 1].gameObject);
                }
            }
            currentRooms[i + 1] = null;
        }

        roomDirection += 2;
        roomDirection %= 4;

        for(int i = 0; i < 4; i++)
        {
            if(i == roomDirection)
            {
                currentRooms[i + 1] = currentRooms[0];
               
            }
            else if(room.adjacentRooms[i] != null)
            {
                currentRooms[i + 1] = Instantiate(room.adjacentRooms[i], room.transform.position + roomPositions[i], Quaternion.identity, transform);
            }
        }
        currentRooms[0] = room;

        CameraController.mainCam.cameraBounds = room.cameraBounds;
        CameraController.mainCam.GetComponent<Camera>().backgroundColor = room.cameraBG;
        foreach(EnemySpawn e in room.GetComponentsInChildren<EnemySpawn>())
        {
            e.Spawn();
        }
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
