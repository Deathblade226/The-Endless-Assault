﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviourPun, IPunObservable {

[SerializeField]PhotonView pv;
[SerializeField]List<Wave> waves;

private int wave = 0;
private bool spawning = false;
private bool waitingToPay = false;

public int Wave { get => wave; set => wave =  value ; }
public List<Wave> Waves { get => waves; set => waves =  value ; }

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	if(stream.IsWriting) {
	stream.SendNext(this.Wave);
	stream.SendNext(this.spawning);
	} else {
	this.Wave = (int)stream.ReceiveNext();
	this.spawning = (bool)stream.ReceiveNext();
	}
}
private void Update() {
	if (waitingToPay && GameObject.FindGameObjectWithTag("Monster") == null) {
	if (PhotonNetwork.IsMasterClient) Game.game.Pv.RPC("ModifyCurrency", RpcTarget.All, -Waves[Wave-1].EndwaveCurrency);
	waitingToPay = false;
	}		
}

[PunRPC] 
public void StartWave() { 
	spawning = (GameObject.FindGameObjectWithTag("Monster") != null || Waves[Wave].gameObject.activeSelf);
	if (Wave < Waves.Count && !spawning) { Waves[Wave].gameObject.SetActive(true); }
}
public void EndWave() {
	waitingToPay = true;
	Wave++;
}

}
