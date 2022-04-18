using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


public class EnemyAIOne : MonoBehaviour {

    [SerializeField] private float Radius;
    // [Range(0,360)] public float FOV;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();
    private Transform target;
    private NavMeshAgent agent;


    void Start() {
        StartCoroutine("FindTargets", .2f);
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        // Color visible ones
        GameObject[] playerstomark = GameObject.FindGameObjectsWithTag("PlayerHelpers");
        foreach (var playerhelper in playerstomark) {
            if (visibleTargets.Contains(playerhelper.transform)) {
                playerhelper.GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                playerhelper.GetComponent<Renderer>().material.color = Color.red;
            }
        }

        // Get closest target
        Transform closest = visibleTargets[0];
        foreach (Transform item in visibleTargets) {
            if (Vector2.Distance(transform.position, item.position) 
            < Vector2.Distance(transform.position, closest.position)) {
                closest = item;
            }
        }
        // Targetting
        // DOES NOT WORK ---- IMPLEMENT A* https://www.youtube.com/watch?v=jvtFUfJ6CP8
        agent.SetDestination(closest.position);
    }

    IEnumerator FindTargets(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(transform.position, Radius, targetMask);

        for (int i = 0; i < targetsInRadius.Length; i++) {
            Transform target = targetsInRadius[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            float degreesToTarget = Mathf.Atan2(transform.position.y - target.transform.position.y, transform.position.x - target.transform.position.x) * Mathf.Rad2Deg;
            
            /*
            // In FOV?
            if (Mathf.DeltaAngle(transform.rotation.z + 180, Mathf.Abs(degreesToTarget)) < FOV / 2) { // NOT WORKING> FIX THIS SHIT
            */

            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            // View blocked?
            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
                visibleTargets.Add(target);
            }
            
        }
    }
}
