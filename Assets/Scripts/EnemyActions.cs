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
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float attackRate;

    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();

    void Attack(Transform toAttack) {
        toAttack.GameObject.GetComponent<PlayerActions>().TakeDamage(damage);
    }

    void TakeDamage(int damage) {
        damage -= defense;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
    }
    
    void Start() {
        StartCoroutine("FindTargets", .2f);
    }

    void Update()
    {
        if (!visibleTargets.length == 0) {
            foreach (Transform toAttack in visibleTargets) {
                Attack(toAttack);
            }
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