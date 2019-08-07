using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYHX.Cards
{

    public class CardData : ScriptableObject
    {
        [System.Serializable]
        public class Data
        {
            public int ID;
            public string 名称;
            public ConnectionType[] 类型1;
            public CardType 类型2;
            public int 升级花费;

            public int[] 升级选项列表;
            public string source;
            public string option;
        }
        public Data[] Cards;
    }
}


