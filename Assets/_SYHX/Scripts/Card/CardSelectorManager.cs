using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYHX.Cards
{
    public class CardSelectorManager : SingletonMonoBehaviour<CardSelectorManager>
    {
        protected override void UnityAwake(){}
        public static void SetIns(CardSelectorManager go)=>Ins = go;
        private List<CardContent> outList;
        private List<SelectableCardUI> uiList = new List<SelectableCardUI>();

        public bool Selectable()
        {
            if(outList.Count < maxValue)
            {
                return true;
            }
            return false;
        }
        private int minValue;
        private int maxValue;
        [SerializeField] private GameObject content;
        [SerializeField] private SelectableCardUI originUI;

        private bool finished = false;

        public IEnumerator Register(List<CardContent> inList,List<CardContent> outList,int minValue = 1,int maxValue = 1)
        {
            if(inList.Count <= minValue)
            { 
                outList = inList;
                yield break;
            }
            this.outList = outList;
            finished = false;
            this.minValue = minValue;
            this.maxValue = maxValue;
            foreach(var cc in inList)
            {
                var ui = GameObject.Instantiate(originUI,content.transform);
                uiList.Add(ui);
                ui.SetCard(cc);
            }
            this.gameObject.SetActive(true);
            yield return new WaitUntil(()=>finished);
            var tempList = uiList.ToArray();
            foreach(var ui in tempList)
            {
                GameObject.Destroy(ui.gameObject);
            }
            uiList.Clear();
            this.gameObject.SetActive(false);
            yield break;
        }

        public void SelectCard(CardContent cc) => outList.Add(cc);

        public void DeselectCard(CardContent cc)
        {
            if(outList.Contains(cc))outList.Remove(cc);
        }

        public void OnPointClick()
        {
            if(outList.Count >= minValue && outList.Count <= maxValue)finished = true;
        }
    }
}
