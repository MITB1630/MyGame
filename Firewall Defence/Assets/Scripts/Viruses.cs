using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Viruses : MonoBehaviour
{
    [SerializeField]
    private float speed;


    [SerializeField]
    private Stats healthStat;

    private Stack<Node> path;
    public Point GridPosition { get; set; }
    public bool IsActive { get; set; }
    private Vector3 destination;




    private string virusType;

    private List<Debuff> debuffs = new List<Debuff>();

    private List<Debuff> debuffsToRemove = new List<Debuff>();

    private List<Debuff> debuffsToAdd = new List<Debuff>();



    private SpriteRenderer mySR;

    public bool Alive
    {
        get { return healthStat.CurrentVal > 0; }

    }

    private void Awake()
    {
        virusType = this.name;
        myAnimator = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();
        healthStat.Initialize();
    }




    //Attachs the sprites to animator
    private Animator myAnimator;

    private void Update()
    {
        HandleDebuffs();
        Move();

    }


 



    public void Spawn(int health)
    {
        myAnimator = GetComponent<Animator>();
        this.healthStat.MaxVal = health;
        this.healthStat.CurrentVal = this.healthStat.MaxVal;

        this.healthStat.Bar.Reset();

        transform.position = LevelManagement.Instance.BluePortal.transform.position;
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1,1), false));

        SetPath(LevelManagement.Instance.FinalPath);
    }


    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {

     
        float progress = 0;
         while(progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;

        IsActive = true;

        if(remove == true)
        {
            Release();
        }



    }

    private void Move()
    {

        if(IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    Animate(GridPosition, path.Peek().gridPosition);
                    GridPosition = path.Peek().gridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }


       
    }


    private void SetPath(Stack<Node> newPath)
    {


        if(newPath != null)
        {
            this.path = newPath;
            Animate(GridPosition, path.Peek().gridPosition);



            GridPosition = path.Peek().gridPosition;
            destination = path.Pop().WorldPosition;
               
        }



    }



    //The animation will play depending on what direction the baddy is moving, this is done with X Y becuse values will change
    private void Animate(Point currentPosition, Point newPosition)
    {
        //Animate Down
        if(currentPosition.Y > newPosition.Y)
        {
            myAnimator.SetInteger("horizontal", 0);
            myAnimator.SetInteger("vertical", 1);
        }
        //Up
        else if(currentPosition.Y < newPosition.Y)
        {
            myAnimator.SetInteger("horizontal", 0);
            myAnimator.SetInteger("vertical", -1);
        }
        if(currentPosition.Y == newPosition.Y)
        {
            //Move to left (Had to put into Y IF statement as it was only exceuting the previous IF. Which makes sense becausE if Y is the same then theya re moving left or right
            if(currentPosition.X > newPosition.X)
            {
                myAnimator.SetInteger("horizontal", -1);
                myAnimator.SetInteger("vertical", 0);
            }
            //Move to Left
            else if(currentPosition.X < newPosition.X)
            {
                myAnimator.SetInteger("horizontal", 1);
                myAnimator.SetInteger("vertical", 0);
            }


        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "RedPortal")
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
            GameManagment.Instance.SystemIntegrity = GameManagment.Instance.SystemIntegrity - 4;
           

            if(this.name == "WormV")
            {
                GameManagment.Instance.Disk = GameManagment.Instance.Disk + 20;
                GameManagment.Instance.Memory = GameManagment.Instance.Memory + 20;
            }
            else if (this.name == "BotV")
            {
                GameManagment.Instance.Disk = GameManagment.Instance.Disk + 20;
            }
            else if (this.name == "BugV")
            {
                GameManagment.Instance.Cpu = GameManagment.Instance.Cpu + 40;
                GameManagment.Instance.Disk = GameManagment.Instance.Disk + 20;
            }
            else if (this.name == "SpyV")
            {
                GameManagment.Instance.Cpu = GameManagment.Instance.Cpu + 40;
                
            }


        }
        if(other.tag == "Path")
        {
            mySR.sortingOrder = other.GetComponent<TileScript>().GridPosition.Y;
        }


    }

    public void Release()
    {
        IsActive = false;
        GridPosition = LevelManagement.Instance.BlueSpawn;
        GameManagment.Instance.Pool.ReleaseObject(gameObject);
        GameManagment.Instance.RemoveVirus(this);
    }

    public void TakeDamage(float damage, Element damageType)
    {
        if(IsActive)
        {
           
                
            healthStat.CurrentVal -= damage;
                         
            if (healthStat.CurrentVal <= 0)
            {
                GameManagment.Instance.Bytes += 50;
                myAnimator.SetTrigger("Die");
                IsActive = false;

                GetComponent<SpriteRenderer>().sortingOrder--;
            }
        }
     
    }



    public void AddDebuff(Debuff debuff)
    {
        if(!debuffs.Exists(x => x.GetType() == debuff.GetType()))
        {
            debuffsToAdd.Add(debuff);
        }
      
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffsToRemove.Add(debuff);
    }

    private void HandleDebuffs()
    {

        if(debuffsToAdd.Count > 0)
        {
            debuffs.AddRange(debuffsToAdd);
            debuffsToAdd.Clear();
        }
        foreach (Debuff debuff in debuffsToRemove)
        {
            debuffs.Remove(debuff);
        }

        debuffsToRemove.Clear();

        foreach (Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }
}
