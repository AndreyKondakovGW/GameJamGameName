using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Enemy enemy;
    public bool IsOuter;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            if (IsOuter)
            {
                enemy.OnEnterWarningRange(collision.gameObject);
            }
            else
            {
                enemy.OnEnterReactionRange(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsOuter)
        {
            enemy.OnStayReactionRange(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            if (IsOuter)
            {
                enemy.OnExitWarningRange(collision.gameObject);
            }
            else
            {
                enemy.OnExitReactionRange(collision.gameObject);
            }
        }
    }
}
