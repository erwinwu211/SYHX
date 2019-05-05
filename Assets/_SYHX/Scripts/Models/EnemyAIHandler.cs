using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(PlayMakerFSM))]
public class EnemyAIHandler : MonoBehaviour
{
    public Enemy enemy;
    public PlayMakerFSM fsm;
    void Awake()
    {
        enemy = this.GetComponent<Enemy>();
        fsm = this.GetComponent<PlayMakerFSM>();
    }
}
