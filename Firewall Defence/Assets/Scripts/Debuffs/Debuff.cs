using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff {

    protected Viruses target;

    protected float duration;

    protected float elasped;
 
    public Debuff(Viruses target, float duration)
    {

        this.target = target;
        this.duration = duration;
    }

    public virtual void Update()
    {

        elasped += Time.deltaTime;

        if(elasped >= duration)
        {
            Remove();
        }
    }

    public virtual void Remove()
    {
       if(target != null)
        {
            target.RemoveDebuff(this);
        }
    }
}
