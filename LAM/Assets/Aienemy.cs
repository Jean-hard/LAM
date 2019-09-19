using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Aienemy : MonoBehaviour
{

    public Transform target;

   static public float speed = 300f;
    public float nextWaypointDistance = 3f;
    Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    [SerializeField]
    private Animator anim;
    Seeker seeker;
    Rigidbody2D rb;
    [SerializeField]
    private Transform sprite;
    public float scale= 1f;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    
        
        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        //int go = 1;

     
        //if (go == 1)
        //{
        //    speed = 300;
        //}

        //if (go == 0)
        //{
        //    speed = 0;
        //}
        if (path == null)
        {
            return;
        }
            
     if (currentWaypoint>= path.vectorPath.Count)
        {
            speed = 0;
            reachedEndOfPath = true;
           
            
            return;
        }
     else
        {
            
            reachedEndOfPath = false;
           // speed = 300f;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance<nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.y>0.01f)
        {
            if(scale>0.5f) {
                scale -= 0.003f;
            }
            
        }

        if (rb.velocity.y < 0.01f)
        {
            if (scale < 1.2f)
            {
                scale += 0.003f;
            }
        }
        if (Mathf.Abs(rb.velocity.y) < Mathf.Abs(rb.velocity.x))
        {
            
            if (rb.velocity.x >= 0.01f)
            {
                anim.SetInteger("GO", 1);
                sprite.localScale = new Vector2(scale, scale);
            }
            if (rb.velocity.x <= -0.01f)
            {
                anim.SetInteger("GO", 1);
                sprite.localScale = new Vector2(-scale, scale);
            }
        }

        else if (speed==0)
        {
            anim.SetInteger("GO", 0);
        }
        else
        {
            if (rb.velocity.y >= 0.01f)
            {
               // scale -= 0.05f;
                anim.SetInteger("GO", 2);
                sprite.localScale = new Vector2(scale, scale);
            }
            if (rb.velocity.x <= -0.01f)
            {
                //scale += 0.05f;
                anim.SetInteger("GO", 2);
                sprite.localScale = new Vector2(scale, scale);
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {

            Debug.Log(speed);
        }

        //if (dist<=10f)
        //   {
        //       speed = 0;
        //   }

    }
}
