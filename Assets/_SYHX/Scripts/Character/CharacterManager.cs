using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{
    public CharacterContent FukasakiKotone;

    protected override void UnityAwake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadCharacterInfo();
    }

    /// <summary>
    /// TODO：读取存档来实例化角色信息
    /// </summary>
    public void LoadCharacterInfo()
    {
        FukasakiKotone = new FukasakiKotone().GenerateCharacter();
    }
}
