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
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100f,layerMask) && MoveJudge(hit.transform))
            {
                Move(hit.transform.position );
            }
        }
    }

    bool MoveJudge(Transform tar)
    {   bool result = false;
        if (tar.name == this.name)
        {
            print(roomNum);
            result = true;
        }
        return result; 
    }


    void Move(Vector3 pos)
    {   //temp move, add animation later
        player.transform.position = pos;
    }
}
