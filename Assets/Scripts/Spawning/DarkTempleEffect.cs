﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkTempleEffect : MapEffect {

[SerializeField] Animator animator = null;

private string key = "00000000";

public override void StartEffect() { 
	Spawner spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
	switch(spawner.Wave) {
	case 1: if (key != "10000000") { key = "10000000"; animator.SetTrigger("R1"); } break;
	case 2: if (key != "01000000") { key = "01000000"; animator.SetTrigger("R2"); } break;
	case 3: if (key != "00100000") { key = "00100000"; animator.SetTrigger("R3"); } break;
	case 4: if (key != "00010000") { key = "00010000"; animator.SetTrigger("R4"); } break;
	case 5: if (key != "00001000") { key = "00001000"; animator.SetTrigger("R5"); } break;
	case 6: if (key != "00000100") { key = "00000100"; animator.SetTrigger("R6"); } break;
	default: Debug.Log("This map wasnt set for 7 waves"); break;
	}
}

public void RebuildNavMesh() { if (PhotonNetwork.IsMasterClient) Pv.RPC("Build", RpcTarget.All); }

[PunRPC]
public void Build() { 
	GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
}

}
