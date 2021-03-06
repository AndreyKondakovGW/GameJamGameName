﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
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
    bool isFlanking = false;

    bool[] AttackTriggers;

    Shader shaderGUItext;
    //Shader shaderSpritesDefault;

    public Material  DefaltMatrial;

    GameObject chasing;
    Transform homePoint;
    MiniHPBar miniHPBar;

    Pathfinder pathfinder;

    LinkedListNode<PathNode> currentNode;

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
        //Debug.Log(homePoint.name);
        homePoint.parent = null;
        shaderGUItext = Shader.Find("GUI/Text Shader");
        //shaderSpritesDefault = Shader.Find("Sprites/Default");

        AttackTriggers = new bool[4];
        pathfinder = new Pathfinder();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            if (collision.transform.position.y > transform.position.y)
            {
                sr.sortingOrder = 11;
            }
            else
            {
                sr.sortingOrder = 9;
            }
        }
    }

    void UpdateAnims()
    {
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    protected override void UpdateAI()
    {
        if (isChasing && !isAttacking)
        {
            //Debug.Log("CHASING" + chasing.transform.position + " " + chasing.name);
            Vector2 diff = chasing.transform.position - transform.position;
            direction = diff.x > 0 ? AttackDirection.right : AttackDirection.left;
            float magnitude = diff.magnitude;
            if (magnitude < attackRange && AttackTriggers.Any(x => x))
            {
                //Debug.Log("ATTACK" + chasing.transform.position + " " + chasing.name);
                isAttacking = true;
                animator.SetFloat("Horizontal", (int)direction * 2 - 1);
                animator.SetBool("Attacking", true);
                Invoke("AttackPlayer", attackDelay);
                Invoke("ResetAttacking", attackDuration);
                movementDirection = Vector2.zero;
            }
            else
            {
                diff = new Vector2(diff.x, diff.y * 4);
                movementDirection = (diff).normalized;
                UpdateAnims();
            }
        }
        else if (isFlanking)
        {
            if (currentNode == null)
            {
                //Invoke("GoHome", 3.0f);
                Debug.Log("Flank Again");
                StartFlanking();
                
                return;
            }
            Vector2 diff = new Vector2(currentNode.Value.pos.x - transform.position.x, currentNode.Value.pos.y - transform.position.y);
            if (diff.sqrMagnitude < 0.1)
            {
                if (currentNode.Next == null)
                {
                    //Invoke("GoHome", 3.0f);
                    Debug.Log("Flank Again");
                    StartFlanking();
                }
                currentNode = currentNode.Next;
                return;
            }
            movementDirection = diff.normalized;
            UpdateAnims();
        }
        else if (isOnWayHome)
        {
            if (currentNode == null)
            {
                ArrivedHome();
                return;
            }
            Vector2 diff = new Vector2(currentNode.Value.pos.x - transform.position.x, currentNode.Value.pos.y - transform.position.y);
            if (diff.sqrMagnitude < 0.1)
            {
                if (currentNode.Next == null)
                {
                    ArrivedHome();
                }
                currentNode = currentNode.Next;
                return;
            }
            movementDirection = diff.normalized / 2;
            UpdateAnims();
        }
        else
        {
            movementDirection = Vector2.zero;
            UpdateAnims();
        }
    }

    bool SeesObstacle()
    {
        RaycastHit2D[] result = Physics2D.LinecastAll(chasing.transform.position, transform.position);
        //return result.Any(x => x.transform.tag == "Walls");
        /*foreach (RaycastHit2D rc in result)
        {
            Debug.Log(((rc.collider.isTrigger == false) && (rc.transform.tag != "Enemy") && (rc.transform.tag != "Player")) + " " + rc.transform.tag + " " + rc.transform.name);
        }*/
        return result.Any(x => ((x.collider.isTrigger == false) && (x.transform.tag != "Enemy") && (x.transform.tag != "Player")));
    }

    void ResetAttacking()
    {
        isAttacking = false;
        animator.SetBool("Attacking", false);
    }

    void AttackPlayer()
    {
        if (chasing != null && AttackTriggers[(int)direction])
        {
            chasing.GetComponent<PlayerController>().OnHit(this, damage);
        }
    }

    void ObstacleCheck()
    {
        //Debug.Log("OBSTACLE CHECK " + isChasing + " " + chasing.transform.position);
        if (SeesObstacle() && !isFlanking)
        { 
            //Debug.Log("WALL " + isChasing);
            GiveUpChasing();
        }
    }

    void GiveUpChasing()
    {
        isChasing = false;
        CancelInvoke("ObstacleCheck");
        movementDirection = Vector2.zero;
        animator.SetFloat("Speed", 0.0f);
        StartFlanking();
    }

    LinkedList<PathNode> GetPath(Vector2 pos1, Vector2 pos2)
    {
        float new_x = Mathf.Floor(pos1.x) + 0.5f;
        float new_y = Mathf.Floor(pos1.y) + 0.5f;
        PathNode start = new PathNode(new Vector2(new_x, new_y));
        new_x = Mathf.Floor(pos2.x) + 0.5f;
        new_y = Mathf.Floor(pos2.y) + 0.5f;
        PathNode end = new PathNode(new Vector2(new_x, new_y));
        return pathfinder.findPath(start, end);
    }

    void StartFlanking()
    {

        LinkedList<PathNode> plist = GetPath(transform.position, chasing.transform.position);
        //Debug.Log("HERE");
        if (plist != null && plist.Count != 0)
        {
            if (plist.Count > 25)
            {
                Invoke("GoHome", 3.0f);
                return;
            }

            currentNode = plist.First;
            isFlanking = true;
        }
        else
        {
            Invoke("GoHome", 3.0f);
        }
    }

    void GoHome()
    {
        isFlanking = false;
        isChasing = false;
        animator.SetFloat("Speed", 0.0f);
        Vector2 dist = homePoint.position - transform.position;
        if (dist.magnitude < 10)
        {
            isOnWayHome = true;
            LinkedList<PathNode> plist = GetPath(transform.position, homePoint.position);
            if (plist != null)
            {
                currentNode = plist.First;
            }
        }
        
    }

    void ArrivedHome()
    {
        isOnWayHome = false;
        animator.SetFloat("Speed", 0.0f);
    }

    void RestoreShader()
    {
        sr.material = DefaltMatrial;
    }

    public override void OnHit(GameObject player, float Damage)
    {
        if (!isDead)
        {
            health -= Damage;
            miniHPBar.UpdateHPByRatio(health / maxHealth);
            sr.material.shader = shaderGUItext;
            Invoke("RestoreShader", 0.2f);
            if (health <= 0)
            {
                isDead = true;
                OnDeath();
            }
        }
    }

    protected override void OnLostTrack()
    {

    }
    protected override void OnDeath()
    {
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().RestoreHP(GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().RestorationperEnemy);
        CancelInvoke("ObstacleCheck");
        CancelInvoke("GoHome");
        animator.SetBool("Dead", true);
        rb.velocity = Vector2.zero;

    }

    void StartChasing(GameObject player)
    {
        isChasing = true;
        isFlanking = false;
        isOnWayHome = false;
        CancelInvoke("GoHome");
        InvokeRepeating("ObstacleCheck", 0.0f, 0.5f);
    }

    public override void OnEnterWarningRange(GameObject player)
    {
        if (!isDead)
        {

        }
        //Debug.Log("entered warning");
    }
    public override void OnEnterReactionRange(GameObject player)
    {
        if (!isDead)
        {
            //Debug.Log("entered reaction");
            chasing = player;
            if (!SeesObstacle())
            {
                //Debug.Log("start chasing");
                StartChasing(player);
            }
        }
    }

    public override void OnStayReactionRange(GameObject player)
    {
        if (!isDead && !isChasing && !SeesObstacle())
        {
            StartChasing(player);
        }
    }

    public override void OnExitWarningRange(GameObject player)
    {
        if (!isDead)
        {
            GoHome();
        }
    }

    public override void OnExitReactionRange(GameObject player)
    {
        //Debug.Log("eexited reaction");
    }

    public override void OnEnterAttackTrigger(AttackDirection playerDirection)
    {
        if (!isDead)
        {
            AttackTriggers[(int)playerDirection] = true;
        }
    }

    public override void OnExitAttackTrigger(AttackDirection playerDirection)
    {
        if (!isDead)
        {
            AttackTriggers[(int)playerDirection] = false;
        }
    }
}
