using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SYHX.Cards
{
    public class CraftableCardUI : MonoBehaviour
    {
        public CardContent cc;
        public Image image;
        public bool selected { get; private set; } = false;
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
            this.EPField.text = cc.EP+"";
        }
        
    }
}
