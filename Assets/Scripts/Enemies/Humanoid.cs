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

    bool isAttacking = false;
    bool isChasing = false;
    bool isOnWayHome = false;
    bool isDead = false;

    AttackDirection playerDirection;

    GameObject chasing;
    Transform homePoint;
    MiniHPBar miniHPBar;

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

        if (!miniHPBar)
        {
            miniHPBar = transform.Find("HPbar").GetComponent<MiniHPBar>();
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
        if (!isDead)
        {
            UpdateAI();
            rb.velocity = movementDirection * speed;
        }
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
        if (isChasing && !isAttacking)
        {
            //Debug.Log("CHASING" + chasing.transform.position + " " + chasing.name);
            Vector2 diff = chasing.transform.position - transform.position;
            direction = diff.x > 0 ? AttackDirection.right : AttackDirection.left;
            float magnitude = diff.magnitude;
            if (magnitude < attackRange)
            {
                Debug.Log("ATTACK" + chasing.transform.position + " " + chasing.name);
                isAttacking = true;
                animator.SetBool("Attacking", true);
                Invoke("AttackPlayer", attackDelay);
                Invoke("ResetAttacking", attackDuration);
                movementDirection = Vector2.zero;
            }
            else
            {
                diff = new Vector2(diff.x, diff.y * 4);
                movementDirection = (diff).normalized;
                if (movementDirection != Vector2.zero)
                {
                    animator.SetFloat("Horizontal", movementDirection.x);
                    animator.SetFloat("Vertical", movementDirection.y);
                }
                animator.SetFloat("Speed", movementDirection.magnitude);
            }
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

    void ResetAttacking()
    {
        isAttacking = false;
        animator.SetBool("Attacking", false);
    }

    void AttackPlayer()
    {
        if (chasing != null && playerDirection == direction)
        {
            chasing.GetComponent<Rigidbody2D>().velocity = (chasing.transform.position - transform.position) * 10;
            PlayerStats st = chasing.GetComponent<PlayerStats>();
            st.HP = st.HP - damage;
        }
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
        CancelInvoke("ObstacleCheck");
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

    public override void OnHit(GameObject player, float Damage)
    {
        health -= Damage;
        miniHPBar.UpdateHPByRatio(health/maxHealth);
        if (health <= 0)
        {
            isDead = true;
            OnDeath();
        }
    }

    protected override void OnLostTrack()
    {

    }
    protected override void OnDeath()
    {
        CancelInvoke("ObstacleCheck");
        animator.SetBool("Dead", true);
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
        GiveUpChasing();
    }

    public override void OnExitReactionRange(GameObject player)
    {
        Debug.Log("eexited reaction");
    }

    public override void OnEnterAttackTrigger(AttackDirection playerDirection)
    {
        this.playerDirection = playerDirection;
    }

    public override void OnExitAttackTrigger(AttackDirection playerDirection)
    {
        if (this.playerDirection == playerDirection)
        {
            this.playerDirection = AttackDirection.none;
        }
    }
}
