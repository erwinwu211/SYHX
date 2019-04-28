using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    private bool isAlive;
    private int maxHp;
    private int correctHp;
    private int attack;
    private int AI_id;

    public Enemy (int id)
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {
        isAlive = false;
    }

    /// <summary>
    /// 回血
    /// </summary>
    public void IncreaseHp(int count)
    {
        correctHp += count;
        if (correctHp > maxHp)
        {
            correctHp = maxHp;
        }
    }

    /// <summary>
    /// 掉血
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHp(int count)
    {
        correctHp -= count;
        if (correctHp <= 0)
        {
            correctHp = 0;
            Death();
        }
    }

    /// <summary>
    /// 返回是否存活
    /// </summary>
    /// <returns></returns>
    public bool IsAlive()
    {
        return isAlive;
    }

}
