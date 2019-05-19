using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent : SAssitant<CharacterContent>
{
    public Talent() { }
    public bool IsEffect;

    public override void SetOwner(CharacterContent owner)
    {
        base.SetOwner(owner);
    }

    public void DoEffect()
    {
        if (IsEffect)
        {
            Effect();
        }
    }

    protected virtual void Effect() { }
}
