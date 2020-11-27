using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAura : Defense, IPunObservable {

[SerializeField] float slow = 1;
[SerializeField] float slowRange = 1;
[SerializeField] float slowRate = 1;
[SerializeField] PhotonView pv;
[SerializeField] ParticleSystem particle;
[SerializeField] LayerMask layers;
[SerializeField] LayerMask ignoredLayers;

private float activateCD = 0;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.activateCD);
	} else {
	this.activateCD = (float)stream.ReceiveNext();
	}
}

void Update() {
	if (particle != null) particle.startSize = slowRange * 2;
    if (activateCD <= 0 && Physics.CheckSphere(transform.position, slowRange, ignoredLayers)) {
    Collider[] colliders = Physics.OverlapSphere(transform.position, slowRange, layers);    
    if (colliders.Length != 0) { 
    foreach (Collider collider in colliders) {
    collider.gameObject.GetComponent<PhotonView>().RPC("SetSlow", RpcTarget.All, slow);
    }
    pv.RPC("ApplyDamage", RpcTarget.All, 1);
    }
    activateCD = slowRate;
	} else if (activateCD > 0) { activateCD -= Time.deltaTime; }
}

private void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, slowRange);
}

}
