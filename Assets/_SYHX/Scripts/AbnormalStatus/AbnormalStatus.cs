using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalStatus : Assitant<BattleCharacter>
{
    public AbnormalStatus(BattleCharacter owner) : base(owner)
    {
        owner.buffs.Add(this);
    }

}
