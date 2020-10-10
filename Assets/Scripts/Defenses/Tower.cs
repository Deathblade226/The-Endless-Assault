using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Defense, IPunObservable {

[SerializeField]float attackRate = 0;
[SerializeField]float damage = 0;
[SerializeField]float shotSpeed = 0;
[SerializeField]bool shotHasGravity = false;
[SerializeField]GameObject spawnPoint = null;
[SerializeField]GameObject projectile = null;

private float attackCD = 0;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.attackCD);
	} else {
	this.attackCD = (float)stream.ReceiveNext();
	}
}

private void Update() {
	GameObject target = gameObject.GetComponent<VisionSystem>().SeenTarget;

	if (target != null && attackCD == 0) {

	gameObject.transform.LookAt(target.transform);
	GameObject shot = PhotonNetwork.Instantiate(projectile.name, spawnPoint.transform.position, gameObject.transform.rotation);
	Rigidbody shotRB = shot.GetComponent<Rigidbody>();
	shotRB.useGravity = shotHasGravity;
	shotRB.AddForce(gameObject.transform.forward * shotSpeed, ForceMode.Acceleration);

	} else { attackCD -= Time.deltaTime; }
}

}
