using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardUI : AssitantMonobehaviour<BattleCardUI>
{
    public CardContent cc;
    void OnMouseDown()
    {
        cc.OnUse();
    }
}
