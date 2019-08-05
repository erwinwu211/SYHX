using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SYHX.Cards
{
    public class ProtoCardUI : MonoBehaviour
    {
        public Sprite redFrame;
        public Sprite blueFrame;
        public Sprite yellowFrame;

        public CardContent cc;
        [SerializeField] public Image CardFrame;
        [SerializeField] public Image BgImage;
        [SerializeField] public Text nameField;
        [SerializeField] public Text descField;
        [SerializeField] public Text EPField;
        [SerializeField] public Text typeField;
        [SerializeField] public Text connectionTypeField;
        public void SetCard(CardContent cc)
        {
            this.cc = cc;
            this.name = cc.name;
            RefreshUI();
        }

        public void RefreshUI()
        {
            this.nameField.text = cc.name;
            this.descField.text = cc.Desc;
            this.EPField.text = cc.TempEP.ToString();
            this.typeField.text = cc.cardType.ToString();
            switch (cc.connectionType)
            {
                case ConnectionType.红:
                    this.CardFrame.sprite = redFrame;
                    this.connectionTypeField.text = "火力";
                    break;
                case ConnectionType.蓝:
                    this.CardFrame.sprite = blueFrame;
                    this.connectionTypeField.text = "战术";
                    break;
                case ConnectionType.黄:
                    this.CardFrame.sprite = yellowFrame;
                    this.connectionTypeField.text = "支援";
                    break;
            }
        }
    }
}