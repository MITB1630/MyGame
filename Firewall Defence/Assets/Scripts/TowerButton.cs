using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;


   


    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int bytes;

    [SerializeField]
    private Text byteText;

    public Sprite Sprite 
    { 
        get 
        { 
          return sprite;
        } 
    }





    private void Start()
    {
        byteText.text = Bytes + " Bytes"; 
    }



    


    public GameObject TowerPrefab { 

        get
        { 
            return towerPrefab;
        }

    }

    public int Bytes 
    { 
        get
        {
            return bytes;
        }
        set 
        {

            this.bytes = value;
        }
    }
}
