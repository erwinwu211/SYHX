using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSource : ScriptableObject
{
    public string desc;
    public virtual void Effect() { }
}
