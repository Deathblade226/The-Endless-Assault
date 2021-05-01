using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

[SerializeField] float damage;
[SerializeField] string target;
[SerializeField] ParticleSystem particle = null;
[SerializeField] PhotonView Pv = null;
[SerializeField] List<DamageType.Damage> damageTypes;

public string Target { get => target; set => target =  value ; }
public float Damage { get => damage; set => damage =  value ; }

public void Update() {
    if (particle != null && particle.time >= 1 && Pv != null) { Pv.RPC("RPC_Destroy", RpcTarget.All); }
}

private void OnTriggerEnter(Collider other) {
	if (other.tag == target) { 
    Damagable health = other.GetComponent<Damagable>(); 
    if (health != null) { 
    other.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, damage, damageTypes);    
    }
	}
}

[PunRPC]
public void RPC_Destroy() {
    Destroy(gameObject); 
}

}
