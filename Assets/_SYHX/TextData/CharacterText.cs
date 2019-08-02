using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterText : ScriptableObject
{
    [System.Serializable]
    public class Character
    {
        public string Name;
        public string Chinese;
        public string Japanese;
    }

    [TableList] public Character[] characters;
}
