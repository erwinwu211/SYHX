using UnityEngine;
using TMPro;

namespace SYHX.Cards
{
    public class BattleCardUI : MonoBehaviour
    {
        public CardContent cc;
        public CardPosition position;
        [SerializeField] public TextMeshProUGUI nameField;
        [SerializeField] public TextMeshProUGUI descField;
        [SerializeField] public TextMeshProUGUI EPField;
        public void SetCard(CardContent cc)
        {
            this.cc = cc;
        }

        public void RefreshUI()
        {
            this.nameField.text = cc.name;
            this.descField.text = cc.Desc;
            this.EPField.text = cc.TempEP.ToString();
        }
        void OnMouseDown()
        {
            cc.OnUse(CardUseTrigger.ByUser);
        }
    }
}
public enum CardPosition
{
    Hand, Deck, Exhaust, Discard, Used
}
