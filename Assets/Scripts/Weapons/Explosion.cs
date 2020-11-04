using Photon.Pun;
using UnityEngine;

public class Explosion : MonoBehaviour {

[SerializeField] float damage;
[SerializeField] string target;
[SerializeField] ParticleSystem particle = null;

public string Target { get => target; set => target =  value ; }
public float Damage { get => damage; set => damage =  value ; }

public void Update() {
    if (!particle.isEmitting) { PhotonNetwork.Destroy(gameObject); }
}

private void OnTriggerEnter(Collider other) {
	if (other.tag == target) { 
    Damagable health = other.GetComponent<Damagable>(); 
    if (health != null) { 
    other.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, damage);    
    }
	}
}

}
