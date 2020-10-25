using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Defense, IPunObservable {

[Tooltip("One attack every x amount of time.")][SerializeField]float attackRate = 0;
[Tooltip("The damage of the projectile.")][SerializeField]float damage = 0;
[Tooltip("The speed of the projectile.")][SerializeField]float shotSpeed = 0;
[Tooltip("Does the shot have gravity.")][SerializeField]bool shotHasGravity = false;
[Tooltip("Spawnpoint of the projectile.")][SerializeField]GameObject spawnPoint = null;
[Tooltip("What the tower shoots.")][SerializeField]GameObject projectile = null;

private float attackCD = 0;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.attackCD);
	} else {
	this.attackCD = (float)stream.ReceiveNext();
	}
}

private void Update() {
	VisionSystem vs = gameObject.GetComponent<VisionSystem>();
	GameObject target = vs.SeenTarget;
	//Debug.Log(attackCD);
	if (target != null && attackCD <= 0) {
	Debug.DrawLine(spawnPoint.transform.position, target.transform.position);

	gameObject.transform.LookAt(target.transform);
	if (projectile != null) { 
	attackCD = attackRate;
	GameObject shot	= null;
	if (PhotonNetwork.IsMasterClient) shot = PhotonNetwork.Instantiate(projectile.name, spawnPoint.transform.position, gameObject.transform.rotation);
	shot.GetComponent<Projectile>().WeaponDamage = damage;
	Rigidbody shotRB = shot.GetComponent<Rigidbody>();
	shotRB.useGravity = shotHasGravity;
	shotRB.AddForce(gameObject.transform.forward * shotSpeed * 10, ForceMode.Acceleration);
	}

	} else if (vs.Active) { attackCD -= Time.deltaTime; }
}

[PunRPC]
private void updateShot(string id) { 

}

}
