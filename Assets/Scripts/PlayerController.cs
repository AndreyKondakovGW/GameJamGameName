using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movementDirection;

    void Start()
    {
        
    }

    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }
}
