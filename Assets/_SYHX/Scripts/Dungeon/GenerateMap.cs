using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    
    public static Dictionary<int,GameObject> roomDictionary = new Dictionary<int, GameObject>();
    //DoorObject
    public GameObject Door;


    private static System.Random rnd = new System.Random();
    public GameObject player;
    public static int mapSize = 7;

    private int mapWidth= mapSize;
    private int mapHeight = mapSize;
    private int[] mapArray;

    private float minDistance = 4f; //minimal distance of start to end
    private int roomWeight = 100;
    private int totalWeight = 250;
    public int roomType = 5; //number of room type

    public static int startPoint;
    public static int endPoint;
    
    public static Dungeon mDungeon;
    public static CharacterContent mCharacter;
    public bool Loaded=false;
    //public int[] vertDoor = new int[MapSize * (MapSize - 1)];
    //public int[] horiDoor = new int[(MapSize-1) * MapSize ];
    // Use this for initialization

    public void LoadDungeonData(Dungeon dungeon,CharacterContent cc)
    {
        mDungeon = dungeon;
        mCharacter = cc;
    }

    void Start ()
    {
        DungeonStatus ds = SceneStatusManager.Ins.current as DungeonStatus;
        LoadDungeonData(ds.Dungeon, ds.cc);
        makeDictionary();
        loadMap();  
    }

    public void clearMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void loadMap()
    {
        mapArray = generateRandomMapArray();
        GenerateRoom(mapArray);
        //GenerateDoor();
    }

    void makeDictionary()
    {   //add the room here
        roomDictionary.Add(0, mDungeon.StartRoom);
        roomDictionary.Add(1, mDungeon.EndRoom);
        for (int i = 2;i<mDungeon.DungeonRooms.Count+2;i++)
        {
            roomDictionary.Add(i , mDungeon.DungeonRooms[i-2]);
        }
        print(mDungeon.DungeonRooms.Count);
        roomType = mDungeon.DungeonRooms.Count+2;
        //roomDictionary.Add(2, treasureRoom);
        //roomDictionary.Add(3, eventRoom);
        //roomDictionary.Add(4, battleRoom);
        //roomDictionary.Add(5, ???Room);
    }

    int[] generateRandomMapArray()
    {
        var mapArray = new int[mapWidth * mapHeight];
        var roomSequence = generateList(mapWidth * mapHeight);
        startPoint = roomSequence[0] - 1;
        mapArray[startPoint] = 0; //set initRoom
        endPoint = roomSequence[1] - 1;
        mapArray[endPoint] = 1; //set endRoom
        
        //set rest room
        for (int i = 2; i < mapWidth * mapHeight; i++)
        {
            int randomResult = rnd.Next(2,roomType); // Room type range
            mapArray[roomSequence[i]-1] = checkWeight(randomResult,1);
        }
        return mapArray;
    }

    int checkWeight(int result, int weight)
    {   //TODO
        return result / weight;
    }

    List<int> generateList(int n)
    {
        var numberList = Enumerable.Range(1, n).ToList();
        //Shuffle<int>(numberList);
        while (checkStartEndDistance(numberList[0], numberList[1]) == false)
        {
            int i = n;
            while (i > 1)
            {
                i--;
                int k = rnd.Next(i + 1);
                int value = numberList[k];
                numberList[k] = numberList[i];
                numberList[i] = value;
            }   
        }

        return numberList;
    }

    bool checkStartEndDistance(int s, int e)  //Script to check distance between start and end point.
    {
        Vector3 startPos = new Vector3((s % mapWidth) * 1.5f + 0.5f, 0, Mathf.Floor(s/ mapHeight) * 1.5f + 0.5f);
        Vector3 endPos = new Vector3((e % mapWidth) * 1.5f + 0.5f, 0, Mathf.Floor(e / mapHeight) * 1.5f + 0.5f);
        print(Vector3.Distance(startPos, endPos));
        if (Vector3.Distance(startPos, endPos) > minDistance) 
        {
            return true;
        }
        return false;
    }

    void GenerateRoom (int[] mapArray) //Generate Room according to the map array
    {
        foreach (var item in mapArray.Select((v, i) => new { v, i }))
        {
            GameObject room = Instantiate(roomDictionary[item.v], new Vector3((item.i % mapWidth) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / mapHeight) * 1.5f + 0.5f), Quaternion.identity);
            room.name = "Room " + item.i;
            room.GetComponent<BattleRoomScript>().changeType(item.v);
            room.transform.parent = this.transform;
            if (item.i == startPoint) //initialize player position to start room
            {
                player.transform.position = room.transform.position;
                BattleRoomScript.currentRoomNum = startPoint;
            }
        }
    }

    void GenerateDoor()
    {   
        for (int i = 0; i <= mapSize*mapSize-1-mapSize; i++) //generate Vertical door
        {
            
            var door = Instantiate(Door, new Vector3((i % mapSize) * 1.5f + 0.5f, 0, Mathf.Floor(i / mapSize) * 1.5f + 1.25f), Quaternion.identity);
            door.name = "DoorV " + i;
            door.transform.parent = this.transform;
            
        }
        for (int i = 0; i <= mapSize * mapSize - 1 - mapSize; i++)        //generate Horizontal door
        {
            var door = Instantiate(Door, new Vector3((i % (mapSize - 1)) * 1.5f + 1.25f, 0, Mathf.Floor(i / (mapSize - 1)) * 1.5f + 0.5f), Quaternion.identity);
            door.name = "DoorH " + i;
            door.transform.Rotate(new Vector3(0, 90f, 0));
            door.transform.parent = this.transform;
        }
    }
}
