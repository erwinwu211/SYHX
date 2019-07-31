using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace SYHX.Cards
{
    public class BattleCardManager : SingletonMonoBehaviour<BattleCardManager>
    {
        #region 卡牌区域
        //卡牌存储区域
        //牌库
        private List<CardContent> deckPile = new List<CardContent>();
        public GameObject deckPos;
        //手牌
        [ShowInInspector] private List<CardContent> handPile = new List<CardContent>();
        public GameObject handPos;

        private List<CardContent> exhaustPile = new List<CardContent>();
        public GameObject exhaustPos;

        //当前使用区
        private List<CardContent> usedPile = new List<CardContent>();
        public GameObject usedPos;
        //弃牌堆
        private List<CardContent> discardPile = new List<CardContent>();
        public GameObject discardPos;

        //临时使用牌区
        private List<CardContent> tempPile = new List<CardContent>();
        #endregion

        public BattleCardUI cardGo;
        public Vector3 zeroPosition;
        public Vector3 shiftPosition;

        public float duration = 1.0f;
        public float nextstep = 0.3f;

        public CardContent[] safeDeckPile => handPile.ToArray();
        public CardContent[] safeDiscardPile => discardPile.ToArray();

        //测试用
        public List<CardSource> cards;
        protected override void UnityAwake() { }
        //
        void Start()
        {
            if (CharacterInDungeon.Ins == null)
            {
                foreach (var card in cards)
                {
                    card.GenerateToDeck();
                }
            }
            else
            {
                foreach (var cc in CharacterInDungeon.Ins.Deck)
                {
                    GenerateCardTo(cc, CardPosition.Deck);
                }
            }
        }

        #region 卡牌基础（抽卡，洗牌）

        /// <summary>
        /// 抽牌方法
        /// </summary>
        /// <param name="count">抽牌的张数</param>
        /// 为了互换性还留存着，最好直接使用协程
        [System.Obsolete]
        public void Draw(int count)
        {
            StartCoroutine(IDraw(null, count));
        }

        public IEnumerator IDraw(List<CardContent> outlist, int count)
        {
            var leftCount = count;
            while (leftCount > 0)
            {
                if (deckPile.Count == 0)
                {
                    if (discardPile.Count > 0)
                    {
                        yield return IRefreshUI();
                        yield return IShuffle();
                        continue;
                    }
                    else break;
                }
                else
                {
                    CardContent card = deckPile[0];
                    yield return IGoTo(card, CardPosition.Hand);
                    leftCount--;
                    card.OnDraw();
                    if (outlist != null)
                    {
                        outlist.Add(card);
                    }
                    if (leftCount == 0) break;
                }
            }
            yield return IRefreshUI();
            yield break;
        }

        /// <summary>
        /// 洗牌方法
        /// </summary>
        public IEnumerator IShuffle()
        {
            List<CardContent> temp = new List<CardContent>();
            //先将弃牌堆打乱
            foreach (CardContent card in discardPile)
            {
                temp.Insert(Random.Range(0, temp.Count), card);
            }
            //将所有弃牌放入牌堆中
            foreach (CardContent card in temp)
            {
                yield return IGoTo(card, CardPosition.Deck);
            }
            yield break;
        }

        public IEnumerator IRefreshUI()
        {
            var index = 0;
            var count = handPile.Count;
            var zero = -count * zeroPosition;
            foreach (var cc in handPile)
            {
                index++;
                cc.bUI.transform.DOLocalMove(index * shiftPosition + zero, duration);
                yield return new WaitForSeconds(nextstep);
            }
            yield break;
        }

        public void RefreshUI()
        {
            var index = 0;
            var count = handPile.Count;
            var zero = -count * zeroPosition;
            foreach (var cc in handPile)
            {
                index++;
                cc.bUI.transform.DOLocalMove(index * shiftPosition + zero, duration);
            }
        }
        #endregion


        #region 卡牌移动相关（使用，消耗，弃牌）
        public void Discard(CardContent cc)
        {
            GoTo(cc, CardPosition.Discard);
            cc.OnDiscard();
            RefreshUI();
        }

        public void Discard(List<CardContent> ccList)
        {
            foreach (var cc in ccList)
            {
                Discard(cc);
            }
        }

        public void DiscardAll()
        {
            var HandPile = handPile.ToArray();
            foreach (var hand in HandPile)
            {
                Discard(hand);
            }
            var UsedPile = usedPile.ToArray();
            foreach (var used in UsedPile)
            {
                Discard(used);
            }
        }
        public void Exhaust(CardContent cc)
        {
            GoTo(cc, CardPosition.Exhaust);
            RefreshUI();
        }

        public void Used(CardContent cc)
        {
            GoTo(cc, CardPosition.Used);
            RefreshUI();
        }
        public void ReturnToDeck(CardContent cc)
        {
            GoTo(cc, CardPosition.Deck);
            RefreshUI();
        }

        public void ReturnToHand(CardContent cc)
        {
            GoTo(cc, CardPosition.Hand);
            RefreshUI();
        }

        public void TempUse(CardContent cc)
        {
            GoTo(cc, CardPosition.Temp);
        }
        #endregion

        #region 卡牌去向
        private IEnumerator IGoTo(CardContent cc, CardPosition position)
        {
            GetCurrentList(cc).Remove(cc);
            cc.bUI.position = position;
            switch (position)
            {
                case CardPosition.Deck:
                    deckPile.Add(cc);
                    cc.bUI.transform.SetParent(deckPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    yield return new WaitForSeconds(nextstep);
                    break;
                case CardPosition.Discard:
                    discardPile.Add(cc);
                    cc.bUI.transform.SetParent(discardPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    yield return new WaitForSeconds(nextstep);
                    break;
                case CardPosition.Exhaust:
                    exhaustPile.Add(cc);
                    cc.bUI.transform.SetParent(exhaustPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    yield return new WaitForSeconds(nextstep);
                    break;
                case CardPosition.Hand:
                    handPile.Add(cc);
                    cc.bUI.transform.SetParent(handPos.transform, true);
                    break;
                case CardPosition.Used:
                    usedPile.Add(cc);
                    cc.bUI.transform.SetParent(usedPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    yield return new WaitForSeconds(nextstep);
                    break;
                case CardPosition.Temp:
                    tempPile.Add(cc);
                    break;
            }
            yield break;
        }
        //等决定动画后再逐步废除
        private void GoTo(CardContent cc, CardPosition position)
        {
            GetCurrentList(cc).Remove(cc);
            cc.bUI.position = position;
            switch (position)
            {
                case CardPosition.Deck:
                    deckPile.Add(cc);
                    cc.bUI.transform.SetParent(deckPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    break;
                case CardPosition.Discard:
                    discardPile.Add(cc);
                    cc.bUI.transform.SetParent(discardPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    break;
                case CardPosition.Exhaust:
                    exhaustPile.Add(cc);
                    cc.bUI.transform.SetParent(exhaustPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    break;
                case CardPosition.Hand:
                    handPile.Add(cc);
                    cc.bUI.transform.SetParent(handPos.transform, true);
                    break;
                case CardPosition.Used:
                    usedPile.Add(cc);
                    cc.bUI.transform.SetParent(usedPos.transform);
                    cc.bUI.transform.DOLocalMove(Vector3.zero, duration);
                    break;
                case CardPosition.Temp:
                    tempPile.Add(cc);
                    break;
            }
        }


        private List<CardContent> GetCurrentList(CardContent cc)
        {
            switch (cc.bUI.position)
            {
                case CardPosition.Hand:
                    return handPile;
                case CardPosition.Deck:
                    return deckPile;
                case CardPosition.Discard:
                    return discardPile;
                case CardPosition.Used:
                    return usedPile;
                case CardPosition.Exhaust:
                    return exhaustPile;
                case CardPosition.Temp:
                    return tempPile;
                default:
                    return deckPile;
            }
        }
        #endregion

        #region 生成卡牌
        private void GenerateCardUI(CardContent cc)
        {
            var go = GameObject.Instantiate(cardGo, deckPos.transform.position, deckPos.transform.rotation, deckPos.transform);
            go.SetCard(cc);
            cc.bUI = go;
        }

        public void GenerateCardTo(CardContent cc, CardPosition position)
        {
            GenerateCardUI(cc);
            GoTo(cc, position);
            cc.RefreshUI();
        }
        #endregion
    }

}

