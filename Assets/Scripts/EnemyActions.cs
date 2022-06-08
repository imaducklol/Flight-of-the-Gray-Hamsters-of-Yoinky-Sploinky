using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int defense;
    [SerializeField] private int damage;
    [SerializeField] private float Range;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float attackTime = .25f;
    private float attackCounter = .25f;

    public Animator animator;
    private Vector2 deltaPos;


    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();

    void Attack(Transform toAttack) {
        Debug.Log("attackin");
        toAttack.gameObject.GetComponent<PlayerActions>().TakeDamage(damage);
        Debug.Log(toAttack);
    }

    public void TakeDamage(int damage) {
        damage -= defense;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
    }
    
    void Start() {
        StartCoroutine("FindTargets", .2f);
    }

    void Update()
    {
        deltaPos =target.position - transform.position;
        //(float)(deltaPos.normalized.x) * .1f
        //(float)(deltaPos.normalized.y) * .1f
        animator.SetBool("withinRange", true);
        animator.SetFloat("Horizontal", deltaPos.x);
        animator.SetFloat("Vertical",   deltaPos.y);
        

        if (visibleTargets.Count != 0 ) {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0) {
                attackCounter = attackTime;
                foreach (Transform toAttack in visibleTargets) {
                    Attack(toAttack);
                }
            }
        }
        if (currentHealth < 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator FindTargets(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(transform.position, Range, targetMask);
        
        /*for (int i = 0; i < targetsInRadius.Length; i++) {
            visibleTargets.Add(targetsInRadius[i].transform);
        }*/

        
        for (int i = 0; i < targetsInRadius.Length; i++) {
            Transform target = targetsInRadius[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            //float degreesToTarget = Mathf.Atan2(transform.position.y - target.transform.position.y, transform.position.x - target.transform.position.x) * Mathf.Rad2Deg;
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            // View blocked?
            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
                visibleTargets.Add(target);
            }
            
        }
    }
}