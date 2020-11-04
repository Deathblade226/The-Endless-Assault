using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile {

[SerializeField] GameObject explosion = null;

private void OnTriggerEnter(Collider other) {
	if (PhotonNetwork.IsMasterClient) {
	if (other.tag == "Floor" || other.tag == EnemyTag) { 
	GameObject spawned = PhotonNetwork.InstantiateRoomObject(explosion.name, transform.position, Quaternion.identity);
	Pv.RPC("RPC_Destroy", RpcTarget.All);
	}
	}
}

}
