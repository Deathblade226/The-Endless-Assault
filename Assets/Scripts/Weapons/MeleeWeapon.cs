using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class MeleeWeapon : Weapon {

private void OnTriggerEnter(Collider other) {
	if (Enemies.Contains(other.tag) && CanAttack) { 
	CanAttack = false;
	other.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, Damage);
	}	
}

}
