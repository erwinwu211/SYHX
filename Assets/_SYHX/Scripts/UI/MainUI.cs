using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameObject LvCount;
    public GameObject ExpBar;
    public GameObject ExpCount;
    public GameObject TimeCount;
    public GameObject LuntCount;
    public GameObject CoreCount;
    public GameObject CharacterPainting;
    public GameObject DialogueBox;

    public void OnCombatBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new ChooseState(SceneStateManager.Ins));
    }

    public void OnMemberBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new CharacterState(SceneStateManager.Ins));
    }

    public void OnDateBtnClick()
    {

    }

    public void OnWarehouseBtnClick()
    {

    }

    public void OnOptionsBtnClick()
    {

    }

    public void OnQuitBtnClick()
    {

    }

    public void OnArchiveBtnClick()
    {

    }


    public void Update()
    {
        RefreshTime();
    }

    /// <summary>
    /// 刷新当前系统时间
    /// </summary>
    private void RefreshTime()
    {
        TimeCount.GetComponent<Text>().text = GetSystemTime();
    }


    /// <summary>
    /// 处理获取到的时间的格式
    /// </summary>
    /// <returns></returns>
    private string GetSystemTime()
    {
        string time;
        string year = System.DateTime.Now.Year + "";
        string month = System.DateTime.Now.Month + "";
        string day = System.DateTime.Now.Day + "";
        string hour;
        string minute;
        string AMorPM;
        if (System.DateTime.Now.Hour >= 12)
        {
            hour = (System.DateTime.Now.Hour - 12) +"";
            AMorPM = "PM";
        }
        else
        {
            hour = System.DateTime.Now.Hour + "";
            AMorPM = "AM";
        }
        if (System.DateTime.Now.Minute < 10)
        {
            minute = "0"+System.DateTime.Now.Minute;
        }
        else
        {
            minute = System.DateTime.Now.Minute + "";
        }
        time = year + "." + month + "." + day + " " + hour + ":" + minute + AMorPM;
        return time;
    }
}
