﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisualPathing : MonoBehaviour {

[SerializeField] Transform startingPoint;
[SerializeField] Transform target;
[SerializeField] TrailRenderer trail;
[SerializeField] NavMeshAgent agent = null;
[SerializeField] float resetTime = 1;


private float resetTimeCD = 0;

void Start() {
    transform.position = startingPoint.position;
    agent.SetDestination(target.position);
    trail.Clear();
}

void Update() {
    bool valid = ( transform.position.z == target.position.z && transform.position.x == target.position.x );
    if (valid && trail.positionCount == 0) { 
    gameObject.transform.position = startingPoint.position;
    trail.Clear();
    agent.SetDestination(target.position);
    }

    if (GameObject.FindGameObjectWithTag("Monster") != null) {
    transform.position = startingPoint.position;
    agent.SetDestination(target.position);
    trail.Clear();
    agent.isStopped = true;
    } else {
    agent.isStopped = false;
    }
}

}
