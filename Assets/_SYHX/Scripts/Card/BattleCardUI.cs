using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCardUI : MonoBehaviour
{
    public CardContent cc;
    [SerializeField]
    public TextMeshProUGUI tmPro;
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
                this.tmPro.text = cc.owner.Desc;
                refreshed = true;
            }
        }

    }
    void OnMouseDown()
    {
        cc.OnUse();
    }
}
