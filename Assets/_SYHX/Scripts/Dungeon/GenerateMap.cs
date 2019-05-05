using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    public GameObject BattleRoom;
    public GameObject InitRoom;
    public GameObject TreasureRoom;
    public GameObject Door;

    public static int MapSize = 5;
    public int[] Room = new int[MapSize * MapSize];
    public int[] vertDoor = new int[MapSize * (MapSize - 1)];
    public int[] horiDoor = new int[(MapSize-1) * MapSize ];
    // Use this for initialization
    void Start () {

        GenerateRoom(Room);
        GenerateDoor(vertDoor, horiDoor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateDoor(int[] vertArray, int[] horiArray )
    {
        foreach (var item in vertArray.Select((v, i) => new { v, i }))
        {
            if (item.v == 1) // Judge door
            {
                var door = Instantiate(Door, new Vector3((item.i % MapSize) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / MapSize) * 1.5f + 1.25f), Quaternion.identity);
                door.name = "DoorV " + item.i;
                door.transform.parent = this.transform;
            }
        }

        foreach (var item in horiArray.Select((v, i) => new { v, i }))
        {
            if (item.v == 1) // Judge door
            {
                var door = Instantiate(Door, new Vector3((item.i % (MapSize-1)) * 1.5f + 1.25f, 0, Mathf.Floor(item.i / (MapSize-1)) * 1.5f + 0.5f), Quaternion.identity);
                door.name = "DoorH " + item.i;
                door.transform.Rotate(new Vector3(0, 90f, 0));
                door.transform.parent = this.transform;
            }
        }
    }


    void GenerateRoom (int[] maparray)
    {
        foreach (var item in maparray.Select((v, i) => new { v, i }))
        {
            if (item.v == 2) // TreasureRoom Room
            {
                var room = Instantiate(TreasureRoom, new Vector3((item.i % MapSize) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / MapSize) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
            else if (item.v == 1) // Battle Room
            {
                var room = Instantiate(BattleRoom, new Vector3((item.i % MapSize) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / MapSize) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
            else if (item.v == 0) //initial Room
            {
                var room = Instantiate(InitRoom, new Vector3((item.i % MapSize) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / MapSize) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
        }
    }
}
