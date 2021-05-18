using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperDebuff : Debuff
{
    public SniperDebuff(Viruses target) : base(target, 1)
    {


    }



    public override void Update()
    {
        target.TakeDamage(1, Element.Sniper);
        base.Update();
    }

}
