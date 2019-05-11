using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour
{
    public bool Active;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        this.gameObject.SetActive(Active);
    }
}
