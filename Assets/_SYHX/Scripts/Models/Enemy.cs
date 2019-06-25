using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;



[RequireComponent(typeof(EnemyAIHandler))]
public class Enemy : BattleCharacter
{
    // [SerializeField]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionText;
    public Button enemy;
    private ColorBlock defaultBlock;
    private EnemyAIHandler aiHandler;
    [ShowInInspector] private EnemyActionContent currentAction;

    public override void RefreshUI()
    {
        ShowHp();
        ShowStatus();
        ShowAction();
    }
    public void SetEnemy(EnemySource enemySource)
    {
        this.currentHp = enemySource.currentHp;
        this.maxHp = enemySource.maxHp;
        this.attack = enemySource.attack;
        this.defence = enemySource.defence;
        this.isAlive = true;
        defaultBlock = enemy.colors;
    }
    public void ShowHp()
    {
        hpText.text = $"{this.currentHp}/{this.maxHp}";
    }
    public void ShowAction()
    {
        if (currentAction != null)
        {
            actionText.text = currentAction.Desc;
        }
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
    public void SetAction(EnemyActionContent action) => currentAction = action;
    public void ExecuteAction() => currentAction.Execute();

    public void SelectThis()
    {
        BattleCharacterManager.Ins.SelectEnemy(this);
    }
}
