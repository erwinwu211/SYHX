using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public void OnAdventureBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new ChooseState(SceneStateManager.Ins));
    }

    public void OnCharacterBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new CharacterState(SceneStateManager.Ins));
    }
}
