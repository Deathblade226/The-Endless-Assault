using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviourPun, IPunObservable {

[SerializeField]float spawnRange = 1;
[SerializeField]PhotonView pv;
[SerializeField]List<SpawnCluster> clusters;

private int spot = 0;
private float currentSpawnCD = 0;

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	if(stream.IsWriting) {
	stream.SendNext(this.spot);
	stream.SendNext(this.currentSpawnCD);
	} else {
	this.spot = (int)stream.ReceiveNext();
	this.currentSpawnCD = (float)stream.ReceiveNext();
	}
}

void Start() {
	//SpawnCluster();
}

void Update() {
	if (currentSpawnCD > 0) { currentSpawnCD -= Time.deltaTime; }
	else { }
}

private void SpawnCluster() {

	for (int i = 0; i < clusters[spot].Count; i++) {
	PhotonNetwork.InstantiateRoomObject(clusters[spot].Monster.gameObject.name, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y + 0.1f, transform.position.z + Random.Range(-spawnRange, spawnRange)), Quaternion.identity);
	}
	currentSpawnCD = clusters[spot].SpawnCD;
	spot++;
}

}
