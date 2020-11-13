using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealWeapon : Weapon {

[SerializeField] float animationWait = 0;
[SerializeField] float healAmount = 0;
[SerializeField] float healRange = 0;
[SerializeField] LayerMask layers;
[SerializeField] Transform healPoint;

private bool healed = true;
private float healCD = 0;

void Update() {
    if (!healed && healCD <= 0) { 
    healed = true;
    Collider[] colliders = Physics.OverlapSphere(healPoint.position, healRange, layers);
    foreach(Collider collider in colliders) { 
    collider.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, -healAmount);
    }
    } else if (healCD > 0) { healCD -= Time.deltaTime; }
}

public void Heal() {
    healed = false;
    healCD = animationWait;
}
private void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(healPoint.position, healRange);
}

}
