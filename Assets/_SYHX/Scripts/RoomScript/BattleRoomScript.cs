using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleRoomScript : MonoBehaviour
{
    private Camera camera;
    public GameObject player;
    public LayerMask layerMask;
    private int roomNum;


    void Start()
    {
        roomNum = System.Convert.ToInt32(string.Join("", this.name.ToCharArray().Where(char.IsDigit)));
        player = GameObject.Find("player");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

            if (judgePos(mousePos) && judgeNearby())
            {
                Move(this.transform.position);
            }
        }
    }

    bool judgeNearby()
    {

        // TODO
        return true;
    }

    bool judgePos(Vector3 pos )
    {
        var roomPos = this.transform.position;
        bool result = false;
        if (pos.x < roomPos.x + 0.5f && pos.x > roomPos.x - 0.5f && pos.z < roomPos.z + 0.5f && pos.z > roomPos.z- 0.5f)
        {
            print(roomNum);
            result = true;
        }
        return result;
    }


    void Move(Vector3 pos)
    {   //temp move, add animation later
        player.transform.position = new Vector3 (pos.x, player.transform.position.y, pos.z);
    }
}
