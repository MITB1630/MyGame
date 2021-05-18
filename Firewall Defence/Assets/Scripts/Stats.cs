using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stats
{
    [SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxVal;



    public BarScript Bar
    {
        get
        {
            return bar;
        }
    }


    public float MaxVal
    {

        get
        {

            return maxVal;
        }
        set
        {        
            this.maxVal = value;
            bar.MaxValue = value;
        }
    }

    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get { return currentVal; }

        set {

            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;
        }
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
