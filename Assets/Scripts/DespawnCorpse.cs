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

private float time = 0;
private bool despawn = false;

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
	}
}

[PunRPC]
public void Despawn() { 
	Destroy(gameObject);
}

}
