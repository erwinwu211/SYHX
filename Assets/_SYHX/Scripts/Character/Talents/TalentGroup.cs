using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentGroup : ScriptableObject
{
    // Start is called before the first frame update
    public int needLv;
    public List<Talent> talents;
    public Talent ActiveTalent;
}
