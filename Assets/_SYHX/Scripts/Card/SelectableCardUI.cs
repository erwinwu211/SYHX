using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SYHX.Cards
{
    public class SelectableCardUI : MonoBehaviour
    {
        public CardContent cc;
        public Image image;
        public bool selected {get ; private set;} = false;
        [SerializeField] public Text nameField;
        [SerializeField] public Text descField;
        [SerializeField] public Text EPField;
        public void SetCard(CardContent cc)
        {
            this.cc = cc;
            RefreshUI();
        }

        public void RefreshUI()
        {
            this.nameField.text = cc.name;
            this.descField.text = cc.Desc;
            this.EPField.text = cc.TempEP.ToString();
        }
        public void OnPointClick()
        {   
            if(selected)Deselect();
            else if(CardSelectorManager.Ins.Selectable())Select();
        }

        private void Select()
        {
            this.selected = true;
            this.image.color = new Color(0,0,1f);
            CardSelectorManager.Ins.SelectCard(this.cc);  
        }

        private void Deselect()
        {
            this.selected = false;
            this.image.color = new Color(1f,0,0);
            CardSelectorManager.Ins.DeselectCard(this.cc);
        }

    }
}
