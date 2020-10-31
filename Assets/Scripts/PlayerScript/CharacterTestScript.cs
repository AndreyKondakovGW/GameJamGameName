using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTestScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Sprite Front;
    public Sprite Right;
    public Sprite Left;

    Vector2 move;

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {   
        if (move.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Right;
        }
        else if (move.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Left;
        }
        if (move.x == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Front;
        }
        
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
