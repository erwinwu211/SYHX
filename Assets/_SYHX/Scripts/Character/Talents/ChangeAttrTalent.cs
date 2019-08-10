using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttrTalent : Talent
{
    // Start is called before the first frame update
    public List<AttrAndCount> IncreaseAttrlist;
    public List<AttrAndCount> DecreaseAttrlist;
    public override void DoEffect()
    {
        foreach (var item in IncreaseAttrlist)
        {
            switch (item.type)
            {
                
                case AttributeType.Attack:
                    CharacterInDungeon.Ins.IncreaseAttack(item.count);
                    break;
                case AttributeType.Defend:
                    CharacterInDungeon.Ins.IncreaseDefend(item.count);
                    break;
                case AttributeType.EP:
                    CharacterInDungeon.Ins.IncreaseEp(item.count);
                    break;
                case AttributeType.MaxHp:
                    CharacterInDungeon.Ins.IncreaseHpMax(item.count);
                    break;
                case AttributeType.DrawCount:
                    CharacterInDungeon.Ins.IncreaseDrawCount(item.count);
                    break;
            }
        }
        foreach (var item in DecreaseAttrlist)
        {
            switch (item.type)
            {
                case AttributeType.Attack:
                    CharacterInDungeon.Ins.DecreaseAttack(item.count);
                    break;
                case AttributeType.Defend:
                    CharacterInDungeon.Ins.DecreaseDefend(item.count);
                    break;
                case AttributeType.EP:
                    CharacterInDungeon.Ins.DecreaseEp(item.count);
                    break;
                case AttributeType.MaxHp:
                    CharacterInDungeon.Ins.DecreaseHpMax(item.count);
                    break;
                case AttributeType.DrawCount:
                    CharacterInDungeon.Ins.DecreaseDrawCount(item.count);
                    break;
            }
        }
    }
}


