using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

[RequireComponent(typeof(SphereCollider))]
public class VisionSystem : MonoBehaviour {

[SerializeField][Range(0,360)] float fieldOfViewAngle = 180f;
[SerializeField][Range(0,50)] float visionRange = 10f;
[SerializeField] List<string> targets;
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
    if (targets.Contains(other.tag)) {
    if (Physics.Raycast(transform.position, targetDir.normalized, out RaycastHit hit, visionRange)) { valid = targets.Contains(hit.collider.gameObject.tag); }
    
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
    Vector3 up = transform.up;
    Gizmos.DrawRay(transform.position + up, (leftRayDirection * visionRange));
    Gizmos.DrawRay(transform.position + up, (rightRayDirection * visionRange));
    DrawEllipse(transform.position + up, transform.forward, transform.up, visionRange, visionRange, 100, Gizmos.color);
    //Gizmos.DrawWireSphere(transform.position + up, visionRange);
}

private void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0) {
    float angle = 0f;
    Quaternion rot = Quaternion.LookRotation(forward, up);
    Vector3 lastPoint = Vector3.zero;
    Vector3 thisPoint = Vector3.zero;
    for (int i = 0; i < segments + 1; i++) {
    thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
    thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;
    if (i > 0) {
    Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
    }
    lastPoint = thisPoint;
    angle += 360f / segments;
    }
}

}
