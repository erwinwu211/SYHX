using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingletonMonoBehaviour<CardManager>
{
    //卡牌存储区域
    //牌库
    private List<CardContent> deckPile = new List<CardContent>();
    //手牌
    private List<CardContent> handPile = new List<CardContent>();
    //当前使用区
    private List<CardContent> usedPile = new List<CardContent>();
    //弃牌堆
    private List<CardContent> foldPile = new List<CardContent>();
    public GameObject cardPos;
    public BattleCardUI cardGo;


    //测试用
    public List<CardSource> cards;
    //
    protected override void UnityAwake()
    {
        foreach (var card in cards)
        {
            card.GenerateCard(1);
        }
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
            var go = GameObject.Instantiate(cardGo, cardPos.transform.position, cardPos.transform.rotation, cardPos.transform);
            // var go = cardGo.Create(cardPos);
            go.transform.localPosition -= (handPile.Count - 1) * new Vector3(150f, 0f, 0f);
            go.SetCard(card);

            if (count > 0) { Draw(count); } else { return; }
        }
    }




    /// <summary>
    /// 洗牌方法
    /// </summary>
    public void Shuffle()
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

    public void AddToDeck(CardContent cc)
    {
        deckPile.Add(cc);
    }
}
