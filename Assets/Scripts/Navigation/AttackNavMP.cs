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

public string Target { get; set; } = "";
public bool Active { get; set; } = false;
public NavigationControllerMP Nc { get => nc; set => nc = value; }
public bool Attacking { get => attacking; }

private float AttackTime = 0;
private bool attacking = false;
//Short for altTarget but this is the object version
private GameObject altT = null;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
	if(stream.IsWriting) {
	stream.SendNext(this.Target);
	stream.SendNext(this.Active);
	stream.SendNext(this.attacking);
	stream.SendNext(this.AttackTime);
	//stream.SendNext(this.altT);
	} else {
	this.Target = (string) stream.ReceiveNext();
	this.Active = (bool) stream.ReceiveNext();
	this.attacking = (bool) stream.ReceiveNext();
	this.AttackTime = (float) stream.ReceiveNext();
	//this.altT = (GameObject) stream.ReceiveNext();
	}
}

private void Awake() {
	if(this.weapon != null) this.weapon.attack = this;
}

private IEnumerator Start() { 
	yield return null;
}

private void Update() {
	VisionSystem vs = GetComponent<VisionSystem>();
	//if (this.nc.GameController.GameRunning) { 
	//if (this.lookForAltTarget && this.altT == null) this.altT = GetComponent<VisionSystem>().SeenTarget;

	//if (this.altT != null) { this.Target = this.altT.tag; this.Active = true; }
	//else { StopAttacking(); this.Nc.Agent.isStopped = false; }

	if (( this.Target != "" || this.altT != null) && this.Active) { 
	
	var target = vs.SeenTarget;

	if (target != null) { 

	this.attacking = (vs.Distance <= this.attackRange && this.AttackTime <= 0);
	
	Debug.Log($"Target Distance <= Attack Range: {vs.Distance <= this.attackRange}");

	if (this.attacking) {
	this.transform.LookAt(target.transform);
	this.AttackTime = attackCD; 
	this.Nc.Agent.isStopped = true; 

	if (this.weapon != null) { 
	this.weapon.Attack(); 
	}
		
	if (this.Nc.Animator != null) this.Nc.Animator.SetTrigger("Attack");  

	} else if ((this.transform.position - target.transform.position).magnitude <= this.attackRange) { 
	this.Nc.Agent.isStopped = true; 
	this.AttackTime -= Time.deltaTime; 

	} else { 
	if (this.weapon != null && this.weapon.Type != "Summon") { 
	this.Nc.Animator.SetTrigger("StopAttack"); 
	this.Nc.Agent.SetDestination(target.transform.position);
	this.Nc.Agent.isStopped = false; 
	}
	this.AttackTime -= Time.deltaTime; 

	}

	}
	}        
	//}
}
public void StartAttacking() { 
	Active = true;
}

public void StopAttacking() { 
	Active = false;
	Target = "";
}

}
