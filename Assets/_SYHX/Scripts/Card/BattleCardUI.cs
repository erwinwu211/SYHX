using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SYHX.Cards
{
    public class BattleCardUI : MonoBehaviour
    {
        public CardContent cc;
        public CardPosition position;
        [SerializeField] public Text nameField;
        [SerializeField] public Text descField;
        [SerializeField] public Text EPField;
        [SerializeField] public Text typeField;
        public void SetCard(CardContent cc)
        {
            this.cc = cc;
        }

        public void RefreshUI()
        {
            this.nameField.text = cc.name;
            this.descField.text = cc.Desc;
            this.EPField.text = cc.TempEP.ToString();
            this.typeField.text = cc.cardType.ToString();
        }
        public void OnPointClick()
        {
            if(position == CardPosition.Hand)
            cc.OnUse(CardUseTrigger.ByUser);
        }
    }
}
public enum CardPosition
{
    Hand, Deck, Exhaust, Discard, Used
}
