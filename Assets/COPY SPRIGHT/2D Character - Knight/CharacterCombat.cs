using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
// Update is called once per frame

public Animator animator;
    void Update()
    {
       if (Input.GetMouseButtonDown(0)) 
       {
           Attack();
       }
    }

    void Attack()
    {

        animator.SetTrigger("Attack");
    }
}
