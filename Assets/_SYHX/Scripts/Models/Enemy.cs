using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using SYHX.EnemyAI;
using DG.Tweening;



[RequireComponent(typeof(EnemyAIHandler))]
public class Enemy : BattleCharacter
{
    // [SerializeField]
    public override bool isEnemy { get => true; }
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionText;
    public Button enemy;
    private ColorBlock defaultBlock;
    private EnemyAIHandler aiHandler;
    [ShowInInspector] private EnemyActionContent currentAction;

    public Outline outline;

    public Color endCorlor;
    public Color startColor;
    private Sequence seq;

    public override void ChildStart() => StartAI();

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
        seq = DOTween.Sequence();
        seq.Append(outline.DOColor(endCorlor, 1f));
        seq.Append(outline.DOColor(startColor, 1f));
        seq.SetLoops(-1);
        seq.Pause();
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
        outline.enabled = true;
        seq.Restart();
    }
    public virtual void LeaveSelected()
    {
        outline.enabled = false;
        seq.Pause();
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
