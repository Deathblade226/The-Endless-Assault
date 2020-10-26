using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPun, IPunObservable {

[SerializeField] float damage = 0;
[SerializeField] string enemyTag = "";
[SerializeField] float LifeTime = 0;
[SerializeField] PhotonView pv;

public float WeaponDamage { get => damage; set => damage = value; }
public string EnemyTag { get => enemyTag; set => enemyTag = value; }

private float currentLife;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
    if(stream.IsWriting) {
	stream.SendNext(this.currentLife);
	} else {
	this.currentLife = (float) stream.ReceiveNext();
	}
}

private void Start() {
    currentLife = LifeTime;	
}
private void Update() {
    if (currentLife > 0) { currentLife -= Time.deltaTime; } else { pv.RPC("RPC_Destroy", RpcTarget.All); }
}
private void OnTriggerEnter(Collider other) {
    if (other.tag == EnemyTag) { 
    
    Damagable health = other.GetComponent<Damagable>();
    //Debug.Log(health);
    if (health != null) { 
    other.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, WeaponDamage);    
    }
    pv.RPC("RPC_Destroy", RpcTarget.All);
    }       
}

[PunRPC]
public void RPC_Destroy() {
    Destroy(gameObject); 
}

}
