using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNavMP : MonoBehaviourPun, IPunObservable {

[SerializeField] float attackCD = 3.0f;
[SerializeField] float attackRange = 2.0f;
[SerializeField] NavigationControllerMP nc = null;
[SerializeField] Weapon weapon = null;
//[SerializeField] bool lookForAltTarget = true;

public bool Active { get; set; } = false;
public NavigationControllerMP Nc { get => nc; set => nc = value; }
public bool Attacking { get => attacking; }

private float AttackTime = 0;
private bool attacking = false;
//Short for altTarget but this is the object version
private GameObject altT = null;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.Active);
	stream.SendNext(this.attacking);
	stream.SendNext(this.AttackTime);
	//stream.SendNext(this.altT);
	} else {
	this.Active = (bool) stream.ReceiveNext();
	this.attacking = (bool) stream.ReceiveNext();
	this.AttackTime = (float) stream.ReceiveNext();
	//this.altT = (GameObject) stream.ReceiveNext();
	}
}

private void Awake() {
	if(this.weapon != null) this.weapon.attack = this;
}

private void Update() {
	//if (this.nc.GameController.GameRunning) { 
	//if (this.lookForAltTarget && this.altT == null) this.altT = GetComponent<VisionSystem>().SeenTarget;

	//if (this.altT != null) { this.Target = this.altT.tag; this.Active = true; }
	//else { StopAttacking(); this.Nc.Agent.isStopped = false; }
	VisionSystem vs = transform.GetComponentInChildren<VisionSystem>();
	GameObject target = (vs.SeenTarget != null && nc.Agent.CalculatePath(vs.SeenTarget.transform.position, nc.Agent.path)) ? vs.SeenTarget : null;

	//Debug.Log(AttackTime);
	if (target != null && Active) { 

	attacking = (vs.Distance <= attackRange && AttackTime <= 0);

	if (attacking) {
	Nc.Agent.isStopped = true; 
	transform.LookAt(target.transform);
	AttackTime = attackCD; 

	if (weapon != null) { weapon.CanAttack = true; }
	if (this.Nc.Animator != null) this.Nc.Animator.SetTrigger("Attack");  
	if (weapon.gameObject.GetComponent<HealWeapon>() != null) { ((HealWeapon)weapon).Heal(); }

	} else if (vs.Distance <= attackRange) { 
	Nc.Agent.isStopped = true; 
	
	} else { 
	
	if (this.weapon != null) { 
	this.Nc.Agent.SetDestination(target.transform.position);
	this.Nc.Agent.isStopped = false; 
	}
	}

	}        
	if (AttackTime > 0) this.AttackTime -= Time.deltaTime; 
	//}
}
public void StartAttacking() { 
	Active = true;
}

public void StopAttacking() { 
	Active = false;
}

}
