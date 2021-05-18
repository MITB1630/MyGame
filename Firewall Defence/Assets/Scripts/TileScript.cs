using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{

    public Point GridPosition { get; private set; }

    public bool isEmpty { get; set; }


    private Color32 fullColour = new Color32(255, 118, 118, 255);
    private Color32 emptyColour = new Color32(96, 255, 90, 255);
    private Tower myTower;


    private SpriteRenderer spriteRenderer;
   

   

    public bool Debugging { get; set; }
    public bool Walkable { get; set; }


    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }



    }

   

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpGrid(Point gridPosition, Vector3 worldPosition, Transform parent)
    {
        if(this.tag == "Wall")
        {
            Walkable = false;
        }
        else
        {
            Walkable = true;
        }
       

        if(this.tag == "Path")
        {
            isEmpty = false;
        }
        else
        {
            isEmpty = true;
        }

      
        this.GridPosition = gridPosition;
        transform.position = worldPosition;
        transform.SetParent(parent);
        LevelManagement.Instance.Tiles.Add(gridPosition, this);
    
    }

    private void OnMouseOver()
    {

        if (!EventSystem.current.IsPointerOverGameObject() && GameManagment.Instance.ClickButton != null)
        {
            if (isEmpty && !Debugging)
            {
                PaintTile(emptyColour);
            }
            if (!isEmpty && !Debugging)
            {
                PaintTile(fullColour);

            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }            
        else if(!EventSystem.current.IsPointerOverGameObject() && GameManagment.Instance.ClickButton == null && Input.GetMouseButtonDown(0))
        {

            if(myTower != null)
            {
                GameManagment.Instance.SelectTower(myTower);


            }
            else
            {
                GameManagment.Instance.Deselected();
            }

        }
    }

    private void OnMouseExit()
    {
        if(!Debugging)
        {
            PaintTile(Color.white);

        }


    }

    private void PlaceTower()
    {

       
        GameObject tower = Instantiate(GameManagment.Instance.ClickButton.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
        GameManagment.Instance.BuyTower();


        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

        //myTower.Price = GameManagment.Instance.ClickButton.Bytes;

        isEmpty = false;
        PaintTile(Color.white);
        Walkable = false;


    }

    private void PaintTile(Color32 newPaint)
    {
        spriteRenderer.color = newPaint;
    }

}
