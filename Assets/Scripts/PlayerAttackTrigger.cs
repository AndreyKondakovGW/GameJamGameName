using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerAttackTrigger : MonoBehaviour
{
    public AttackDirection direction;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            player.OnEnemyEnterTrigger(collision.GetComponent<Enemy>(), direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            player.OnEnemyExitTrigger(collision.GetComponent<Enemy>(), direction);
        }
    }
}
