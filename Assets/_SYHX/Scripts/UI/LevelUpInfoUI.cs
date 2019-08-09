using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpInfoUI : MonoBehaviour
{
    public Text prevLvCount;
    public Text afterLvCount;
    public Transform Hp;
    public Transform Attack;
    public Transform Defend;
    public Transform Energy;
    public Transform DrawCount;

    /// <summary>
    /// 根据升级前的等级来显示面板
    /// 用两个dungeonLvInfo来暂时作为数据类充数，正好需求的变量是相同的，就是名字有点怪。。。
    /// </summary>
    /// <param name="prevLv">升级前的等级</param>
    public void ShowLevelUpInfoUI(DungeonLvInfo prevLv, DungeonLvInfo afterLv)
    {
        gameObject.SetActive(true);
        prevLvCount.text = "Lv." + prevLv.LvName;
        afterLvCount.text = "Lv." + afterLv.LvName;
        Hp.Find("PrevCount").GetComponent<Text>().text = prevLv.HpReward + "";
        Hp.Find("AfterCount").GetComponent<Text>().text = afterLv.HpReward + "";
        Hp.Find("RewardCount").GetComponent<Text>().text = "(+"+(afterLv.HpReward - prevLv.HpReward) + ")";

        Attack.Find("PrevCount").GetComponent<Text>().text = prevLv.AttackReward + "";
        Attack.Find("AfterCount").GetComponent<Text>().text = afterLv.AttackReward + "";
        Attack.Find("RewardCount").GetComponent<Text>().text = "(+"+(afterLv.AttackReward - prevLv.AttackReward) + ")";

        Defend.Find("PrevCount").GetComponent<Text>().text = prevLv.DefendReward + "";
        Defend.Find("AfterCount").GetComponent<Text>().text = afterLv.DefendReward + "";
        Defend.Find("RewardCount").GetComponent<Text>().text = "(+"+(afterLv.DefendReward - prevLv.DefendReward) + ")";

        Energy.Find("PrevCount").GetComponent<Text>().text = prevLv.EPReward + "";
        Energy.Find("AfterCount").GetComponent<Text>().text = afterLv.EPReward + "";
        Energy.Find("RewardCount").GetComponent<Text>().text = "(+"+(afterLv.EPReward - prevLv.EPReward) + ")";

        DrawCount.Find("PrevCount").GetComponent<Text>().text = prevLv.DrawCount + "";
        DrawCount.Find("AfterCount").GetComponent<Text>().text = afterLv.DrawCount + "";
        DrawCount.Find("RewardCount").GetComponent<Text>().text = "(+"+(afterLv.DrawCount - prevLv.DrawCount) + ")";
    }

    public void OnCloseBtnClick()
    {
        Destroy(this.gameObject);
    }
}
