using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public void OnReturnBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new MainState(SceneStateManager.Ins));
    }
}
