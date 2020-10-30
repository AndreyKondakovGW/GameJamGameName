using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class Humanoid : Enemy
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator animator;

    bool isTracked = false;
    bool isChasing = false;
    bool isOnWayHome = false;

    GameObject chasing;
    Transform homePoint;

    Vector2 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (!sr)
        {
            sr = transform.Find("sprite").GetComponent<SpriteRenderer>();
        }

        if (!animator)
        {
            animator = transform.Find("sprite").GetComponent<Animator>();
        }

        homePoint = gameObject.transform.Find("home_point");
        Debug.Log(homePoint.name);
        homePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        UpdateAI();
        rb.velocity = movementDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            sr.sortingOrder = 11;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            sr.sortingOrder = 9;
        }
    }

    protected override void UpdateAI()
    {
        if (isChasing)
        {
            Debug.Log("CHASING" + chasing.transform.position + " " + chasing.name);
            movementDirection = (chasing.transform.position - transform.position).normalized;
            if (movementDirection != Vector2.zero)
            {
                animator.SetFloat("Horizontal", movementDirection.x);
                animator.SetFloat("Vertical", movementDirection.y);
            }
            animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        }
        else if (isOnWayHome)
        {
            Vector2 diff = homePoint.position - transform.position;
            if (diff.sqrMagnitude < 0.1)
            {
                isOnWayHome = false;
                return;
            }
            movementDirection = diff.normalized / 2;
            if (movementDirection != Vector2.zero)
            {
                animator.SetFloat("Horizontal", movementDirection.x);
                animator.SetFloat("Vertical", movementDirection.y);
            }
            animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }

    bool SeesObstacle()
    {
        RaycastHit2D[] result = Physics2D.LinecastAll(chasing.transform.position, transform.position);
        if (result.Any(x => x.transform.tag == "Walls"))
        {
            Debug.Log(result.FirstOrDefault(x => x.transform.tag == "Walls").transform.name);
        }
        
        return result.Any(x => x.transform.tag == "Walls");
    }
    

    void ObstacleCheck()
    {
        Debug.Log("OBSTACLE CHECK " + isChasing + " " + chasing.transform.position);
        if (SeesObstacle())
        { 
            Debug.Log("WALL " + isChasing);
            GiveUpChasing();
        }
    }

    void GiveUpChasing()
    {
        isChasing = false;
        CancelInvoke();
        movementDirection = Vector2.zero;
        animator.SetFloat("Speed", 0.0f);
        Invoke("GoHome", 3.0f);
    }

    void GoHome()
    {
        isOnWayHome = true;
    }

    void ArrivedHome()
    {

    }

    protected override void OnHit(GameObject player)
    {

    }
    protected override void OnLostTrack()
    {

    }
    protected override void OnDeath()
    {

    }

    void StartChasing(GameObject player)
    {
        isChasing = true;
        isOnWayHome = false;
        InvokeRepeating("ObstacleCheck", 0.0f, 0.5f);
    }

    public override void OnEnterWarningRange(GameObject player)
    {
        Debug.Log("entered warning");
        isTracked = true;
    }
    public override void OnEnterReactionRange(GameObject player)
    {
        Debug.Log("entered reaction");
        chasing = player;
        if (!SeesObstacle())
        {
            Debug.Log("start chasing");
            StartChasing(player);
        }
    }

    public override void OnStayReactionRange(GameObject player)
    {
        if (!isChasing && !SeesObstacle())
        {
            StartChasing(player);
        }
    }

    public override void OnExitWarningRange(GameObject player)
    {
        Debug.Log("exited warning");
        isTracked = false;
        GiveUpChasing();
    }

    public override void OnExitReactionRange(GameObject player)
    {
        Debug.Log("eexited reaction");
    }
}
