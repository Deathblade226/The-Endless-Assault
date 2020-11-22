using Photon.Pun;
using UnityEngine;

public class MeleeWeapon : Weapon {

private void Update() {
	if (BuffDamageCd <= 0) {
	BuffDamage = 0;		
	} else if (BuffDamageCd > 0) { BuffDamageCd -= Time.deltaTime; }
}

private void OnTriggerEnter(Collider other) {
	if (other.GetComponent<PhotonView>() == null) return;
	if (Enemies.Contains(other.tag) && (CanAttack || ContinuousAttack) && PhotonNetwork.IsMasterClient) { 
	if (!ContinuousAttack) CanAttack = false;
	other.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, Damage);
	}	
}

public override void Attack() { CanAttack = true; }

}
