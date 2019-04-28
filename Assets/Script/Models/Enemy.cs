using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : BattleCharacter
{
    // [SerializeField]
    public TextMeshProUGUI text;
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        ShowHp();
    }
    public void SetEnemy(EnemySource enemySource)
    {
        this.currentHp = enemySource.currentHp;
        this.maxHp = enemySource.maxHp;
        this.attack = enemySource.attack;
        this.defence = enemySource.defence;
        this.isAlive = true;
        ShowHp();
    }
    public void ShowHp()
    {
        text.text = $"{this.currentHp}/{this.maxHp}";
    }
    public virtual void OnSelected()
    {

    }

}
