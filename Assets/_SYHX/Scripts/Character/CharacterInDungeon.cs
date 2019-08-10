using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;
using System.Linq;

public enum BasicAttributeType
{
    Force, Aglie, Constitution, Fortune
}

public class BasicAttribute
{
    public BasicAttributeType type;
    public int currentLv;
    public int maxLv = 5;
    public int currentExp;
    public int maxExp => Initializer.Ins.AttrLvInfos[currentLv - 1].RequireCount;
    public bool IsMaxLv() => currentLv >= maxLv;

    public void GainExp(int count)
    {
        if (currentLv >= maxLv) return;
        currentExp += count;
        Debug.Log("得到了"+count+type.ToString()+"经验");
        if (currentExp >= maxExp) LvUp();
    }

    public void LvUp()
    {
        if (currentLv >= maxLv) return;
        currentExp -= maxExp;
        currentLv++;
        Debug.Log(type.ToString()+"升至了"+currentLv+"级");
        DungeonManager.Ins.RefreshUI();
    }


    public BasicAttribute(BasicAttributeType type, int initLv)
    {
        this.type = type;
        this.currentLv = initLv;
        this.currentExp = 0;
    }
}

/// <summary>
/// 当玩家进入地宫时，根据所选择的人物及其设定所生成的对象
/// 用于记录玩家在地宫中的信息
/// </summary>
public class CharacterInDungeon : SingletonMonoBehaviour<CharacterInDungeon>
{
    public CharacterContent character;
    public int maxLv { get; private set; }
    public int currentLv { get; set; }
    public int maxExp => character.lvInfos[currentLv - 1].RequireCount;
    public int currentExp;
    public int maxHp { get; set; }
    public int currentHp { get; set; }
    public int Attack { get; private set; }
    public int Defend { get; private set; }
    public int maxEp { get; private set; }
    public int Draw_count { get; private set; }
    public BasicAttribute Force { get; private set; }
    public BasicAttribute Agile { get; private set; }
    public BasicAttribute Constitution { get; private set; }
    public BasicAttribute Fortune { get; private set; }
    public List<CardContent> Deck { get; private set; }
    public List<TalentGroup> talentGroups { get; private set; }
    public List<Talent> activeTalents{get;private set;}
    public GameObject LevelUpPanel;

    public void Start()
    {
        //这里先暂时用一下预设好的人物
        // Umirika umirika = new Umirika();
        // Init(umirika);
    }

    public void Init(CharacterContent cc)
    {
        character = cc;
        this.maxHp = cc.Hp_max;
        this.currentHp = cc.Hp_max;
        this.Attack = cc.Attack;
        this.Defend = cc.Defend;
        this.maxEp = cc.Energy_max;
        this.Draw_count = cc.Draw_count;
        this.Force = new BasicAttribute(BasicAttributeType.Force, cc.Force);
        this.Agile = new BasicAttribute(BasicAttributeType.Aglie, cc.Agile);
        this.Constitution = new BasicAttribute(BasicAttributeType.Constitution, cc.Constitution);
        this.Fortune = new BasicAttribute(BasicAttributeType.Fortune, cc.Fortune);
        this.Deck = new List<CardContent>();
        this.maxLv = cc.currentGrade.LvMax;
        this.currentLv = 1;
        this.activeTalents = new List<Talent>();
        InitTalent();
        GainExp(cc.InitDungeonExp);
        foreach (var cs in cc.Deck)
        {
            this.Deck.Add(cs.GenerateCard());
        }
    }

    #region 人物等级相关
    /// <summary>
    /// 获得经验值
    /// </summary>
    /// <param name="count"></param>
    public void GainExp(int count)
    {
        currentExp += count;
        Debug.Log("获得了" + count + "经验");
        switch (CheckCanLevelUp())
        {
            case LvUpCheck.yes:
                DungeonLvInfo prevInfo = new DungeonLvInfo()
                {
                    LvName = this.currentLv + "",
                    HpReward = this.maxHp,
                    AttackReward = this.Attack,
                    DefendReward = this.Defend,
                    EPReward = this.maxEp,
                    DrawCount = this.Draw_count,
                };
                LevelUp();
                DungeonManager.Ins.RefreshUI();
                DungeonLvInfo afterInfo = new DungeonLvInfo()
                {
                    LvName = this.currentLv + "",
                    HpReward = this.maxHp,
                    AttackReward = this.Attack,
                    DefendReward = this.Defend,
                    EPReward = this.maxEp,
                    DrawCount = this.Draw_count,
                };
                Transform parent = GameObject.Find("Canvas").transform;
                GameObject go = Instantiate(LevelUpPanel, parent);
                go.transform.SetParent(parent);
                go.GetComponent<LevelUpInfoUI>().ShowLevelUpInfoUI(prevInfo, afterInfo);
                break;
            case LvUpCheck.max:
                return;
            case LvUpCheck.cost_unenough:
                return;
        }
    }

    public LvUpCheck CheckCanLevelUp()
    {
        Debug.Log("当前人物等级为" + currentLv + " 升级所需经验*" + maxExp);
        if (currentLv >= maxLv) return LvUpCheck.max;
        if (currentExp < maxExp) return LvUpCheck.cost_unenough;
        return LvUpCheck.yes;
    }

    public void LevelUp()
    {
        if (currentLv >= maxLv) return;
        DungeonLvInfo _info = character.lvInfos[currentLv - 1];
        if (_info.HpReward != 0)
        {
            this.IncreaseHpMaxWithCurrentHp(_info.HpReward);
        }
        if (_info.AttackReward != 0)
        {
            this.IncreaseAttack(_info.AttackReward);
        }
        if (_info.DefendReward != 0)
        {
            this.IncreaseDefend(_info.DefendReward);
        }
        if (_info.EPReward != 0)
        {
            this.maxEp += _info.EPReward;
        }
        if (_info.DrawCount != 0)
        {
            this.Draw_count += _info.DrawCount;
        }
        currentExp -= maxExp;
        this.currentLv++;
        Debug.Log("升级成功，现在是" + currentLv + "级， 还剩下" + currentExp + "经验值");
        if (CheckCanLevelUp() == LvUpCheck.yes) LevelUp();
        DungeonManager.Ins.RefreshUI();
    }
    #endregion

    #region 属性的增加减少
    /// <summary>
    /// 增加生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpMax(int count)
    {
        if (count < 0) return;
        maxHp += count;
    }

    /// <summary>
    /// 增加生命最大值的同时增加等量当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpMaxWithCurrentHp(int count)
    {
        if (count < 0) return;
        maxHp += count;
        currentHp += count;
    }

    /// <summary>
    /// 减少生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpMax(int count)
    {
        if (count < 0) return;
        maxHp -= count;
        if (maxHp < currentHp)
        {
            currentHp = maxHp;
        }
        if (maxHp <= 0)
        {
            //TODO:SendEvent("DungeonEnd");
        }
    }

    /// <summary>
    /// 增加当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpCurrect(int count)
    {
        if (count < 0) return;
        currentHp += count;
        if (currentHp > maxHp) currentHp = maxHp;
    }

    /// <summary>
    /// 削减当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpCurrect(int count)
    {
        if (count < 0) return;
        currentHp -= count;
        if (currentHp <= 0)
        {
            //TODO:SendEvent("DungeonEnd");
        }
    }

    /// <summary>
    /// 增加攻击力
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseAttack(int count)
    {
        if (count < 0) return;
        Attack += count;
    }

    /// <summary>
    /// 减少攻击力
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseAttack(int count)
    {
        if (count < 0) return;
        Attack -= count;
    }

    /// <summary>
    /// 增加防御力
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseDefend(int count)
    {
        if (count < 0) return;
        Defend += count;
    }

    /// <summary>
    /// 减少防御力
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseDefend(int count)
    {
        if (count < 0) return;
        Defend -= count;
    }

    /// <summary>
    /// 获得基础属性的经验
    /// </summary>
    /// <param name="attr"></param>
    /// <param name="count"></param>
    public void IncreaseBasicAttributeExp(BasicAttributeType attr, int count)
    {
        switch (attr)
        {
            case BasicAttributeType.Force:
                this.Force.GainExp(count);
                break;
            case BasicAttributeType.Aglie:
                this.Agile.GainExp(count);
                break;
            case BasicAttributeType.Constitution:
                this.Constitution.GainExp(count);
                break;
            case BasicAttributeType.Fortune:
                this.Fortune.GainExp(count);
                break;
        }
    }

    #endregion

    #region 卡牌的操作
    /// <summary>
    /// 移除一张卡牌
    /// </summary>
    /// <param name="card"></param>
    public void RemoveCard(CardContent card)
    {
        if (Deck.Contains(card))
        {
            Deck.Remove(card);
        }
    }

    /// <summary>
    /// 加入一张卡牌
    /// </summary>
    /// <param name="card"></param>
    public void JoinCard(CardContent card)
    {
        Deck.Add(card);
    }

    /// <summary>
    /// 替换一张卡牌
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="now"></param>
    public void ChangeCard(CardContent origin, CardContent now)
    {
        Deck.Remove(origin);
        Deck.Add(now);
    }
    #endregion

    #region 天赋相关
    /// <summary>
    /// 重置每隔天赋组的选中状态
    /// </summary>
    public void InitTalent()
    {
        this.talentGroups = character.TalentGroups;
        foreach (TalentGroup t in character.TalentGroups)
        {
            t.ActiveTalent = null;
        }
    }

    /// <summary>
    /// 激活一个天赋
    /// </summary>
    /// <param name="t"></param>
    public void ActiveTalent(Talent t)
    {
        t.OnActive();
        t.Owner.ActiveTalent = t;
        activeTalents.Add(t);
        DungeonManager.Ins.TalentPanelUI.Refresh(talentGroups);
    }
    #endregion

    protected override void UnityAwake()
    {
    }
}

