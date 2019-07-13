using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageMono : MonoBehaviour
{
    public Text damage;

    // Start is called before the first frame update
    void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOLocalMoveY(20f, 1f).SetRelative())
        .Append(this.transform.DOLocalMoveY(-20f, 1f).SetRelative())
        .OnComplete(() => GameObject.Destroy(this.gameObject));
    }

    public void SetText(string text)
    {
        damage.text = text;
    }
}
