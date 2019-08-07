using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade : ScriptableObject
{
    public GradeType currentGrade;
    public Grade nextGrade;
    public int LvMax;
    public List<ItemSourceAndCount> UpgradeRequireList;
}


public enum GradeType
{
    One,Two,Three,Four,Five
}
