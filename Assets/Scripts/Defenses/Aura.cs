using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using Photon.Pun;

public class Aura : Defense, IPunObservable {

[SerializeField] float damage = 1;
[SerializeField] float damageRange = 1;
[SerializeField] float damageRate = 1;
[SerializeField] PhotonView pv;
[SerializeField] ParticleSystem particle;
[SerializeField] LayerMask layers;
[SerializeField] LayerMask ignoredLayers;

private float damageCD = 0;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.damageCD);
	} else {
	this.damageCD = (float)stream.ReceiveNext();
	}
}

void Update() {
    if (particle != null) particle.startSize = damageRange * 2;
    if (damageCD <= 0 && Physics.CheckSphere(transform.position, damageRange, ignoredLayers)) {
    Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange, layers);    
    if (colliders.Length != 0) { 
    foreach (Collider collider in colliders) {
    collider.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, damage);
    }
    pv.RPC("ApplyDamage", RpcTarget.All, 1f);
    }
    damageCD = damageRate;
	} else if (damageCD > 0) { damageCD -= Time.deltaTime; }
}

private void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, damageRange);
}

}
