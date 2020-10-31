using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSectionControler : MonoBehaviour
{
    public int part_in_section = 5;
    public int field_size = 10;
    public Room[] RoomPartsPerf;
    public Room[] ExitRoomPerf;
    public Room StartingRoom;


    private Room[,] RoomsFiled;

    // Start is called before the first frame update
    void Start()
    {
        CreateSection();
    }

    public void CreateSection()
    {
        RoomsFiled = new Room[field_size,field_size];
        RoomsFiled[5,5] = StartingRoom;
        StartingRoom.LockAllDoors();

        for (int i = 0; i < part_in_section; i++)
        {
            CreateNewRoom();
        }
        CreateEndRoom();
    }

    private void CreateNewRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for (int x = 0; x < field_size; x++)
        {
            for (int y = 0; y < field_size; y++)
            {
                if (RoomsFiled[x,y] == null) continue;

                if (x > 0 && RoomsFiled[x-1,y] == null) vacantPlaces.Add(new Vector2Int(x-1,y));
                if (y > 0 && RoomsFiled[x,y-1] == null) vacantPlaces.Add(new Vector2Int(x,y-1));
                if (x < field_size - 1 && RoomsFiled[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1,y));
                if (y > field_size - 1 && RoomsFiled[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x,y + 1));

                
            }
        }

        Room newRoom = Instantiate(RoomPartsPerf[Random.Range(0, RoomPartsPerf.Length)]);
        int limit = 100;

        while (limit-- > 0)
        {
            
            Vector2Int pos = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (newRoom.rotatable)
            {
                newRoom.RotateRandomly();
            }
            if (ConnectRoom(newRoom, pos))
            {
                newRoom.transform.position = new Vector3(pos.x - 5, pos.y - 5 , 0) * 10;
                
                newRoom.activateRoom();

                RoomsFiled[pos.x, pos.y] = newRoom;
                return;
            } 
        }
        Destroy(newRoom);
        
    }

    private bool ConnectRoom(Room room, Vector2Int p)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        if (room.Door_U != null && p.y < field_size - 1 && RoomsFiled[p.x , p.y + 1]?.Door_B != null) neighbors.Add(Vector2Int.up);
        if (room.Door_B != null && p.y > 0 && RoomsFiled[p.x , p.y - 1]?.Door_U != null) neighbors.Add(Vector2Int.down);

        if (room.Door_R != null && p.x < field_size - 1 && RoomsFiled[p.x + 1 , p.y]?.Door_L != null) neighbors.Add(Vector2Int.right);
        if (room.Door_L != null && p.x > 0 && RoomsFiled[p.x - 1 , p.y]?.Door_R != null) neighbors.Add(Vector2Int.left);

        if (neighbors.Count == 0) return false;

        Vector2Int selectenDirection = neighbors[Random.Range(0, neighbors.Count)];

        Room selectedRoom = RoomsFiled[p.x + selectenDirection.x, p.y + selectenDirection.y];

        if (selectenDirection == Vector2Int.up)
        {
            room.Door_U.SetActive(false);
            selectedRoom.Door_B.SetActive(false);
        }
        if (selectenDirection == Vector2Int.down)
        {
            room.Door_B.SetActive(false);
            selectedRoom.Door_U.SetActive(false);
        }
        if (selectenDirection == Vector2Int.right)
        {
            room.Door_R.SetActive(false);
            selectedRoom.Door_L.SetActive(false);
        }
        if (selectenDirection == Vector2Int.left)
        {
            room.Door_L.SetActive(false);
            selectedRoom.Door_R.SetActive(false);
        }

        return true;
    }

    private void CreateEndRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for (int x = 0; x < field_size; x++)
        {
            for (int y = 0; y < field_size; y++)
            {
                if (RoomsFiled[x,y] == null) continue;

                if (x > 0 && RoomsFiled[x-1,y] == null) vacantPlaces.Add(new Vector2Int(x-1,y));
                if (y > 0 && RoomsFiled[x,y-1] == null) vacantPlaces.Add(new Vector2Int(x,y-1));
                if (x < field_size - 1 && RoomsFiled[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1,y));
                if (y > field_size - 1 && RoomsFiled[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x,y + 1));

                
            }
        }

        Room newRoom = Instantiate(ExitRoomPerf[Random.Range(0, ExitRoomPerf.Length)]);

        while (true)
        {
            
            Vector2Int pos = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));

            if (ConnectRoom(newRoom, pos))
            {
                newRoom.transform.position = new Vector3(pos.x - 5, pos.y - 5 , 0) * 10;
                RoomsFiled[pos.x, pos.y] = newRoom;
                break;
            } 
        }

    }
}
