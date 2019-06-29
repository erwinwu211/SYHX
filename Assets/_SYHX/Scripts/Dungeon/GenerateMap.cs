using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    public GameObject BattleRoom;
    public GameObject InitRoom;
    public GameObject TreasureRoom;
    public GameObject Door;
    System.Random r = new System.Random(1000);


    public static int mapSize = 7;

    public int mapWidth=7;
    public int mapHeight = 7;
    private int[] mapArray;

    private int roomWeight = 100;
    private int totalWeight = 250;
    //public int[] vertDoor = new int[MapSize * (MapSize - 1)];
    //public int[] horiDoor = new int[(MapSize-1) * MapSize ];
    // Use this for initialization
    void Start () {
        mapArray = new int[mapWidth * mapHeight];
        for (int i =0; i< mapWidth * mapHeight; i++)
        {
             
            var randomResult = r.Next(totalWeight);
            mapArray[i] = checkWeight(randomResult, roomWeight);
        }
        GenerateRoom(mapArray);
        //GenerateDoor(vertDoor, horiDoor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    int checkWeight(int r, int w)
    {
        return r / w;
    }
    void GenerateRandomMap()
    {

    }
    //void GenerateDoor(int[] vertArray, int[] horiArray )
    //{
    //    foreach (var item in vertArray.Select((v, i) => new { v, i }))
    //    {
    //        if (item.v == 1) // Judge door
    //        {
    //            var door = Instantiate(Door, new Vector3((item.i % MapSize) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / MapSize) * 1.5f + 1.25f), Quaternion.identity);
    //            door.name = "DoorV " + item.i;
    //            door.transform.parent = this.transform;
    //        }
    //    }

    //    foreach (var item in horiArray.Select((v, i) => new { v, i }))
    //    {
    //        if (item.v == 1) // Judge door
    //        {
    //            var door = Instantiate(Door, new Vector3((item.i % (MapSize-1)) * 1.5f + 1.25f, 0, Mathf.Floor(item.i / (MapSize-1)) * 1.5f + 0.5f), Quaternion.identity);
    //            door.name = "DoorH " + item.i;
    //            door.transform.Rotate(new Vector3(0, 90f, 0));
    //            door.transform.parent = this.transform;
    //        }
    //    }
    //}


    void GenerateRoom (int[] maparray)
    {
        foreach (var item in maparray.Select((v, i) => new { v, i }))
        {
            if (item.v == 2) // TreasureRoom Room
            {
                var room = Instantiate(TreasureRoom, new Vector3((item.i % mapWidth) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / mapHeight) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
            else if (item.v == 1) // Battle Room
            {
                var room = Instantiate(BattleRoom, new Vector3((item.i % mapWidth) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / mapHeight) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
            else if (item.v == 0) //initial Room
            {
                var room = Instantiate(InitRoom, new Vector3((item.i % mapWidth) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / mapHeight) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
        }
    }
}
