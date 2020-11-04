using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile {

[SerializeField] GameObject explosion = null;

private void OnTriggerEnter(Collider other) {
	if (PhotonNetwork.IsMasterClient) { 
	GameObject spawned = PhotonNetwork.InstantiateRoomObject(explosion.name, transform.position, Quaternion.identity);
	spawned.GetComponent<Explosion>().Damage = WeaponDamage;
	spawned.GetComponent<Explosion>().Target = EnemyTag;
	Pv.RPC("RPC_Destroy", RpcTarget.All);
	}
}

}
