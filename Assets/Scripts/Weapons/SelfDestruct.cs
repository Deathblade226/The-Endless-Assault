using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : Weapon {

[SerializeField] float range = 1;
[SerializeField] GameObject particles = null;
[SerializeField] GameObject model = null;
[SerializeField] LayerMask layers;


public void Update() {
	if (!model.activeSelf && !particles.GetComponent<ParticleSystem>().isEmitting) {
	PhotonNetwork.Destroy(gameObject);
	}		
}

public void Explode() { 
	Collider[] colliders = Physics.OverlapSphere(transform.position, range, layers);
	if (colliders.Length < 0) { 
	foreach(Collider collider in colliders) { 
	collider.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, Damage);
	}
	GetComponent<NavigationControllerMP>().Agent.isStopped = true;
	particles.SetActive(true);
	particles.GetComponent<ParticleSystem>().Play();
	}
}

}
