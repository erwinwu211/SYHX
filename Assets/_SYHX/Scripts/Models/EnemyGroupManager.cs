using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    public static EnemyGroupManager Ins { get; private set; }
    public GameObject EnemyParent;
    [SerializeField] public List<EnemyGroup> enemyGroup;
    void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }
}
