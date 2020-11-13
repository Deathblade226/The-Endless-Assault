using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DespawnCorpse : MonoBehaviour {

[SerializeField] PhotonView pv = null;
[SerializeField] float waitTime = 1f;
[SerializeField] Animator animator = null;
[SerializeField] GameObject model = null;
[SerializeField] GameObject particles = null;
[SerializeField] float deathDamage = 0;
[SerializeField] float deathDamageRange = 1;
[SerializeField] LayerMask targetLayers;

private float time = 0;
private bool despawn = false;
private bool damaged = false;

private void Start() {
	time = waitTime;		
}

private void Update() {
	despawn = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.5f);
	if (despawn && time > 0) time -= Time.deltaTime;
	if (time <= 0 && !particles.GetComponent<ParticleSystem>().isPlaying) { pv.RPC("Despawn", RpcTarget.All); } 
	else if (time/waitTime <= 0.5f && !particles.activeSelf) { 
	particles.SetActive(true);
	particles.GetComponent<ParticleSystem>().Play(true);
	model.SetActive(false);
	} else if (time/waitTime <= 0.25f && !damaged) { 
	damaged = true;
	Collider[] defenses = Physics.OverlapSphere(transform.position, deathDamageRange, targetLayers);
	foreach(Collider defense in defenses) { 
	//Debug.Log(defense.gameObject);
	if (defense.gameObject.GetComponent<PhotonView>() != null) defense.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, deathDamage);
	}

	}
}

[PunRPC]
public void Despawn() { 
	Destroy(gameObject);
}

}
