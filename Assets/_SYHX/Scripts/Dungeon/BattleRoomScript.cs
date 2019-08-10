using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleRoomScript : MonoBehaviour
{
    private Camera camera;
    public GameObject player;
    public LayerMask layerMask;

    public static int currentRoomNum;
    public int thisRoomNum;
    public int thisRoomType;
    public int thisRoomRaw;
    public int thisRoomColumn;

    public GameObject RightRoom;
    public GameObject LeftRoom;
    public GameObject UpRoom;
    public GameObject DownRoom;
    public bool hasLoaded = false;

    private static bool enableInput = true;
    public RoomEvent roomEvent;

    public RoomType roomType;
    void Start()
    {
        thisRoomNum = System.Convert.ToInt32(string.Join("", this.name.ToCharArray().Where(char.IsDigit)));
        player = GameObject.Find("player");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    /// <summary>
    /// 检测鼠标是否点击到了挂有本脚本的物体
    /// 并判断该房间是否与主角在的房间相邻
    /// 如果是，则进行移动
    /// </summary>
    void Update()
    {
    }
    public void changeType(int type)
    {
        thisRoomType = type;
    }

    public void MoveOnEvent()
    {
        print(this.name + " Event happened!");
        DetectNearby();
        roomEvent.EnterEvent();
    }

    /// <summary>
    /// 探测该格子周围的房间
    /// </summary>
    public void DetectNearby()
    {
        if (LeftRoom) LeftRoom.GetComponent<BattleRoomScript>().roomEvent.OnDetected();
        if (RightRoom) RightRoom.GetComponent<BattleRoomScript>().roomEvent.OnDetected();
        if (UpRoom) UpRoom.GetComponent<BattleRoomScript>().roomEvent.OnDetected();
        if (DownRoom) DownRoom.GetComponent<BattleRoomScript>().roomEvent.OnDetected();
    }

    public void DetectRoomByAxis(int raw,int column)
    {

    }

    bool judgeNearby()
    {
        Vector3 targetPos = this.transform.position;
        Vector3 currentPos = GameObject.Find("Room " + currentRoomNum).transform.position;
        if (Vector3.Distance(targetPos,currentPos) <= 1.5f)
        {
            return true;
        }
        return false;
    }

    bool judgePos(Vector3 pos )
    {
        var roomPos = this.transform.position;
        bool result = false;
        if (pos.x < roomPos.x + 0.5f && pos.x > roomPos.x - 0.5f && pos.z < roomPos.z + 0.5f && pos.z > roomPos.z- 0.5f)
        {
            
            result = true;
        }
        return result;
    }

    bool judgeNerabyRoom(GameObject roomA,GameObject roomB)
    {
        bool flag = false;
        BattleRoomScript scriptA = roomA.GetComponent<BattleRoomScript>();
        if (scriptA.LeftRoom == roomB) flag = true;
        if (scriptA.RightRoom == roomB) flag = true;
        if (scriptA.UpRoom == roomB) flag = true;
        if (scriptA.DownRoom == roomB) flag = true;
        Debug.Log("flag is" + flag);
        return flag;
    }

}

public enum RoomType
{
    battle_normal,
    battle_elite,
    battle_boss,
    random_event,
    rest,
    craft,
    next_floor,
    no_happen,
}
