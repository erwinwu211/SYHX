using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContent : MonoBehaviour
{
    CardContent owner;
    public void OnDraw() => owner.OnDraw();
    public void OnUse() => owner.OnUse();
    public void OnFold() => owner.OnFold();
    public void OnExiled() => owner.OnExiled();
    public void OnOtherCardUse() => owner.OnOtherCardUse();
}
