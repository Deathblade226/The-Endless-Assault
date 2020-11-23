using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffWeapon : Weapon {

[SerializeField] GameObject projectile;
[SerializeField] Transform spawnpoint;
[SerializeField] bool lookAtTarget = false;
[SerializeField] string enemy = "";
[SerializeField] float waitTime = 1;
[SerializeField] float shotSpeed = 1;

[SerializeField] VisionSystem vs = null;

private float waitCD = 0;
private bool cast = false;

void Update() {
	if (!PV.IsMine) return;
	if (waitCD > 0) { waitCD -= Time.deltaTime; }
	else if (cast) { 
	cast = false; 
	GameObject item = PhotonNetwork.Instantiate(projectile.gameObject.name, spawnpoint.position, Quaternion.identity);
	PV.RPC("Spawn", RpcTarget.All, item.GetComponent<PhotonView>().ViewID);
	}
}

[PunRPC] 
private void Spawn(int id) { 
	if (projectile != null) { 
	GameObject shot = PhotonView.Find(id).gameObject;
	transform.rotation = spawnpoint.rotation;
	if (lookAtTarget && vs != null)  { shot.transform.LookAt(vs.SeenTarget.transform.position); }
	Projectile projectile = shot.GetComponent<Projectile>();
	projectile.WeaponDamage = Damage;
	projectile.EnemyTag = (vs != null) ? vs.SeenTarget.tag : enemy;
	Rigidbody shotRB = shot.GetComponent<Rigidbody>();
	shotRB.useGravity = false;
	shotRB.AddForce(spawnpoint.transform.forward * shotSpeed * 10, ForceMode.Acceleration);
	}
}

public override void Attack() { cast = true; waitCD = waitTime; }

}
