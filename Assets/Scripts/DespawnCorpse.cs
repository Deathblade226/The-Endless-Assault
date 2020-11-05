using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DespawnCorpse : MonoBehaviour {

[SerializeField] PhotonView pv = null;
[SerializeField] float waitTime = 1f;
[SerializeField] GameObject model = null;
[SerializeField] GameObject particles = null;

private float time = 0;
private bool despawn = false;

private void Start() {
	time = waitTime;		
}

private void Update() {
	if (despawn && time > 0) time -= Time.deltaTime;
	if (time <= 0) { pv.RPC("Despawn", RpcTarget.All); } 
	else if (time/waitTime <= 0.5f) { 
	particles.SetActive(true);
	particles.GetComponent<ParticleSystem>().Play(true);
	model.SetActive(false);
	}
}

public void StartDespawnTimer() {
	despawn = true;
}

[PunRPC]
public void Despawn() { 
	Destroy(gameObject);
}

}
