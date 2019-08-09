using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SYHX.Cards
{
    public class ProtoCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Sprite redFrame;
        public Sprite blueFrame;
        public Sprite yellowFrame;
        public bool canSelect;
        public bool IsSelect;
        public bool canZoom;
        public float scaleSpeed = 1.4f;

        public CardContent cc;
        [SerializeField] public Image CardFrame;
        [SerializeField] public Image BgImage;
        [SerializeField] public Text nameField;
        [SerializeField] public Text descField;
        [SerializeField] public Text EPField;
        [SerializeField] public Text typeField;
        [SerializeField] public Text connectionTypeField;
        [SerializeField] public Image ChosenShadow;

        /// <summary>
        /// 设置卡牌信息
        /// </summary>
        /// <param name="cc">卡牌</param>
        /// <param name="canSelect">是否能显示选择框</param>
        /// <param name="canScale">是否鼠标移入有放大</param>
        public void SetCard(CardContent cc, bool canSelect = false, bool canScale = true)
        {
            this.cc = cc;
            this.name = cc.name;
            this.canSelect = canSelect;
            this.canZoom = canScale;
            RefreshUI();
        }

        public void RefreshUI()
        {
            this.nameField.text = cc.name;
            this.descField.text = cc.Desc;
            this.EPField.text = cc.TempEP.ToString();
            this.typeField.text = cc.cardType.ToString()+"-"+Initializer.Ins.TechlvInfos[cc.techLevel].LvName;
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

        public void ShowSelected()
        {
            IsSelect = true;
            ChosenShadow.gameObject.SetActive(true);
        }

        public void EnhanceSelected()
        {
            IsSelect = false;
            ChosenShadow.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (canZoom)
            {
                StopCoroutine("ScaleDown");
                StartCoroutine("ScaleUp");
            }
        }

        /// <summary>
        /// 鼠标移出时，收起背景然后隐藏
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (canZoom)
            {
                StopCoroutine("ScaleUp");
                StartCoroutine("ScaleDown");
            }
        }

        IEnumerator ScaleUp()
        {
            for (; this.gameObject.transform.localScale.x < 1.25f;)
            {
                this.gameObject.transform.localScale += new Vector3(scaleSpeed*Time.deltaTime,scaleSpeed*Time.deltaTime,0);
                yield return null;
            }
        }

        IEnumerator ScaleDown()
        {
            for (; this.gameObject.transform.localScale.x > 1;)
            {
                this.gameObject.transform.localScale -= new Vector3(scaleSpeed*Time.deltaTime,scaleSpeed*Time.deltaTime,0);
                yield return null;
            }
        }
    }
}