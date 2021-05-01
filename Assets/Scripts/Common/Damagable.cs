using Photon.Pun;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Damagable : MonoBehaviourPun, IPunObservable {

[FoldoutGroup("-- Photon --")] [SerializeField] PhotonView PV = null;

[FoldoutGroup("-- Animator --")] [SerializeField] Animator animator = null;

[Header("Logic")]
[FoldoutGroup("-- Damage Logic --")] [SerializeField] bool m_killOnDeath = false;
[FoldoutGroup("-- Damage Logic --")] [SerializeField] float m_damageCd = 0;

[FoldoutGroup("-- Damage Logic --")] [SerializeField] [Range(-1,1)]float m_damageReduction = 0;

[FoldoutGroup("-- Damage Logic --")] [SerializeField] float m_health = 100;

[Header("Defenses")]
[FoldoutGroup("-- Damage Logic --")] [SerializeField] List<DefenseDamageTypes> resistances;
[Header("Immunities")]
[FoldoutGroup("-- Damage Logic --")] [SerializeField] List<ImmuneDamageTypes> immunities;

[FoldoutGroup("-- Regeneration --")] [SerializeField] bool canRegen = false;
[FoldoutGroup("-- Regeneration --")] [SerializeField] [Range(0,1)]float m_regenCap = 1;
[FoldoutGroup("-- Regeneration --")] [SerializeField] float m_regenAmount = 0;
[FoldoutGroup("-- Regeneration --")] [SerializeField] float m_regenCd = 0;
[FoldoutGroup("-- Regeneration --")] [SerializeField] bool m_constantRegen = false;

[FoldoutGroup("-- Death --")] [SerializeField] GameObject m_hideObject = null;
[FoldoutGroup("-- Death --")] [SerializeField] GameObject m_deathSpawn = null;

[FoldoutGroup("-- Display --")] [SerializeField] Slider m_healthBar = null;
[FoldoutGroup("-- Display --")] [SerializeField] TextMeshProUGUI m_healthText = null;

[FoldoutGroup("-- Hide settings --")] [SerializeField] GameObject hideObject = null;
[FoldoutGroup("-- Hide settings --")] [SerializeField] bool hide = false;
[FoldoutGroup("-- Hide settings --")] [SerializeField] float hideDistance = 10;

[FoldoutGroup("-- Color --")] [SerializeField] Image fillArea = null;
[FoldoutGroup("-- Color --")] [SerializeField] Color oneHundred;
[FoldoutGroup("-- Color --")] [SerializeField] Color sevenFive;
[FoldoutGroup("-- Color --")] [SerializeField] Color five;
[FoldoutGroup("-- Color --")] [SerializeField] Color twoFive;

private float maxHealth;
private float damageCd;
private float regenCd;
private bool alive = true;

public float MaxHealth { get => maxHealth; set => maxHealth = value; }
public float health { get => m_health; set => m_health = value; }
public bool destroyed { get; set; } = false;
public float DamageReduction { get => m_damageReduction; set => m_damageReduction = value; }
public Color OneHundred { get => oneHundred; set => oneHundred = value; }
public Color SevenFive { get => sevenFive; set => sevenFive = value; }
public Color Five { get => five; set => five = value; }
public Color TwoFive { get => twoFive; set => twoFive =  value ; }
public GameObject HideObject { get => hideObject; set => hideObject =  value ; }

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.health);
	stream.SendNext(this.damageCd);
	stream.SendNext(this.regenCd);
	} else {
	this.health = (float) stream.ReceiveNext();
	this.damageCd= (float) stream.ReceiveNext();
	this.regenCd = (float) stream.ReceiveNext();
	}
}

private void Start() { MaxHealth = health; 
	if (m_healthBar != null) { 
	m_healthBar.maxValue = health;
	m_healthBar.value = health;
	}
}

private void Update() {
	if (animator != null) { animator.SetFloat("Health", health);  }
	//if (PhotonNetwork.IsMasterClient) Debug.Log($"{PV.IsMine}|{hide}|{hideObject != null}|{( Camera.main.transform.position - hideObject.transform.position ).magnitude}");
	if (PV.IsMine && hide && HideObject != null && ( Camera.main.transform.position - HideObject.transform.position).magnitude >= hideDistance) { 
	HideObject.SetActive(false);
	} else if (PV.IsMine && HideObject != null) { HideObject.SetActive(true); }

	m_healthText.SetText($"{health}/{maxHealth}");
	Color color = new Color();
	float percent = health/maxHealth;
	if (percent <= 0.25f) { color = TwoFive; }
	else if (percent <= 0.5f) { color = Five; }
	else if (percent <= 0.75f) { color = SevenFive; }
	else { color = OneHundred; }

	//Updates the healthbar
	if (m_healthBar != null) { 
	m_healthBar.value = health; 
	if (fillArea != null) fillArea.color = color;
	}

	//Reduces the IFrames after hit
	if (damageCd > 0) { damageCd -= Time.deltaTime; }
	//Redices the regen cd when the target hasnt taken damage in a bit
	//This will start the regen when both cds are 0
	//Resets the regen cd if hit
	if (canRegen) { 
	if (damageCd <= 0 && regenCd > 0) { regenCd -= Time.deltaTime; }
	else if (m_constantRegen || ( damageCd <= 0 && regenCd <= 0 )) { PV.RPC("RegenHealth", RpcTarget.All); 
	}
	else { regenCd = m_regenCd; }
	}
}

[PunRPC]
public void ApplyDamage(float damageAmount) {
	//Debug.Log(damageAmount);
	if (damageCd <= 0) {
	damageCd = m_damageCd;
	regenCd = m_regenCd;
	if (PhotonNetwork.IsMasterClient) {
	health -= (damageAmount - (damageAmount*DamageReduction));
	if (health > maxHealth) health = maxHealth;
	}
	if (!destroyed && health <= 0 && m_killOnDeath) {
	//Game.game.Currency += score;
	if (m_deathSpawn != null) {
	GameObject damage = PhotonNetwork.Instantiate(m_deathSpawn.name, transform.position, transform.rotation);
	}
	//Destroy(gameObject);
	if (PV.IsMine) { 
	PhotonNetwork.Destroy(gameObject);
	destroyed = true;
	}
	} else if (!destroyed && health <= 0 && !m_killOnDeath) { 
	if (m_hideObject != null) { 
	m_hideObject.SetActive(false);
	destroyed = true;
	alive = false;
	if (animator != null) { animator.SetBool("Alive", alive); }
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
	//m_healthText.SetText($"{health}/{maxHealth}");
}

public void RunRPCMethod(float damage) { 
	PV.RPC("ApplyDamage", RpcTarget.All, damage);

}

}
