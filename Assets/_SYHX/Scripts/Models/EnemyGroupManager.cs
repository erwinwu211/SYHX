using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : SingletonMonoBehaviour<EnemyGroupManager>
{

    public GameObject EnemyParent;
    [SerializeField] public List<EnemyGroup> enemyGroup;
    protected override void UnityAwake() { }
}
