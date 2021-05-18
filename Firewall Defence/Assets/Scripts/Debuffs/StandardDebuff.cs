using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardDebuff : Debuff
{
    public StandardDebuff(Viruses target) : base(target, 1)
    {


    }



    public override void Update()
    {
        target.TakeDamage(1, Element.Standard);
        base.Update();
    }

}
