using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : SingletonMonoBehaviour<MainUI>
{
    public GameObject LvCount;
    public GameObject ExpBar;
    public GameObject ExpCount;
    public GameObject TimeCount;
    public GameObject LuntCount;
    public GameObject CoreCount;
    public GameObject CharacterPainting;
    public GameObject DialogueBox;
    private float timer;

    /// <summary>
    /// 出击按钮事件
    /// </summary>
    public void OnCombatBtnClick()
    {
        SceneStatusManager.Ins.SetSceneStatus(new ChooseStatus(SceneStatusManager.Ins));
    }

    /// <summary>
    /// 干员按钮事件
    /// </summary>
    public void OnMemberBtnClick()
    {
        SceneStatusManager.Ins.SetSceneStatus(new CharacterStatus(SceneStatusManager.Ins));
    }

    /// <summary>
    /// 约会按钮事件
    /// </summary>
    public void OnDateBtnClick()
    {

    }

    /// <summary>
    /// 仓库按钮事件
    /// </summary>
    public void OnWarehouseBtnClick()
    {

    }

    /// <summary>
    /// 设置按钮事件
    /// </summary>
    public void OnOptionsBtnClick()
    {

    }

    /// <summary>
    /// 退出按钮事件
    /// </summary>
    public void OnQuitBtnClick()
    {
        Application.Quit();
    }

    /// <summary>
    /// 图鉴按钮事件
    /// </summary>
    public void OnArchiveBtnClick()
    {

    }

    /// <summary>
    /// 立绘被点击时的事件
    /// </summary>
    public void OnCharacterPaintingClick()
    {
        MainStatus ms = SceneStatusManager.Ins.current as MainStatus;
        int i = Random.Range(0, ms.cc.Words.Touch.Length);
        ShowDialogueBox(ms.cc.Words.Touch[i], 5);
    }

    /// <summary>
    /// 刷新当前系统时间
    /// </summary>
    public void RefreshTime()
    {
        TimeCount.GetComponent<Text>().text = GetSystemTime();
    }

    /// <summary>
    /// 刷新玩家的伦特币数量
    /// </summary>
    /// <param name="count"></param>
    public void RefreshLuntCount(int count)
    {
        LuntCount.GetComponent<Text>().text = count + "";
    }

    /// <summary>
    /// 刷新玩家的核心数量
    /// </summary>
    /// <param name="count"></param>
    public void RefreshCoreCount(int count)
    {
        CoreCount.GetComponent<Text>().text = count + "";
    }

    /// <summary>
    /// 刷新玩家的等级信息
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="currentExp"></param>
    public void RefreshLvInfo(int lv,int currentExp)
    {
        string LvText = ""+lv;
        if (lv < 10)
        {
            LvText = "0" + lv;
        }
        LvCount.GetComponent<Text>().text = LvText;
        int expMax = Initializer.Ins.lvInfos[lv - 1].RequireCount;
        ExpBar.GetComponent<Slider>().value = (float)currentExp / expMax;
        ExpCount.GetComponent<Text>().text = "EXP "+currentExp + " / " + expMax;
    }

    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="text">对话框内容</param>
    /// <param name="closeTime">多少时间后对话框消失</param>
    public void ShowDialogueBox(string text,float closeTime)
    {
        DialogueBox.SetActive(true);
        DialogueBox.transform.Find("Dialogue").GetComponent<Text>().text = text;
        StopCoroutine("EnhanceDialogueBox");
        StartCoroutine("EnhanceDialogueBox",5);
    }

    /// <summary>
    /// 隐藏对话框
    /// </summary>
    IEnumerator EnhanceDialogueBox(float closeTime)
    {
        yield return new WaitForSeconds(closeTime);
        DialogueBox.SetActive(false);
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

    protected override void UnityAwake()
    {
    }
}
