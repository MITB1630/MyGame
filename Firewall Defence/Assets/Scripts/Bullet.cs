using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Viruses target;
    private Tower parent;
    private Animator animator;

    private Element elementType;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Track();
    }

    public void Initialize(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
        this.elementType = parent.ElementType;
    }


    private void Track()
    {
        if(target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.BulletSpeed);

            Vector2 dir = target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if(!target.IsActive)
        {
            GameManagment.Instance.Pool.ReleaseObject(gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Virus")
          {
            if(target.gameObject == other.gameObject)
            {
                target.TakeDamage(parent.Damage, elementType);
                GameManagment.Instance.Pool.ReleaseObject(animator.gameObject);
                ApplyDebuff();
            }
      
           
          }

    }

    private void ApplyDebuff()
    {
        float roll = Random.Range(0, 100);

        if(roll <= parent.Proc)
        {
            target.AddDebuff(parent.GetDebuff());
        }
            

       
    }
}
