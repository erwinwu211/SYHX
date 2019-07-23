using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private static bool run = false;
    private float time = 0f;
    private static Animator animator;
    private static Vector3 tarPos;
    private float timeToReachTarget=1f;
    private static Vector3 curPos;
    public BattleRoomScript roomScript;
    private static GenerateMap mapGenerator;
    private static GameObject room;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (run == true && time < 1f)
        {
            time += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(curPos, tarPos, time);
            if (time >= 1f)
            {
                run = false;
                time = 0f;
                animator.SetBool("Run", false);
                BattleRoomScript.Enable();
                room.GetComponent<BattleRoomScript>().MoveOnEvent();
            }
        }
    }

    //Start running
    public static void Run(Vector3 Pos, Vector3 cPos, GameObject r)
    {
        animator.SetBool("Run", true);
        run = true;
        curPos = cPos;
        tarPos = Pos;
        room = r;
    }


}
