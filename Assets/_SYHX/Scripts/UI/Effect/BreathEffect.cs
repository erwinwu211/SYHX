using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathEffect : MonoBehaviour
{
    public Color startCol;
    public Color endCol;
    public float duration;

    public Image image;
    private bool flag = false;
    private float timer = 0;

    private void Update()
    {
        if (flag)
        {
            timer += Time.deltaTime;
            image.color = Color.Lerp(startCol, endCol, timer / duration);
            if (timer > duration) 
            {
                flag = false;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            image.color = Color.Lerp(startCol, endCol, timer / duration);
            if (timer < 0) 
            {
                flag = true;
            }
        }

    }
}
