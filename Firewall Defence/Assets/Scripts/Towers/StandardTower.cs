using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : Tower
{
    // Start is called before the first frame update
    private void Start()
    {
        ElementType = Element.Standard;
    }

    public override Debuff GetDebuff()
    {

        return new StandardDebuff(target);
    }


}
