using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AttackNav : MonoBehaviour {

[SerializeField] float attackCD = 3.0f;
[SerializeField] float attackRange = 2.0f;
[SerializeField] NavigationController nc = null;
[SerializeField] Weapon weapon = null;
[SerializeField] bool lookForAltTarget = true;
[SerializeField] public string target = "";

public string Target { get; set; } = "";
public bool Active { get; set; } = false;
public NavigationController Nc { get => nc; set => nc = value; }
public bool Attacking { get => attacking; }

private float AttackTime = 0;
private bool attacking = false;
//Short for altTarget but this is the object version
private GameObject altT = null;

private void Awake() {
	//if(weapon != null) weapon.attack = this;
}

private void Update() {
	if (nc.GameController.GameRunning) { 
	if (lookForAltTarget && altT == null) altT = AIUtilities.GetNearestGameObject(gameObject, target, attackRange);

	if (altT != null) { Target = altT.tag; Active = true; }
	else { Nc.Pv.RPC("StopAttacking", RpcTarget.All); Nc.Agent.isStopped = false; }

	if ((Target != "" || altT != null) && Active) { 
	
	var target = AIUtilities.GetNearestGameObject(gameObject, Target, Nc.Range, Nc.Fov, Nc.SeeThroughWalls);

	if (target != null) { 

	attacking = ((transform.position - target.transform.position).magnitude <= attackRange && AttackTime <= 0);
	
	if (attacking) {
	transform.LookAt(target.transform);
	AttackTime = attackCD; 
	Nc.Agent.isStopped = true; 

	if (weapon != null) { 
	weapon.CanAttack = true; 
	}
		
	if (Nc.Animator != null) Nc.Animator.SetTrigger("Attack");  

	} else if ((transform.position - target.transform.position).magnitude <= attackRange) { 
	Nc.Agent.isStopped = true; 
	AttackTime -= Time.deltaTime; 

	} else { 
	if (weapon.Type != "Summon") { 
	Nc.Animator.SetTrigger("StopAttack"); 
	Nc.Agent.SetDestination(target.transform.position); 
	Nc.Agent.isStopped = false; 
	}
	AttackTime -= Time.deltaTime; 

	}

	}
	}        
	}
}


[PunRPC]
public void StartAttacking() { 
	Active = true;
}

[PunRPC]
public void StopAttacking() { 
	Active = false;
	Target = "";
}

}
