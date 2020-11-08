using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelNavMP : MonoBehaviourPun, IPunObservable {

[SerializeField] string targetTag = "";
[SerializeField] NavigationControllerMP nc = null;

public bool Moving { get; set; }
public string TargetTag { get => targetTag; set => targetTag = value; }
public NavigationControllerMP Nc { get => nc; set => nc = value; }
public Vector3 Target { get; set; }

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.Moving);
	} else {
	this.Moving = (bool) stream.ReceiveNext();
	}
}
public void StartTravel() {
    this.Moving = true; 
	if (this.Target == new Vector3()) Target = AIUtilities.GetNearestGameObject(this.gameObject, this.TargetTag, xray:true).transform.position;
}

public void Update() {
	if (nc.Agent.isOnNavMesh && Moving && Target != new Vector3() && nc.Agent.destination != Target) {
	nc.Agent.SetDestination(Target);
	//Debug.Log($"{gameObject.name} | {nc.Agent.isOnNavMesh}");
	}
}

}
