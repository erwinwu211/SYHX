using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject battleCamera;
    private BattleManager battleManager;

    // Use this for initialization
    void Start () {
        mainCamera = GameObject.Find("Main Camera");
        battleCamera = GameObject.Find("Battle Camera");
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        mainCamera.SetActive(true);
        battleCamera.SetActive(false);
        Debug.Log("2019.3.22 CardKun:按“A”键进入战斗界面，按“S”键回到主界面");
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            BattleStart();
        }
        if (Input.GetKey(KeyCode.S))
        {
            BattleEnd();
        }
    }

    /// <summary>
    /// 战斗开始的入口，切换摄像机画面
    /// </summary>
    public void BattleStart()
    {
        mainCamera.SetActive(false);
        battleCamera.SetActive(true);
        Hero hero = new Hero();
        battleManager.BattleStart(0,this);
    }


    /// <summary>
    /// 战斗结束后的处理，切回摄像机画面
    /// </summary>
    public void BattleEnd()
    {
        mainCamera.SetActive(true);
        battleCamera.SetActive(false);
        battleManager.BattleEnd();
    }

}
