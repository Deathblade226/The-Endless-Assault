using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave : MonoBehaviourPun, IPunObservable {

[SerializeField]float spawnRange = 1;
[SerializeField]int endwaveCurrency = 0;
[SerializeField]Spawner spawner = null;
[SerializeField]List<SpawnCluster> clusters;

private int spot = 0;
private float currentSpawnCD = 0;

public int EndwaveCurrency { get => endwaveCurrency; set => endwaveCurrency =  value ; }

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	if(stream.IsWriting) {
	stream.SendNext(this.spot);
	stream.SendNext(this.currentSpawnCD);
	} else {
	this.spot = (int)stream.ReceiveNext();
	this.currentSpawnCD = (float)stream.ReceiveNext();
	}
}

void Update() {
	if (spot == clusters.Count && GameObject.FindGameObjectsWithTag("Monster").Length == 0) {
	spawner.EndWave();
	gameObject.SetActive(false); 	
	} else if (currentSpawnCD > 0 && spot != clusters.Count) { currentSpawnCD -= Time.deltaTime; }
	else if (currentSpawnCD <= 0 && PhotonNetwork.IsMasterClient && spot != clusters.Count) {  

	for (int i = 0; i < clusters[spot].Count; i++) {
	float rX = Random.Range(-spawnRange, spawnRange);
	float rZ = Random.Range(-spawnRange, spawnRange);
	string name = clusters[spot].Monster.gameObject.name;
	Vector3 spawn = new Vector3(transform.position.x + rX, transform.position.y, transform.position.z + rZ);
	PhotonNetwork.InstantiateRoomObject(name, spawn, Quaternion.identity);
	}
	currentSpawnCD = clusters[spot].SpawnCD;
	spot++;
	}
}
private void OnDrawGizmos() {
	Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange, spawnRange, spawnRange)); 
}
}
