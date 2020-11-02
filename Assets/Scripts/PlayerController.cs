using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float attackDelay = 0.1f;

    bool isAttacking = false;

    AttackDirection direction = AttackDirection.down;

    HashSet<Enemy>[] enemiesInTriggers;

    public Sprite Front;
    public Sprite Right;
    public Sprite Left;
    public Sprite Back;
    
    public Material  DefaltMatrial;

    public Rigidbody2D rb;
    //public Animator animator;
    public PlayerStats playerStats;

    public SpriteRenderer sr;

    Shader shaderGUItext;
    //Shader shaderSpritesDefault;

    public Animator animator;

    Vector2 movementDirection;

    void Start()
    {
        enemiesInTriggers = new HashSet<Enemy>[4];
        for (int i = 0; i < 4; i++)
        {
            enemiesInTriggers[i] = new HashSet<Enemy>();
        }
        if (!playerStats)
        {
            playerStats = GetComponent<PlayerStats>();
        }
        if (!sr)
        {
            sr = transform.Find("sprite").GetComponent<SpriteRenderer>();
        }
        if (!animator)
        {
            animator = transform.Find("sprite").GetComponent<Animator>();
        }

        shaderGUItext = Shader.Find("GUI/Text Shader");
        //shaderSpritesDefault = Shader.Find("Sprites/Default");

    }

    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        if (movementDirection != Vector2.zero)
        {
            direction = AttackDirections.ClosestDirection(movementDirection);
            //Debug.Log(direction + " " + movementDirection);

            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        //animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        if (Input.GetKeyDown("space"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        /*if ( movementDirection.x > 0)
        {
            transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = Right;
        }
        else if ( movementDirection.x < 0)
        {
            transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = Left;
        }
        if ( movementDirection.x == 0)
        {
            if ( movementDirection.y > 0 )
            {
                transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = Back;
            }
            else
            {
                transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = Front;
            }
            
        }*/

        rb.velocity = movementDirection * movementSpeed;
    }

    void Attack()
    {
        if (direction != AttackDirection.none && !isAttacking)
        {
            isAttacking = true;
            foreach (Enemy enemy in enemiesInTriggers[(int)direction])
            {
                //TODO: insert damage calculations
                enemy.OnHit(gameObject,playerStats.Damage * (1 + playerStats.Srength / 100));
            }
            Invoke("ReleaseAttack", attackDelay);
        }
    }

    void ReleaseAttack()
    {
        isAttacking = false;
    }

    public void OnEnemyEnterTrigger(Enemy enemy, AttackDirection direction)
    {
        enemiesInTriggers[(int)direction].Add(enemy);
    }

    public void OnEnemyExitTrigger(Enemy enemy, AttackDirection direction)
    {
        enemiesInTriggers[(int)direction].Remove(enemy);
    }

    public void OnHit(Enemy enemy, float damage)
    {
        playerStats.HP = playerStats.HP - damage * Math.Max(0.5f, 1 - playerStats.Agility / 100);
        if (playerStats.HP < 0)
        {
            playerStats.HP = 0f;
            StartCoroutine(GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControler>().EndGame());
        }
        sr.material.shader = shaderGUItext;
        Invoke("RestoreShader", 0.2f);
    }

    void RestoreShader()
    {
        sr.material = DefaltMatrial;//shader = shaderSpritesDefault;
    }
}
