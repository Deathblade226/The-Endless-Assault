using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class VisionSystem : MonoBehaviour {

[SerializeField] float fieldOfViewAngle = 180f;
[SerializeField] float visionRange = 10f;
[SerializeField] string target;
[SerializeField] SphereCollider visionTrigger = null;

private GameObject seenTarget = null;

public GameObject SeenTarget { get => seenTarget; set => seenTarget =  value ; }

private void Awake() {
    visionTrigger.radius = visionRange;		
}

private void OnTriggerStay(Collider other) {
    bool valid = true;
    Vector3 targetDir = other.transform.position - transform.position;
    float angleDif = Vector3.Angle(targetDir, transform.forward);
    if (other.tag == target) {
    if (Physics.Raycast(transform.position, targetDir.normalized, out RaycastHit hit, visionRange)) { valid = hit.collider.gameObject.tag == target; }
    if (seenTarget != null && valid && ( transform.position - seenTarget.transform.position).sqrMagnitude > (transform.position - other.transform.position).sqrMagnitude && angleDif <= fieldOfViewAngle) { 
    SeenTarget = other.gameObject;
    }
    }
}

}
