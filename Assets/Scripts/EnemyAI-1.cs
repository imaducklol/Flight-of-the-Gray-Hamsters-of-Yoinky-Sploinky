using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Control : MonoBehaviour {
    /*Transform Player;
    Transform selfTransform;
    [SerializeField,Range(30,90)] int FOV;
    [SerializeField] int range;

    int viewDirection; // 0 north 1 west 2 south 3 east
    bool playerInSight = false;

    void Start() {
        selfTransform = GetComponent<Transform>();
    }

    void Update() {
        // range
        if (Vector2.Distance(Player.position, selfTransform.position) <= range) {
            // angle
            float angle = Vector2.SignedAngle(selfTransform.position, Player.position
            switch (angle) {
                case 
            }
        }
    }*/

    public float Radius;
    [Range(0,360)]
    public float FOV;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start() {
        StartCoroutine("FindTargets", .2f);
    }

    IEnumerator FindTargets(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        https://github.com/SebLague/Field-of-View/blob/master/Episode%2001/Scripts/FieldOfView.cs
    }
}