using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero {

    protected int maxLv;
    protected int lv;

    //原型属性值
    protected int protoHp;
    protected int protoEp;
    protected int protoATK;
    protected int protoDEF;

    //成长属性值
    protected int growHp;
    protected int growATK;
    protected int growDEF;


    //战斗属性值
    protected int battleCorrectHp;
    protected int battleCorrectEp;

    //地宫加成属性值
    protected int dgMaxHpAddition;
    protected int dgCorrectHp;
    protected int dgEpAddition;
    protected int dgATKAddition;
    protected int dgDEFAddition;



    /// <summary>
    /// 创建一个新角色
    /// </summary>
    public Hero()
    {
        lv = 1;
        protoHp = 100;
        protoEp = 5;
        protoATK = 10;
        protoDEF = 10;
    }

    /// <summary>
    /// 通过存档读取一个角色
    /// </summary>
    /// <param name="id">存档编号</param>
    public Hero(int id)
    {

    }

    /// <summary>
    /// 升级,提升原始属性值
    /// </summary>
    public void LevelUp()
    {
        if (lv < maxLv)
        {
            lv++;
            protoHp += growHp;
            protoATK += growATK;
            protoDEF += growDEF;
        }
    }

    /// <summary>
    /// 返回当前等级数值
    /// </summary>
    /// <returns></returns>
    public int Lv()
    {
        return lv;
    }

    /// <summary>
    /// 返回原始攻击力数值
    /// </summary>
    /// <returns></returns>
    public int ProtoATK()
    {
        return protoATK;
    }

    /// <summary>
    /// 提升原始攻击力数值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoATKUp(int count)
    {
        protoATK += count;
    }

    /// <summary>
    /// 减少原始攻击力数值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoATKDown(int count)
    {
        protoATK -= count;
    }

    /// <summary>
    /// 返回原始防御力数值
    /// </summary>
    /// <returns></returns>
    public int ProtoDEF()
    {
        return protoDEF;
    }

    /// <summary>
    /// 提升原始防御力数值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoDEFUp(int count)
    {
        protoDEF += count;
    }

    /// <summary>
    /// 减少原始防御力数值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoDEFDown(int count)
    {
        protoDEF -= count;
    }

    /// <summary>
    /// 返回原始最大生命值
    /// </summary>
    /// <returns></returns>
    public int ProtoHp()
    {
        return protoHp;
    }

    /// <summary>
    /// 提升原始最大生命值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoHpUp(int count)
    {
        protoHp += count;
    }

    /// <summary>
    /// 减少原始最大生命值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoHpDown(int count)
    {
        protoHp -= count;
    }

    /// <summary>
    /// 返回原始最大能量值
    /// </summary>
    /// <returns></returns>
    public int ProtoEp()
    {
        return protoEp;
    }

    /// <summary>
    /// 提升原始最大能量值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoEpUp(int count)
    {
        protoEp += count;
    }

    /// <summary>
    /// 减少原始最大能量值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoEpDown(int count)
    {
        protoEp -= count;
    }

    /// <summary>
    /// 改写原始最大能量值
    /// </summary>
    /// <param name="count"></param>
    public void ProtoEpOverwrite(int count)
    {
        protoEp = count;
    }

    /// <summary>
    /// 返回地宫攻击力数值
    /// </summary>
    /// <returns></returns>
    public int DgATK()
    {
        return protoATK+dgATKAddition;
    }

    /// <summary>
    /// 改变地宫攻击力加成的数值
    /// </summary>
    /// <param name="count">加成值</param>
    public void DgATKAddition(int count)
    {
        dgATKAddition += count;
    }

    /// <summary>
    /// 返回地宫防御力数值
    /// </summary>
    /// <returns></returns>
    public int DgDEF()
    {
        return protoDEF+dgDEFAddition;
    }

    /// <summary>
    /// 改变地宫防御力数值
    /// </summary>
    /// <param name="count"></param>
    public void DgDEFAddition(int count)
    {
        dgDEFAddition += count;
    }

    /// <summary>
    /// 返回地宫最大生命值
    /// </summary>
    /// <returns></returns>
    public int DgMaxHp()
    {
        return protoHp + dgMaxHpAddition;
    }

    /// <summary>
    /// 改变地宫最大生命值
    /// </summary>
    /// <param name="count"></param>
    public void DgMaxHpAddition(int count)
    {
        dgMaxHpAddition += count;
    }

    /// <summary>
    /// 返回地宫最大能量值
    /// </summary>
    /// <returns></returns>
    public int DgMaxEp()
    {
        return protoEp+dgEpAddition;
    }

    /// <summary>
    /// 改变地宫最大能量值
    /// </summary>
    /// <param name="count"></param>
    public void DgEp(int count)
    {
        protoEp += count;
    }

    /// <summary>
    /// 返回战斗时的当前生命值
    /// </summary>
    /// <returns></returns>
    public int GetCorrectHp()
    {
        return battleCorrectHp;
    }
}
 

