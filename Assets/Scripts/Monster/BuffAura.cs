using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAura : MonoBehaviour {

[SerializeField] float buffAmount = 0;
[SerializeField] float buffRange = 0;
[SerializeField] LayerMask layers = 0;

void Update() {
    Collider[] colliders = Physics.OverlapSphere(transform.position, buffRange, layers);
    if (colliders.Length > 0) { 
    foreach(Collider collider in colliders) {
    if (collider.gameObject != gameObject) { 
    Weapon weapon = collider.GetComponentInChildren<Weapon>();
    if (weapon.BuffDamage < buffAmount) { 
    weapon.BuffDamage = buffAmount; 
    weapon.BuffDamageCd = 5;
    }
    }
    }
    }
}

private void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, buffRange);
}

}
