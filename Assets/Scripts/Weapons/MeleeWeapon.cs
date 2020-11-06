using Photon.Pun;
using UnityEngine;

public class MeleeWeapon : Weapon {

private void OnTriggerEnter(Collider other) {
	if (Enemies.Contains(other.tag) && (CanAttack || ContinuousAttack) && PhotonNetwork.IsMasterClient) { 
	if (!ContinuousAttack) CanAttack = false;
	other.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, Damage);
	}	
}

}
