using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelNavMP : MonoBehaviourPun, IPunObservable {

[SerializeField] string targetTag = "";
[SerializeField] NavigationControllerMP nc = null;

public bool Moving { get; set; }
public string TargetTag { get => targetTag; set => targetTag = value; }
public NavigationControllerMP Nc { get => nc; set => nc = value; }
public GameObject Target { get => target; set => target = value; }

private GameObject target = null;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.Target);
	stream.SendNext(this.Moving);
	} else {
	this.Target = (GameObject) stream.ReceiveNext();
	this.Moving = (bool) stream.ReceiveNext();
	}
}
public void StartTravel() {
    if (this.Target == null) this.Target = AIUtilities.GetNearestGameObject(this.gameObject, this.TargetTag, xray:true);
    this.Moving = true; 
    this.nc.Agent.SetDestination(this.Target.transform.position);
}

}
