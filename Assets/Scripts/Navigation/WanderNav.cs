using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WanderNav : MonoBehaviour {

[SerializeField] float MoveCd = 5.0f;
[SerializeField] float searchDistance = 5.0f;
[SerializeField] NavigationController nc = null;
public bool Active { get; set; } = false;
public NavigationController Nc { get => nc; set => nc = value; }

private float MoveTime = 0;

private void Update() {
	if (Active && nc.Agent.isOnNavMesh) { 
	if (MoveTime <= 0) {

	if (nc.Agent.isStopped && nc.Agent != null && nc.Agent.isOnNavMesh) { nc.Agent.isStopped = false;}
	
	MoveTime = MoveCd;
	Vector3 target = Vector3.up;
	
	if (nc.X && nc.Z) target = new Vector3(gameObject.transform.position.x + Random.Range(-searchDistance, searchDistance+1), 0, gameObject.transform.position.z + Random.Range(-searchDistance, searchDistance+1));
	else if (!nc.X && nc.Z) target = new Vector3(transform.position.x, 0, gameObject.transform.position.z + Random.Range(-searchDistance, searchDistance+1));
	else if (nc.X && !nc.Z) target = new Vector3(gameObject.transform.position.x + Random.Range(-searchDistance, searchDistance+1), 0, transform.position.z);

	if (target != null && nc.Agent != null && nc.Agent.isOnNavMesh) { nc.Agent.SetDestination(target); }

	}

	if (MoveTime > 0 && nc.Agent.destination == transform.position) { MoveTime -= Time.deltaTime; }        
	}
}

[PunRPC]
public void StartWander() { 
	if (!Nc.Pv.IsMine) return;
	Active = true;
	MoveTime = MoveCd;
}

[PunRPC]
public void StopWander() { 
	if (!Nc.Pv.IsMine) return;
	Active = false;
}

}
