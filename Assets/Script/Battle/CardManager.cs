using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingletonMonoBehaviour<CardManager>
{
    //卡牌存储区域
    //牌库
    private List<CardContent> deckPile;
    //手牌
    private List<CardContent> handPile;
    //当前使用区
    private List<CardContent> usedPile;
    //弃牌堆
    private List<CardContent> foldPile;
    public GameObject cardPos;
    public GameObject cardGo;
    protected override void UnityAwake()
    {

    }
    /// <summary>
    /// 抽牌方法
    /// </summary>
    /// <param name="count">抽牌的张数</param>
    public void Draw(int count)
    {
        if (deckPile.Count == 0)
        {
            if (foldPile.Count > 0)
            {
                Shuffle();
                Draw(count);
                return;
            }
            else
            {
                return;
            }
        }
        else
        {
            CardContent card = deckPile[0];
            deckPile.Remove(card);
            handPile.Add(card);
            count--;
            card.OnDraw();

            //TODO : 动画改进
            var go = cardGo.Create(cardPos);
            // go.transform.

            if (count > 0) { Draw(count); } else { return; }
        }
    }




    /// <summary>
    /// 洗牌方法
    /// </summary>
    private void Shuffle()
    {
        List<CardContent> temp = new List<CardContent>();
        //先将弃牌堆打乱
        foreach (CardContent card in foldPile)
        {
            temp.Insert(Random.Range(0, temp.Count), card);
        }
        //将所有弃牌放入牌堆中
        foreach (CardContent card in temp)
        {
            deckPile.Add(card);
        }
        //清空弃牌堆
        foldPile.Clear();
    }
}
