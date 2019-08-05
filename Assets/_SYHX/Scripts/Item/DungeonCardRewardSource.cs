using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public class DungeonCardRewardSource : ItemSource
{
    // Start is called before the first frame update
    public List<CardSource> CardSourcePool;

    /// <summary>
    /// 返回随机出来的卡牌
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<CardSource> GetCardSourceInPool(int count)
    {
        List<CardSource> list = new List<CardSource>();
        for (int i = 0; i < count; i++)
        {
            CardSource cs = CardSourcePool[Random.Range(0, CardSourcePool.Count - 1)];
            list.Add(cs);
        }
        return list;
    }
}
