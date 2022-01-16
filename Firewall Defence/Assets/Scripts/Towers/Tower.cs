using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum Element { Standard, Corrupt, Sniper, Slow}

public abstract class Tower : MonoBehaviour
{
    private SpriteRenderer mySR;
    public Viruses target;
    private Animator animator;





    [SerializeField]
    private string towerType;

    public Element ElementType
    {
        get;
        protected set;
    }




    private int price = 50;


    public int Price
    {
        get { return price; }

        set { this.price = value; }
    }

    public Viruses Target
    {
        get
        {
            return target;
        }
    }


    [SerializeField]
    private string bulletVer;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private int damage;



    [SerializeField]
    private float debuffDuration;

    [SerializeField]
    private float proc;


    public float Proc
    {
        get { return proc; }
        set { this.proc = value; }
    }



    public float DebuffDuration
    {
        get
        {
            return debuffDuration;
        }
        set
        {
            this.debuffDuration = value;
        }
    }



    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public float BulletSpeed
    {
        get
        {
            return bulletSpeed;
        }
    }

    private Queue<Viruses> viruses = new Queue<Viruses>();




    private bool canAttack = true;

    private float attackTimer;
    [SerializeField]
    private float attackCooldown;

    // Start is called before the first frame update
    void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Select()
    {


        mySR.enabled = !mySR.enabled;

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Virus")
        {
            viruses.Enqueue(other.GetComponent<Viruses>());
            Debug.Log(other);
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Virus")
        {
            target = null;
            Debug.Log("Target has left");
        }

    }


    private void Attack()
    {


        if (!canAttack)
        {
            attackTimer += Time.deltaTime;


            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }


        }

        if (target == null && viruses.Count > 0)
        {
            target = viruses.Dequeue();


        }
        if (target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();
                animator.SetTrigger("Attack");
                canAttack = false;
            }

        }
        else if (viruses.Count > 0)
        {
            target = viruses.Dequeue();

        }
        if (target != null && !target.Alive)
        {
            target = null;
        }





    }

    public abstract Debuff GetDebuff();
   


    private void Shoot()
    {

        Bullet bullet = GameManagment.Instance.Pool.GetObject(bulletVer).GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        bullet.Initialize(this);
    }
}
