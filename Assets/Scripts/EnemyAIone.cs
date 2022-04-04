using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


//         https://github.com/SebLague/Field-of-View/blob/master/Episode%2001/Scripts/FieldOfView.cs
//         https://www.youtube.com/watch?v=G1yAkfdlDtM
public class EnemyAIOne : MonoBehaviour {

    public float Radius;
    [Range(0,360)] public float FOV;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start() {
        StartCoroutine("FindTargets", .2f);
    }

    void Update() {
        GameObject[] playerstomark = GameObject.FindGameObjectsWithTag("PlayerHelpers");
        foreach (var playerhelper in playerstomark) {
            if (visibleTargets.Contains(playerhelper.transform)) {
                playerhelper.GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                playerhelper.GetComponent<Renderer>().material.color = Color.red;
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
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(transform.position, Radius, targetMask);

        for (int i = 0; i < targetsInRadius.Length; i++) {
            Transform target = targetsInRadius[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            float degreesToTarget = Mathf.Atan2(transform.position.y - target.transform.position.y, transform.position.x - target.transform.position.x) * Mathf.Rad2Deg;

            // In FOV?
            if (Mathf.DeltaAngle(transform.rotation.z + 180, Mathf.Abs(degreesToTarget)) < FOV / 2) { // NOT WORKING> FIX THIS SHIT
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                // View blocked?
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
                    visibleTargets.Add(target);
                }
            }
        }
    }
}
