using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using Photon.Pun;

public class Aura : MonoBehaviour {

[SerializeField] float damage = 1;
[SerializeField] float damageRange = 1;
[SerializeField] float damageRate = 1;
[SerializeField] Damagable damagable;
[SerializeField] PhotonView pv;
[SerializeField] LayerMask layers;
[SerializeField] LayerMask ignoredLayers;

private float damageCD = 0;

void Update() {
    if (damageCD <= 0 && Physics.CheckSphere(transform.position, damageRange, ignoredLayers)) {
    Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange, layers);    
    foreach (Collider collider in colliders) {
    collider.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, damage);
    pv.RPC("ApplyDamage", RpcTarget.All, 1);
    }
    damageCD = damageRate;
	} else if (damageCD > 0) { damageCD -= Time.deltaTime; }
}

private void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, damageRange);
}

}
