using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisualPathingController : MonoBehaviour {

[SerializeField] Transform start;
[SerializeField] Transform target;
[SerializeField] GameObject spawnSpirit;

private GameObject spirit = null;
private bool wait = false;

public void Update() {
	bool monsters = (GameObject.FindGameObjectWithTag("Monster") != null);
	bool reached = (spirit != null) ? ((target.transform.position - spirit.transform.position).magnitude < 1) : false;
	if (spirit != null && reached && spirit.GetComponentInChildren<TrailRenderer>().positionCount == 0 && PhotonNetwork.IsMasterClient) { PhotonNetwork.Destroy(spirit); }
	if (spirit == null && !wait && !monsters) { 
	if (PhotonNetwork.IsMasterClient) { 
	spirit = PhotonNetwork.InstantiateRoomObject(spawnSpirit.name, start.transform.position, Quaternion.identity);
	spirit.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
	}
	} else if (monsters && PhotonNetwork.IsMasterClient && spirit != null) { PhotonNetwork.Destroy(spirit); }
}

public void StartSpirit() { wait = false; }
public void PauseSpirit() { 
	wait = true;  
	if (PhotonNetwork.IsMasterClient && spirit != null) PhotonNetwork.Destroy(spirit);
}

}
