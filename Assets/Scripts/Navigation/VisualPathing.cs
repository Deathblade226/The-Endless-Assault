using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisualPathing : MonoBehaviour {

[SerializeField] Transform startingPoint;
[SerializeField] Transform target;
[SerializeField] TrailRenderer trail;
[SerializeField] NavMeshAgent agent = null;
[SerializeField] float resetTime = 1;
[SerializeField] bool hold = false;

private float resetTimeCD = 0;

public bool Hold { get => hold; set => hold =  value ; }

void Start() {
    transform.position = startingPoint.position;
    if (agent != null && agent.isOnNavMesh && gameObject.activeSelf && !Hold) { 
    agent.ResetPath();
    agent.SetDestination(target.position);
    } else if (Hold) { agent.isStopped = true; }
    trail.Clear();
}

void Update() {
    bool valid = ((target.transform.position - transform.position).magnitude < 1);
    if (agent != null && agent.isOnNavMesh && !Hold) {
    if (valid && trail.positionCount == 0) { 
    gameObject.transform.position = startingPoint.position;
    trail.Clear();
    agent.ResetPath();
    agent.SetDestination(target.position);
    }

    if (GameObject.FindGameObjectWithTag("Monster") != null) {
    transform.position = startingPoint.position;
    agent.ResetPath();
    agent.SetDestination(target.position);
    trail.Clear();
    agent.isStopped = true;
    } else {
    agent.isStopped = false;
    }
    }
}

}
