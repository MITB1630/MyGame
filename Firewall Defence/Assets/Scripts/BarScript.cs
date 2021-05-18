using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    private float fillAmount;


    [SerializeField]
    private Image content;

    //[SerializeField]
    //private Text valueText;

    [SerializeField]
    private float lurpSpeed;


    [SerializeField]
    private Color fullcolor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private bool lerpColours;

    private float maxValue;


    public float MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            this.maxValue = value;
        }
    }

    public float Value
    {
        
        set
        {
            //string[] tmp = valueText.text.Split(':');
          //  valueText.text = tmp[0] + ":" + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if(lerpColours)
        {
            content.color = fullcolor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }
    private void HandleBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lurpSpeed);
        }
        if(lerpColours)
        {
            content.color = Color.Lerp(lowColor, fullcolor, fillAmount);
        }
      
       
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void Reset()
    {
        content.fillAmount = 1;
        Value = MaxValue;
    }
}
