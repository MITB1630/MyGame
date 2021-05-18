using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptTower : Tower
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
        ElementType = Element.Corrupt;
    }

    public override Debuff GetDebuff()
    {

        return new PoisonDebuff(tickDamage, tickTime, DebuffDuration, target);
    }


}
