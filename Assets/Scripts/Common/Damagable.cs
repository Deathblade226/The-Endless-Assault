using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damagable : MonoBehaviour {

[SerializeField] float m_health = 100;
[SerializeField] int value = 0;
[SerializeField] [Range(-1,1)]float m_damageReduction = 0;
[SerializeField] [Range(0,1)]float m_regenCap = 1;
[SerializeField] float m_regenAmount = 0;
[SerializeField] float m_regenCd = 0;
[SerializeField] float m_damageCd = 0;
[SerializeField] bool m_constantRegen = false;
[SerializeField] PhotonView PV = null;
[SerializeField] GameObject m_deathSpawn = null;
[SerializeField] Slider m_healthBar = null;

private float maxHealth;
private float damageCd;
private float regenCd;

public float MaxHealth { get => maxHealth; set => maxHealth = value; }
public float health { get => m_health; set => m_health = value; }
public bool destroyed { get; set; } = false;
public float DamageReduction { get => m_damageReduction; set => m_damageReduction = value; }
private void Start() { MaxHealth = health; 
	if (m_healthBar != null) { 
	m_healthBar.maxValue = health;
	m_healthBar.value = health;
	}
}

private void Update() {
	//Updates the healthbar
	if (m_healthBar != null) { m_healthBar.value = health; }
	//Reduces the IFrames after hit
	if (damageCd > 0) { damageCd -= Time.deltaTime; }
	//Redices the regen cd when the target hasnt taken damage in a bit
	//This will start the regen when both cds are 0
	//Resets the regen cd if hit
	if (damageCd <= 0 && regenCd > 0) { regenCd -= Time.deltaTime; }
	else if (m_constantRegen || ( damageCd <= 0 && regenCd <= 0 )) { PV.RPC("RegenHealth", RpcTarget.All); }
	else { regenCd = m_regenCd; }
}

[PunRPC]
public void ApplyDamage(float damageAmount) {
	if (damageCd <= 0) {
	damageCd = m_damageCd;
	health = health - (damageAmount - (damageAmount*DamageReduction));
	if (!destroyed && health <= 0) {
	//Game.game.Currency += score;
	if (m_deathSpawn != null) {
	GameObject damage = PhotonNetwork.Instantiate(m_deathSpawn.name, transform.position, transform.rotation);
	damage.GetComponent<Damage>().Spawn(transform.position, Vector3.zero, Vector3.up);
	}
	//Destroy(gameObject);
	if (PV.IsMine) { 
	PhotonNetwork.Destroy(gameObject);
	destroyed = true;
	}
	}
	}
}

[PunRPC]
public void RegenHealth() {
	float regenCap = ( maxHealth * m_regenCap);
	//Debug.Log(regenCap);
	if (health + m_regenAmount <= regenCap) { health += m_regenAmount; }
	else if (health + m_regenAmount > regenCap && health < regenCap) { health = regenCap; }
}

public void RunRPCMethod(float damage) { 
	PV.RPC("ApplyDamage", RpcTarget.All, damage);

}

}
