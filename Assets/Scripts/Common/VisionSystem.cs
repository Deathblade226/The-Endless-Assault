using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

[RequireComponent(typeof(SphereCollider))]
public class VisionSystem : MonoBehaviour {

[SerializeField][Range(0,360)] float fieldOfViewAngle = 180f;
[SerializeField] float visionRange = 10f;
[SerializeField] string target;
[SerializeField] SphereCollider visionTrigger = null;

private GameObject seenTarget = null;

public GameObject SeenTarget { get => seenTarget; set => seenTarget =  value ; }
public float Distance { get => (seenTarget.transform.position - transform.position).magnitude; }

private void Awake() {
    visionTrigger.radius = visionRange;		
}

private void Update() {
    //Debug.Log(SeenTarget);		
}

private void OnTriggerStay(Collider other) {
    bool valid = true;
    Vector3 targetDir = other.transform.position - transform.position;
    float angleDif = Vector3.Angle(targetDir, transform.forward);
    if (other.tag == target) {
    if (Physics.Raycast(transform.position, targetDir.normalized, out RaycastHit hit, visionRange)) { valid = hit.collider.gameObject.tag == target; }
    
    //( transform.position - seenTarget.transform.position).sqrMagnitude > (transform.position - other.transform.position).sqrMagnitude
    float distanceSeen = SeenTarget == null ? float.MaxValue : ( transform.position - seenTarget.transform.position ).sqrMagnitude;
    float distanceNew = (transform.position - other.transform.position).sqrMagnitude;

    if (valid && distanceSeen > distanceNew && angleDif <= fieldOfViewAngle) { 
    SeenTarget = other.gameObject;
    }
    }
}

private void OnDrawGizmos() {
    float halfFOV = fieldOfViewAngle / 2.0f;
    Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFOV, Vector3.up );
    Quaternion rightRayRotation = Quaternion.AngleAxis( halfFOV, Vector3.up );
    Vector3 leftRayDirection = leftRayRotation * transform.forward;
    Vector3 rightRayDirection = rightRayRotation * transform.forward;
    Gizmos.DrawRay( transform.position, leftRayDirection * visionRange);
    Gizmos.DrawRay( transform.position, rightRayDirection * visionRange);
    Gizmos.DrawWireSphere(transform.position, visionRange);
}

}
