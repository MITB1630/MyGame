using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : Tower
{
    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get { return tickTime; }
    }

    public float TickDamage
    {
        get { return tickDamage; }
    }

    private void Start()
    {
        ElementType = Element.Slow;
    }

    public override Debuff GetDebuff()
    {

        return new SlowDebuff(tickDamage, tickTime, DebuffDuration, target);
    }


}
