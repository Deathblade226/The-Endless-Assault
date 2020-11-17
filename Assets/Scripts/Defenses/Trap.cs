using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Defense, IPunObservable {

[SerializeField] float cooldown = 0;
[SerializeField] float damage = 0;
[SerializeField] float damageRange = 0;
[SerializeField] LayerMask layers;
[SerializeField] PhotonView pv;
[SerializeField] ParticleSystem particle;
[SerializeField] List<string> tags;

private float resetCd = 0;

public void Start() {
	particle.Stop();		
}

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.resetCd);
	} else {
	this.resetCd = (float)stream.ReceiveNext();
	}
}
void Update() { if (resetCd > 0) resetCd -= Time.deltaTime; }

private void OnTriggerEnter(Collider other) {
	if (resetCd <= 0 && tags.Contains(other.tag)) { 
	resetCd = cooldown;
	Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange, layers);
	foreach(Collider collider in colliders) { collider.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, damage); }
	pv.RPC("ApplyDamage", RpcTarget.All, 1);
	if (particle != null) particle.Play();
	}
}

}
