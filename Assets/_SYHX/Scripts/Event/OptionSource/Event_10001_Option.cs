using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_10001_Option : OptionSource
{
    public int count;
    public override void Effect()
    {
        int f = CharacterInDungeon.Ins.Fortune.Get_xDy(2, 0);
        string result = "";
        if (f > 9)
        {
            CharacterInDungeon.Ins.IncreaseEp(1 * count);
            result += "喝下之后一股强大的力量涌现，你感觉自己已经无所不能了，你的EP最大值提升" + count;
        }
        else if (f == 9)
        {
            CharacterInDungeon.Ins.IncreaseAttack(3 * count);
            result += "喝下之后明显感觉力量变得更强了，攻击力提高" + 3 * count;
        }
        else if (f == 8)
        {
            CharacterInDungeon.Ins.IncreaseDefend(3 * count);
            result += "喝下之后明显感觉自己更强壮了，防御力提高了" + 3 * count;
        }
        else if (f == 7)
        {
            CharacterInDungeon.Ins.IncreaseHpMax(10 * count);
            result += "喝下之后体力更胜以往，生命最大值提高了" + 10 * count;
        }
        else if (f == 6)
        {
            CharacterInDungeon.Ins.IncreaseAttack(1 * count);
            result += "喝下之后似乎感觉力量变得更强了，攻击力提高了" + count;
        }
        else if (f == 5)
        {
            CharacterInDungeon.Ins.IncreaseDefend(1 * count);
            result += "喝下之后似乎感觉更强壮了，防御力提高了" + count;
        }
        else if (f == 4)
        {
            int hp = Mathf.FloorToInt(0.1f * CharacterInDungeon.Ins.currentHp * count);
            CharacterInDungeon.Ins.IncreaseHpCurrect(hp);
            result += "喝下之后体力稍有恢复，生命值恢复了" + hp;
        }
        else if (f == 3)
        {
            result += "喝下之后什么感觉都没有" + count;
        }
        else if (f == 2)
        {
            int hp = Mathf.FloorToInt(0.1f * CharacterInDungeon.Ins.currentHp * count);
            CharacterInDungeon.Ins.DecreaseHpCurrect(hp);
            result += "喝下之后感觉头晕目眩，生命值降低了" + hp;
        }
        else if (f == 1)
        {
            CharacterInDungeon.Ins.DecreaseHpMax(5 * count);
            result += "喝下之后感觉一阵腹痛，最大生命值降低了" + 5 * count;
        }
        else if (f == 0)
        {
            CharacterInDungeon.Ins.DecreaseAttack(1 * count);
            result += "喝下之后顿时感觉虚弱无力，攻击力降低了" + count;
        }
        EventManager.Ins.EUI.ShowResultPanel(result);
    }
}
