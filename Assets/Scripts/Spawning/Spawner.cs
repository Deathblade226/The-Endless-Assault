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
public void Update() {
	if (waitingToPay && !spawning && GameObject.FindGameObjectWithTag("Monster") != null && wave < waves.Count-1 && !waves[wave].gameObject.activeSelf) {
	waitingToPay = false;
	Game.game.Pv.RPC("ModifyCurrency", RpcTarget.All, -waves[wave].EndwaveCurrency);		
	}
}

[PunRPC] 
public void StartWave() { 
	spawning = (GameObject.FindGameObjectWithTag("Monster") != null && waves[wave].gameObject.activeSelf);
	//Debug.Log($"{!spawning} | {!waves[wave].gameObject.activeSelf} | {wave}");
	if (wave < waves.Count) waves[wave].gameObject.SetActive(true);
	if (!spawning && !waves[wave].gameObject.activeSelf) wave++;
	waitingToPay = true;
}

}
