using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviourPun, IPunObservable {

[SerializeField]PhotonView pv;
[SerializeField]List<Wave> waves;

private int wave = 0;
private bool spawning = false;
private bool waitingToPay = false;

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	if(stream.IsWriting) {
	stream.SendNext(this.wave);
	stream.SendNext(this.spawning);
	} else {
	this.wave = (int)stream.ReceiveNext();
	this.spawning = (bool)stream.ReceiveNext();
	}
}
private void Update() {
	if (waitingToPay && GameObject.FindGameObjectWithTag("Monster") == null) {
	if (PhotonNetwork.IsMasterClient) Game.game.Pv.RPC("ModifyCurrency", RpcTarget.All, -waves[wave-1].EndwaveCurrency);
	waitingToPay = false;
	}		
}

[PunRPC] 
public void StartWave() { 
	spawning = (GameObject.FindGameObjectWithTag("Monster") != null || waves[wave].gameObject.activeSelf);
	if (wave < waves.Count && !spawning) { waves[wave].gameObject.SetActive(true); }
}
public void EndWave() {
	waitingToPay = true;
	wave++;
}

}
