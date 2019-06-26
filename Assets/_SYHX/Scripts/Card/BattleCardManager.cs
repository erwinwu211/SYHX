﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SYHX.Cards
{
    public class BattleCardManager : SingletonMonoBehaviour<BattleCardManager>
    {
        //卡牌存储区域
        //牌库
        private List<CardContent> deckPile = new List<CardContent>();
        //手牌
        [ShowInInspector] private List<CardContent> handPile = new List<CardContent>();
        private List<CardContent> exhaustPile = new List<CardContent>();
        //当前使用区
        private List<CardContent> usedPile = new List<CardContent>();
        //弃牌堆
        private List<CardContent> foldPile = new List<CardContent>();
        public GameObject cardPos;
        public GameObject initPos;
        public BattleCardUI cardGo;
        public Vector3 zeroPosition;
        public Vector3 shiftPosition;


        //测试用
        public List<CardSource> cards;
        protected override void UnityAwake(){}
        //
        void Start()
        {

            foreach (var card in cards)
            {
                card.GenerateToDeck();
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
                GoTo(card,CardPosition.Hand);
                count--;
                card.OnDraw();
                card.bUI.transform.SetParent(cardPos.transform, false);
                RefreshUI();
                if (count > 0) { Draw(count); } else { return; }
            }
        }

        public void Discard(CardContent cc)
        {
            GoTo(cc,CardPosition.Discard);
            cc.OnFold();
            cc.bUI.transform.SetParent(initPos.transform);
            cc.bUI.transform.localPosition = Vector3.zero;
            RefreshUI();
        }

        public void DiscardAll()
        {
            var HandPile = handPile.ToArray();
            foreach(var hand in HandPile)
            {
                Discard(hand);
            }
            var UsedPile = usedPile.ToArray();
            foreach(var used in UsedPile)
            {
                Discard(used);
            }
        }
        public void Exhaust(CardContent cc)
        {
            GoTo(cc,CardPosition.Exhaust);
            cc.bUI.transform.SetParent(initPos.transform);
            cc.bUI.transform.localPosition = Vector3.zero;
            RefreshUI();
        }

        public void Used(CardContent cc)
        {
            GoTo(cc,CardPosition.Used);
            cc.bUI.transform.SetParent(initPos.transform);
            cc.bUI.transform.localPosition = Vector3.zero;
            RefreshUI();
        }
        public void ReturnToDeck(CardContent cc)
        {
            GoTo(cc,CardPosition.Deck);
            RefreshUI();
        }
        public void RefreshUI()
        {
            var index = 0;
            var count = handPile.Count;
            var zero = -count * zeroPosition;
            foreach (var cc in handPile)
            {
                index++;
                cc.bUI.transform.localPosition = index * shiftPosition + zero;
            }
        }


        #region 卡牌去向
        private void GoTo(CardContent cc,CardPosition position)
        {
            GetCurrentList(cc).Remove(cc);
            cc.bUI.position = position;
            switch(position)
            {
                case CardPosition.Deck:
                deckPile.Add(cc);
                break;
                case CardPosition.Discard:
                foldPile.Add(cc);
                break;
                case CardPosition.Exhaust:
                exhaustPile.Add(cc);
                break;
                case CardPosition.Hand:
                handPile.Add(cc);
                break;
                case CardPosition.Used:
                usedPile.Add(cc);
                break;
            }
        }
        #endregion

        private List<CardContent> GetCurrentList(CardContent cc)
        {
            switch (cc.bUI.position)
            {
                case CardPosition.Hand:
                    return handPile;
                case CardPosition.Deck:
                    return deckPile;
                case CardPosition.Discard:
                    return foldPile;
                case CardPosition.Used:
                    return usedPile;
                case CardPosition.Exhaust:
                    return exhaustPile;
                default:
                    return deckPile;
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
                GoTo(card,CardPosition.Deck);
            }
            //清空弃牌堆
            foldPile.Clear();
        }

        public void GenerateCardUI(CardContent cc)
        {
            var go = GameObject.Instantiate(cardGo, initPos.transform.position, initPos.transform.rotation, initPos.transform);
            go.SetCard(cc);
            cc.bUI = go;
        }

        public void AddToDeck(CardContent cc) => deckPile.Add(cc);
    }

}
