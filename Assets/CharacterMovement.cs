using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool isAttacking;

    SpriteRenderer spriteRenderer;
    public Transform interactor;

    void Start()
    {
    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical"); 

       animator.SetFloat("Horizontal", movement.x);
       animator.SetFloat("Vertical", movement.y);
       animator.SetFloat("Speed", movement.sqrMagnitude);

       if(Input.GetAxisRaw("Horizontal")==1 || Input.GetAxisRaw("Horizontal")== -1 || Input.GetAxisRaw("Vertical")==1 || Input.GetAxisRaw("Vertical")==-1)
       {
           animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
           animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
       }
      

       if(isAttacking)
       {
            attackCounter -=Time.deltaTime;
            if(attackCounter <= 0)
            {
                gameObject.GetComponent<PlayerActions>().Attack();
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
       }

        if (Input.GetMouseButtonDown(0)){
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
