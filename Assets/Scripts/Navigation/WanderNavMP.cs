using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNavMP : MonoBehaviourPun, IPunObservable {

[SerializeField] float MoveCd = 5.0f;
[SerializeField] float searchDistance = 5.0f;
[SerializeField] NavigationControllerMP nc = null;
public bool Active { get; set; } = false;
public NavigationControllerMP Nc { get => nc; set => nc = value; }

private float MoveTime = 0;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.Active);
	stream.SendNext(this.MoveTime);
	} else {
	this.Active = (bool) stream.ReceiveNext();
	this.MoveTime = (float) stream.ReceiveNext();
	}
}

void Update() {
	if (this.Active && this.nc.Agent.isOnNavMesh) { 
	if (this.MoveTime <= 0) {

	if (this.nc.Agent.isStopped && this.nc.Agent != null && this.nc.Agent.isOnNavMesh) { this.nc.Agent.isStopped = false;}
	
	this.MoveTime = MoveCd;
	Vector3 target = Vector3.up;
	
	if (this.nc.X && nc.Z) target = new Vector3(this.gameObject.transform.position.x + Random.Range(-this.searchDistance, this.searchDistance +1), 0, this.gameObject.transform.position.z + Random.Range(-this.searchDistance, this.searchDistance +1));
	else if (!this.nc.X && nc.Z) target = new Vector3(this.transform.position.x, 0, this.gameObject.transform.position.z + Random.Range(-this.searchDistance, this.searchDistance +1));
	else if (this.nc.X && !nc.Z) target = new Vector3(this.gameObject.transform.position.x + Random.Range(-this.searchDistance, this.searchDistance +1), 0, this.transform.position.z);

	if (target != null && this.nc.Agent != null && this.nc.Agent.isOnNavMesh) { this.nc.Agent.SetDestination(target); }

	}

	if (this.MoveTime > 0 && this.nc.Agent.destination == this.transform.position) { this.MoveTime -= Time.deltaTime; }        
	}
}

public void StartWander() { 
	this.Active = true;
	this.MoveTime = MoveCd;
}

public void StopWander() { 
	this.Active = false;
}

}
