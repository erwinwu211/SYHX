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
    private int thisRoomNum;
    private int thisRoomType;
    private static bool enableInput = true;
    public RoomEvent roomEvent;
    void Start()
    {
        thisRoomNum = System.Convert.ToInt32(string.Join("", this.name.ToCharArray().Where(char.IsDigit)));
        player = GameObject.Find("player");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonDown(0) && enableInput)
        { // if left button pressed...
            var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

            if (judgePos(mousePos) && judgeNearby())
            {
                Move(this.transform.position);
            }
        }
    }
    public void changeType(int type)
    {
        thisRoomType = type;
    }

    public void MoveOnEvent()
    {
        print(this.name +" Event happened!");
        roomEvent = this.GetComponentInParent<RoomEvent>();
        roomEvent.Event(thisRoomType);
        //TODO by SONGLEI
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

    public static void Enable()
    {
        enableInput = true;
    }

    public static void Disable()
    {
        enableInput = false;
    }


    void Move(Vector3 pos)
    {   //temp move, add animation later
        var curPos = player.transform.position;
        
        if (pos.x < curPos.x) //left
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0,-90f,0));
            Disable();
            PlayerMove.Run(pos, curPos, this.gameObject);
        }
        if (pos.x > curPos.x) //right
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
            Disable();
            PlayerMove.Run(pos, curPos, this.gameObject);
        }
        if (pos.z > curPos.z) //up
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            Disable();
            PlayerMove.Run(pos, curPos, this.gameObject);
        }
        if (pos.z < curPos.z) //down
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -180f, 0));
            Disable();
            PlayerMove.Run(pos, curPos, this.gameObject);
        }
        //player.transform.position = new Vector3 (pos.x, player.transform.position.y, pos.z);
        currentRoomNum = thisRoomNum;
    }
}
