using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : SingletonMonoBehaviour<DungeonManager>
{

    public Dungeon mDungeon { get; private set; }
    public CharacterContent mCharacter { get; private set; }
    public CharacterInDungeon dungeonCharacter => CharacterInDungeon.Ins;
    public DungeonUI DungeonUI;
    public GenerateMap Generator;
    public GameObject player;
    public int dataChipCount = 0;
    public int score = 0;
    public int currentRoomNum;
    public int Floor = 1;
    public int ChangeColorCost = 100;
    public int ChangeColorCount = 0;
    public const int CostIncrease = 30;
    private static bool enableInput = true;


    public void LoadData(Dungeon dungeon, CharacterContent cc)
    {
        mDungeon = dungeon;
        mCharacter = cc;
        dungeonCharacter.Init(cc);
    }

    private CharacterInDungeon InitDungeonCharacter(CharacterContent character)
    {
        return dungeonCharacter;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        //从场景中获取人物与地图信息
        DungeonStatus ds = SceneStatusManager.Ins.current as DungeonStatus;
        LoadData(ds.Dungeon, ds.cc);
        //读取地图信息
        Generator.LoadDungeonData(mDungeon, mCharacter);
        Generator.makeDictionary();
        Generator.loadMap();
        //读取人物信息
        InitDungeonCharacter(mCharacter);
        //刷新UI界面
        DungeonUI.RefreshUI();
    }

    /// <summary>
    /// 每帧进行鼠标点击判断
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && enableInput)
        {
            //创建一条从摄像机到触摸位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 定义射线
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                print("info   " + rayHit.collider.gameObject.name);
                //判断是否点击的是房间
                if (rayHit.collider.gameObject.tag == "Room")
                {
                    GameObject go = rayHit.collider.gameObject;
                    //判断该房间是否与玩家现在所在房间相邻
                    if (judgeNerabyRoom(go, GetRoomByNumber(currentRoomNum)))
                    {
                        //如果是，则移动到该房间
                        Move(go);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 检查两个房间是否相邻
    /// </summary>
    /// <param name="roomA"></param>
    /// <param name="roomB"></param>
    /// <returns></returns>
    bool judgeNerabyRoom(GameObject roomA, GameObject roomB)
    {
        bool flag = false;
        BattleRoomScript scriptA = roomA.GetComponent<BattleRoomScript>();
        if (scriptA.LeftRoom == roomB) flag = true;
        if (scriptA.RightRoom == roomB) flag = true;
        if (scriptA.UpRoom == roomB) flag = true;
        if (scriptA.DownRoom == roomB) flag = true;
        return flag;
    }

    /// <summary>
    /// 恢复接受鼠标输入
    /// </summary>
    public void Enable()
    {
        enableInput = true;
    }

    /// <summary>
    /// 停止接受鼠标输入
    /// </summary>
    public void Disable()
    {
        enableInput = false;
    }

    void Move(GameObject room)
    {   
        //触发离开事件
        GetRoomByNumber(currentRoomNum).GetComponent<BattleRoomScript>().roomEvent.LeaveEvent();

        var curPos = player.transform.position;

        if (room.transform.position.x < curPos.x) //left
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -90f, 0));
            Disable();
            PlayerMove.Run(room.transform.position, curPos, room);
        }
        if (room.transform.position.x > curPos.x) //right
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
            Disable();
            PlayerMove.Run(room.transform.position, curPos, room);
        }
        if (room.transform.position.z > curPos.z) //up
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            Disable();
            PlayerMove.Run(room.transform.position, curPos, room);
        }
        if (room.transform.position.z < curPos.z) //down
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -180f, 0));
            Disable();
            PlayerMove.Run(room.transform.position, curPos, room);
        }
        //在PlayerMove的Run方法的最后，会触发该房间的进入事件
        DungeonManager.Ins.currentRoomNum = room.GetComponent<BattleRoomScript>().thisRoomNum;
    }

    protected override void UnityAwake()
    {
    }


    /// <summary>
    /// 进入下一层地图
    /// </summary>
    public void GotoNextFloor()
    {
        Generator.clearMap();
        Generator.loadMap();
        Floor++;
        DungeonUI.RefreshUI();
    }

    /// <summary>
    /// 将战斗结果返回
    /// </summary>
    /// <param name="information"></param>
    //TODO
    public void DealWithBattleResult(PassedResultInformation information)
    {

    }

    public void BattleHappen(EnemyGroup eg)
    {
        BattleManager.information = new PassedBattleInformation { enemyGroup = eg };
        SceneStatusManager.Ins.SetSceneStatus(new BattleStatus(SceneStatusManager.Ins), true, true);
    }

    /// <summary>
    /// 通过编号查找房间的GameObject
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public GameObject GetRoomByNumber(int number)
    {
        GameObject go = GameObject.Find("Room " + number);
        if (go)
        {
            return go;
        }
        else
        {
            Debug.Log("没有找到编号为" + number + "的房间");
            return null;
        }
    }


}
