using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Tower
{
    private void Start()
    {
        ElementType = Element.Sniper;
    }

    public override Debuff GetDebuff()
    {
        return new SniperDebuff(target);
    }

}
