using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathingDebug))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AttackNavMP))]
[RequireComponent(typeof(TravelNavMP))]
[RequireComponent(typeof(WanderNavMP))]
public class NavigationControllerMP : NavigationMP, IPunObservable {

[SerializeField] AttackNavMP attackNav = null;
[SerializeField] TravelNavMP travelNav = null;
[SerializeField] WanderNavMP wanderNav = null;

public AttackNavMP AttackNav { get => attackNav; set => attackNav = value; }
//public Game GameController { get; set; }

private NavMeshPath navPath;
private GameObject objective = null;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 

}

private void Awake() {
    transform.GetComponentInChildren<VisionSystem>().Active = true;
    Agent = gameObject.GetComponent<NavMeshAgent>();
    attackNav = gameObject.GetComponent<AttackNavMP>();        
    travelNav = gameObject.GetComponent<TravelNavMP>();        
    wanderNav = gameObject.GetComponent<WanderNavMP>();
}

private IEnumerator Start() { 
	yield return null;
    //this.GameController = Game.game;
    this.navPath = new NavMeshPath();
    if (this.attackNav != null) this.attackNav.Nc = this;
    if (this.wanderNav != null) this.wanderNav.Nc = this;
    if (this.travelNav != null && this.travelNav.TargetTag != "") {
    this.travelNav.Nc = this;
    this.objective = AIUtilities.GetNearestGameObject(this.gameObject, this.travelNav.TargetTag, xray:true);
    }
}

private void Update() {
    GameObject target = transform.GetComponentInChildren<VisionSystem>().SeenTarget;
    //Debug.Log(target);
    if (target != null) { 
    
    travelNav.Moving = false;
    wanderNav.StopWander(); 
    attackNav.StartAttacking(); 
    
    } else if (objective != null && target == null) { 
    
    wanderNav.StopWander();
    attackNav.StopAttacking();
    travelNav.StartTravel();  
    
    } else { 
    
    travelNav.Moving = false;
    attackNav.StopAttacking();
    wanderNav.StartWander();
    }

    if (Animator != null) Animator.SetFloat("Speed", Agent.velocity.magnitude);
}

}
