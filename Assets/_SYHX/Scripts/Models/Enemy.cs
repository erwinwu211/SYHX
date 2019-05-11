using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyAIHandler))]
public class Enemy : BattleCharacter
{
    // [SerializeField]
    public TextMeshProUGUI text;
    public Button enemy;
    private ColorBlock defaultBlock;
    public EnemyAIHandler aiHandler;
    private EnemyAction nextAction;
    public override void TakeDamage(BattleCharacter bc, int damage)
    {
        base.TakeDamage(bc, damage);
        ShowHp();
    }
    public void SetEnemy(EnemySource enemySource)
    {
        this.currentHp = enemySource.currentHp;
        this.maxHp = enemySource.maxHp;
        this.attack = enemySource.attack;
        this.defence = enemySource.defence;
        this.isAlive = true;
        defaultBlock = enemy.colors;
        ShowHp();
    }
    public void ShowHp()
    {
        text.text = $"{this.currentHp}/{this.maxHp}";
    }
    public virtual void OnSelected()
    {
        var colors = enemy.colors;
        colors.normalColor = colors.highlightedColor = colors.pressedColor = new Color(1f, 0f, 0f, 1f);
        enemy.colors = colors;
    }
    public virtual void LeaveSelected()
    {
        enemy.colors = defaultBlock;
    }

    public void SetAIHandler(EnemyAIHandler handler)
    {
        this.aiHandler = handler;
    }
    public void StartAI() => aiHandler.DoStart();
    public void NextAI() => aiHandler.DoNext();

    //临时用
    public override bool IsAlive() => true;

    public void SelectThis()
    {
        BattleCharacterManager.Ins.SelectEnemy(this);
    }

    public void SetAction(EnemyAction action)
    {
        this.nextAction = action;
    }
    public IEnumerator Execute()
    {
        yield return StartCoroutine(nextAction.Execute());
        yield break;
    }
}
