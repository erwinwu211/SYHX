using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(PlayMakerFSM))]
public class EnemyAIHandler : MonoBehaviour
{
    [NonSerialized] public Enemy enemy;
    [NonSerialized] public PlayMakerFSM fsm;
    void Awake()
    {
        enemy = this.GetComponent<Enemy>();
        fsm = this.GetComponent<PlayMakerFSM>();
    }

    public void DoNext()
    {
        fsm.SendEvent("_NEXT");
    }
    public void DoStart()
    {
        fsm.SendEvent("_START");
    }
}
