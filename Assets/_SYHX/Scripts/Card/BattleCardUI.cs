using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCardUI : MonoBehaviour
{
    public CardContent cc;
    [SerializeField] public TextMeshProUGUI nameField;
    [SerializeField] public TextMeshProUGUI descField;
    private bool refreshed = false;
    public void SetCard(CardContent cc)
    {
        this.cc = cc;
    }
    void Update()
    {
        if (!refreshed)
        {
            if (cc != null)
            {
                this.nameField.text = cc.name;
                this.descField.text = cc.Desc;
                refreshed = true;
            }
        }

    }
    void OnMouseDown()
    {
        cc.OnUse(CardUseTrigger.ByUser);
    }
}
