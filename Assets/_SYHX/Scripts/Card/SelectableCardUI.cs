using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SYHX.Cards
{
    public class SelectableCardUI : ProtoCardUI
    {
        public bool selected {get ; private set;} = false;
        
        public void OnPointClick()
        {   
            if(selected)Deselect();
            else if(CardSelectorManager.Ins.Selectable())Select();
        }

        private void Select()
        {
            this.selected = true;
            this.ChosenShadow.color = new Color(0,0,1f);
            CardSelectorManager.Ins.SelectCard(this.cc);  
        }

        private void Deselect()
        {
            this.selected = false;
            this.ChosenShadow.color = new Color(1f,0,0);
            CardSelectorManager.Ins.DeselectCard(this.cc);
        }

    }
}
