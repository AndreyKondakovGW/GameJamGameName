using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public AttackDirection direction;
    private Enemy enemy;
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
            enemy.OnEnterAttackTrigger(direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            enemy.OnExitAttackTrigger(direction);
        }
    }
}
