using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

[SerializeField] float damage = 0;
[SerializeField] string enemyTag = "";
[SerializeField] float LifeTime = 0;
[SerializeField] PhotonView pv;

public float WeaponDamage { get => damage; set => damage = value; }
public string EnemyTag { get => enemyTag; set => enemyTag = value; }

private void Start() { pv.RPC("Destroy", RpcTarget.All, LifeTime); }

private void OnTriggerEnter(Collider other) {
    if (other.tag == EnemyTag) { 
    
    Damagable health = other.GetComponent<Damagable>();
    if (health != null) { 
    health.ApplyDamage(WeaponDamage);
    }
    pv.RPC("Destroy", RpcTarget.All, 0);
    }       
}

[PunRPC]
public void Destroy(float lifetime) {
    if (lifetime == 0) { Destroy(gameObject); } else { Destroy(gameObject, LifeTime); }
}

}
