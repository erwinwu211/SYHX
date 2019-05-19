using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : SingletonMonoBehaviour<SceneStateManager>
{
    protected override void UnityAwake()
    {
    }

    public SceneState current;
    private AsyncOperation mAO;
    private bool mHasStartDone = false;

    public void SetSceneStatus(SceneState next, bool needLoad = true)
    {
        //当前状态不为空时，则执行当前状态的end方法
        if (current != null)
        {
            current.Exit();
        }
        //更新当前状态
        current = next;
        //根据新状态加载场景,若不需要加载，则直接进行start操作
        if (needLoad)
        {
            mAO = SceneManager.LoadSceneAsync(current.SceneName());
            mHasStartDone = false;
        }
        else
        {
            current.Enter();
            mHasStartDone = true;
        }
    }

    public void StateUpdate()
    {
        //当进行了异步加载，但还未加载完成时
        if (mAO != null && mAO.isDone == false)
        {
            return;
        }
        //当进行了异步加载，且加载完成时，进行只执行一次的start操作
        if (mHasStartDone == false && mAO != null && mAO.isDone)
        {
            current.Enter();
            mHasStartDone = true;
        }
        //根据当前的状态进行更新
        if (current != null)
        {
            current.Update();
        }
    }

    public void OnAdventureBtnClick()
    {
        SetSceneStatus(new ChooseState(this));
    }

    public void OnCharacterBtnClick()
    {
        SetSceneStatus(new CharacterState(this));
    }
}
