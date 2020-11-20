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
[SerializeField] GameObject particle;

public bool canHeal { get => (Physics.OverlapSphere(healPoint.position, healRange, layers).Length > 1); }

private bool healed = true;
private float healCD = 0;

void Update() {
    if (!particle.GetComponent<ParticleSystem>().isEmitting) particle.SetActive(false);
    if (!healed && healCD <= 0) { 
    particle.GetComponent<ParticleSystem>().Stop();
    healed = true;
    Collider[] colliders = Physics.OverlapSphere(healPoint.position, healRange, layers);
    particle.SetActive(true);
    if (colliders.Length > 0) { foreach (Collider collider in colliders) { collider.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, -healAmount); }  }
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
