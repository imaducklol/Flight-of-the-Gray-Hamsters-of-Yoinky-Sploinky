using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    

    public Rigidbody2D rb;
    private Animator animator;
    
    [SerializeField]
    private float moveSpeed = 5f;
    Vector2 movement;

    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool isAttacking;

    SpriteRenderer spriteRenderer;
    public Transform interactor;

    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed * Time.deltaTime;
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
            rb.velocity = Vector2.zero;
            attackCounter -=Time.deltaTime;
            if(attackCounter <= 0)
            {
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
