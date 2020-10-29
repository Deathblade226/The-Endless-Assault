using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {

[SerializeField] GameObject projectile = null;
[SerializeField] GameObject spawner = null;
[SerializeField] bool gravity = false;
[SerializeField] float speed = 1.0f;
[SerializeField] float lifeTime = 1.0f;
[SerializeField] string Target;

private void Awake() { Type = "Ranged"; }

public override void Attack() {
    PV.RPC("Shoot", RpcTarget.All);
}

private void Update() {
    if (attack != null) { 
    GameObject target = AIUtilities.GetNearestGameObject(spawner.gameObject, Target, xray:true);
    if (target != null) spawner.gameObject.transform.LookAt(target.transform);
    }
}

[PunRPC]
private void Shoot() {
    //GameObject go = Instantiate(projectile, spawner.gameObject.transform.position, Quaternion.identity);
    GameObject go = PhotonNetwork.Instantiate(projectile.name, spawner.gameObject.transform.position, Quaternion.identity);
    go.GetComponent<MeleeWeapon>().attack = attack;
    go.GetComponent<Rigidbody>().useGravity = gravity;
    go.GetComponent<SphereCollider>().isTrigger = true;
    go.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
    go.GetComponent<Weapon>().Damage = Damage;
    //Destroy(go, lifeTime);
}

}
