using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathingDebug : MonoBehaviour {
[SerializeField] NavMeshAgent agent;
private LineRenderer lineRenderer;

private void Awake() {
    agent = gameObject.GetComponent<NavMeshAgent>();            
}

void Start() {
    lineRenderer = GetComponent<LineRenderer>();
}

void Update() {
    if (agent.hasPath) { 
    lineRenderer.positionCount = agent.path.corners.Length;
    lineRenderer.SetPositions(agent.path.corners);
    lineRenderer.enabled = true;
    } else { lineRenderer.enabled = false; }
}

}
