using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkTempleEffect : MapEffect {

[SerializeField] Animator animator = null;
[SerializeField] List<GameObject> visPath;

private string key = "00000000";

public void Start() {
	if (PhotonNetwork.IsMasterClient) Pv.RPC("PauseSpawns", RpcTarget.All);
}

public override void StartEffect() { 
	Spawner spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
	switch(spawner.Wave % 8) {
	case 0: if (key != "00000001") { key = "00000001"; animator.SetTrigger("R8"); } break;
	case 1: if (key != "10000000") { key = "10000000"; animator.SetTrigger("R1"); } break;
	case 2: if (key != "01000000") { key = "01000000"; animator.SetTrigger("R2"); } break;
	case 3: if (key != "00100000") { key = "00100000"; animator.SetTrigger("R3"); } break;
	case 4: if (key != "00010000") { key = "00010000"; animator.SetTrigger("R4"); } break;
	case 5: if (key != "00001000") { key = "00001000"; animator.SetTrigger("R5"); } break;
	case 6: if (key != "00000100") { key = "00000100"; animator.SetTrigger("R6"); } break;
	case 7: if (key != "00000010") { key = "00000010"; animator.SetTrigger("R7"); } break;
	default: Debug.Log($"{spawner.Wave % 7} is larger then 8 and smaller then 1"); break;
	}
}

public void ReBuildMap() { if (PhotonNetwork.IsMasterClient) Pv.RPC("Build", RpcTarget.All); }
public void Pause() { if (PhotonNetwork.IsMasterClient) Pv.RPC("PauseSpawns", RpcTarget.All); }

[PunRPC]
public void Build() { 
	GameObject[] gos = GameObject.FindGameObjectsWithTag("Spawner");
	GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
	foreach(GameObject go in gos) { 
	go.GetComponent<Spawner>().WaitToSpawn = false;
	}
	foreach(GameObject go in visPath) { 
	go.GetComponent<VisualPathingController>().StartSpirit();
	}	
}
[PunRPC]
public void PauseSpawns() { 
	GameObject[] gos = GameObject.FindGameObjectsWithTag("Spawner");
	foreach(GameObject go in gos) { 
	go.GetComponent<Spawner>().WaitToSpawn = true;
	}
	foreach(GameObject go in visPath) { 
	go.GetComponent<VisualPathingController>().PauseSpirit();
	}	
}

}
