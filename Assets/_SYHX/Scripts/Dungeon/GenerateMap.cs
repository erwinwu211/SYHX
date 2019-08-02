using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    
    public static Dictionary<int,GameObject> roomDictionary = new Dictionary<int, GameObject>();
    //DoorObject


    private static System.Random rnd = new System.Random();
    public GameObject player;

    private int mapWidth;
    private int mapHeight;
    private int[] mapArray;

    public float minDistance = 4f; //minimal distance of start to end
    public float roomSpacing = 1.2f;
    private int roomWeight = 100;
    private int totalWeight = 250;
    private int count_of_roomType; //number of room type

    public static int startPoint;
    public static int endPoint;
    public List<GameObject> roomList;
    
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
        mapWidth = dungeon.mapWidth;
        mapHeight = dungeon.mapHeight;
        count_of_roomType = mDungeon.DungeonRooms.Count + 2;
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
    }

    /// <summary>
    /// 将房间类型注册进字典
    /// </summary>
    public void makeDictionary()
    {   //add the room here
        roomDictionary.Add(0, mDungeon.StartRoom);
        roomDictionary.Add(1, mDungeon.EndRoom);
        for (int i = 2;i<mDungeon.DungeonRooms.Count+2;i++)
        {
            roomDictionary.Add(i , mDungeon.DungeonRooms[i-2]);
        }
        print(mDungeon.DungeonRooms.Count);
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
            int randomResult = rnd.Next(2,count_of_roomType); // Room type range
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
        Vector3 startPos = new Vector3((s % mapWidth) * roomSpacing + 0.5f, 0, Mathf.Floor(s/ mapHeight) * roomSpacing + 0.5f);
        Vector3 endPos = new Vector3((e % mapWidth) * roomSpacing + 0.5f, 0, Mathf.Floor(e / mapHeight) * roomSpacing + 0.5f);
        print(Vector3.Distance(startPos, endPos));
        if (Vector3.Distance(startPos, endPos) > minDistance) 
        {
            return true;
        }
        return false;
    }

    void GenerateRoom (int[] mapArray) //Generate Room according to the map array
    {
        roomList = new List<GameObject>();
        foreach (var item in mapArray.Select((v, i) => new { v, i }))
        {
            GameObject room = Instantiate(roomDictionary[item.v], new Vector3((item.i % mapWidth) * roomSpacing + 0.5f, 0, Mathf.Floor(item.i / mapHeight) * roomSpacing + 0.5f), Quaternion.identity);
            BattleRoomScript roomScript = room.GetComponent<BattleRoomScript>();
            roomScript.thisRoomRaw = item.i % mapWidth;
            roomScript.thisRoomColumn = item.i / mapHeight;
            room.name = "Room " + item.i;
            room.GetComponent<BattleRoomScript>().changeType(item.v);
            room.transform.parent = this.transform;
            if (item.i == startPoint) //initialize player position to start room
            {
                player.transform.position = room.transform.position;
                DungeonManager.Ins.currentRoomNum = startPoint;
            }
            roomList.Add(room);
        }
        foreach (GameObject room in roomList)
        {
            BattleRoomScript roomScript = room.GetComponent<BattleRoomScript>();
            roomScript.RightRoom = GetRoomByAxis(roomScript.thisRoomRaw + 1, roomScript.thisRoomColumn);
            roomScript.LeftRoom = GetRoomByAxis(roomScript.thisRoomRaw - 1, roomScript.thisRoomColumn);
            roomScript.UpRoom = GetRoomByAxis(roomScript.thisRoomRaw, roomScript.thisRoomColumn + 1);
            roomScript.DownRoom = GetRoomByAxis(roomScript.thisRoomRaw, roomScript.thisRoomColumn - 1);
            roomScript.hasLoaded = true;
        }
    }

    /// <summary>
    /// 通过坐标获得房间
    /// </summary>
    /// <param name="raw"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public GameObject GetRoomByAxis(int raw,int column)
    {
        if (raw < 0 || raw >= mapWidth)
        {
            return null;
        }
        if (column < 0 || column >= mapHeight)
        {
            return null;
        }
        GameObject room = roomList[column * mapHeight + raw];
        return room;
    }

    //void GenerateDoor()
    //{   
    //    for (int i = 0; i <= mapSize*mapSize-1-mapSize; i++) //generate Vertical door
    //    {
            
    //        var door = Instantiate(Door, new Vector3((i % mapSize) * 1.5f + 0.5f, 0, Mathf.Floor(i / mapSize) * 1.5f + 1.25f), Quaternion.identity);
    //        door.name = "DoorV " + i;
    //        door.transform.parent = this.transform;
            
    //    }
    //    for (int i = 0; i <= mapSize * mapSize - 1 - mapSize; i++)        //generate Horizontal door
    //    {
    //        var door = Instantiate(Door, new Vector3((i % (mapSize - 1)) * 1.5f + 1.25f, 0, Mathf.Floor(i / (mapSize - 1)) * 1.5f + 0.5f), Quaternion.identity);
    //        door.name = "DoorH " + i;
    //        door.transform.Rotate(new Vector3(0, 90f, 0));
    //        door.transform.parent = this.transform;
    //    }
    //}
}
