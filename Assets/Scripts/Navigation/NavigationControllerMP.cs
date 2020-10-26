using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathingDebug))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AttackNavMP))]
[RequireComponent(typeof(TravelNavMP))]
[RequireComponent(typeof(WanderNavMP))]
[RequireComponent(typeof(VisionSystem))]
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
    GameObject target = GetComponent<VisionSystem>().SeenTarget;
    if (target == null) target = AIUtilities.GetNearestGameObject(gameObject, travelNav.TargetTag, xray:true);

    if (target != null) { 
    
    travelNav.Moving = false; 
    wanderNav.StopWander(); 
    Agent.SetDestination(target.transform.position); 
    
    } else if (target != null && attackNav != null && attackNav.Target != "" && !attackNav.Active) { 
    
    travelNav.Moving = false; 
    wanderNav.StopWander(); 
    attackNav.StartAttacking(); 
    
    } else if (objective != null && !travelNav.Moving && !attackNav.Active) { 
    
    wanderNav.StopWander();
    travelNav.StartTravel();  
    
    } else if (!wanderNav.Active && !travelNav.Moving && !attackNav.Active) { 
    
    wanderNav.StartWander();
    travelNav.Moving = false; 
    
    }
    if (Animator != null) Animator.SetFloat("Speed", Agent.velocity.magnitude);
}

}
