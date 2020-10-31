using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float attackDelay = 0.1f;

    bool isAttacking = false;

    AttackDirection direction = AttackDirection.down;

    HashSet<Enemy>[] enemiesInTriggers;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerStats playerStats;

    public SpriteRenderer sr;

    Shader shaderGUItext;
    Shader shaderSpritesDefault;

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

        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");

    }

    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        if (movementDirection != Vector2.zero)
        {
            direction = AttackDirections.ClosestDirection(movementDirection);
            Debug.Log(direction + " " + movementDirection);
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        if (Input.GetKeyDown("space"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
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
                enemy.OnHit(gameObject, 20.0f);
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
        playerStats.HP = playerStats.HP - damage;
        sr.material.shader = shaderGUItext;
        Invoke("RestoreShader", 0.2f);
    }

    void RestoreShader()
    {
        sr.material.shader = shaderSpritesDefault;
    }
}
