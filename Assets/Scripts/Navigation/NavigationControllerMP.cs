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
[SerializeField] float speed = 3;

public AttackNavMP AttackNav { get => attackNav; set => attackNav = value; }
public TravelNavMP TravelNav { get => travelNav; set => travelNav = value; }
public WanderNavMP WanderNav { get => wanderNav; set => wanderNav =  value ; }
public float Speed { get => speed; set => speed = value; }
public float Slow { get; set; }

//public Game GameController { get; set; }

private NavMeshPath navPath;
private GameObject objective = null;
private float slowDuration = 0;

void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 
    if(stream.IsWriting) {
	stream.SendNext(this.Speed);
	stream.SendNext(this.Slow);
	stream.SendNext(this.slowDuration);
	} else {
	this.Speed = (float)stream.ReceiveNext();
	this.Slow = (float)stream.ReceiveNext();
	this.slowDuration = (float)stream.ReceiveNext();
	}
}

private void Awake() {
    transform.GetComponentInChildren<VisionSystem>().Active = true;
    Agent = gameObject.GetComponent<NavMeshAgent>();
    Speed = Agent.speed;
    attackNav = gameObject.GetComponent<AttackNavMP>();        
    TravelNav = gameObject.GetComponent<TravelNavMP>();        
    WanderNav = gameObject.GetComponent<WanderNavMP>();
}

private IEnumerator Start() { 
	yield return null;
    //this.GameController = Game.game;
    this.navPath = new NavMeshPath();
    if (this.attackNav != null) this.attackNav.Nc = this;
    if (this.WanderNav != null) this.WanderNav.Nc = this;
    if (this.TravelNav != null && this.TravelNav.TargetTag != "") {
    this.TravelNav.Nc = this;
    this.objective = AIUtilities.GetNearestGameObject(this.gameObject, this.TravelNav.TargetTag, xray:true);
    }
}

private void Update() {
    if (slowDuration <= 0) { Slow = 0;
	} else if (slowDuration > 0) { slowDuration -= Time.deltaTime; }

    Agent.speed = Mathf.Clamp(speed - Slow, 0.5f, 100);
    if (transform.GetComponentInChildren<VisionSystem>() == null) return;
    GameObject target = transform.GetComponentInChildren<VisionSystem>().SeenTarget;
    if (target != null) { 
    
    //Debug.Log("Attack");
    TravelNav.Moving = false;
    WanderNav.StopWander(); 
    attackNav.StartAttacking(); 
    
    } else if (objective != null && target == null) { 

    //Debug.Log("Travel");
    WanderNav.StopWander();
    attackNav.StopAttacking();
    TravelNav.StartTravel();  
    
    } else { 
    
    //Debug.Log("Waner");
    TravelNav.Moving = false;
    attackNav.StopAttacking();
    WanderNav.StartWander();
    }
    if (Animator != null) Animator.SetFloat("Speed", Agent.velocity.magnitude);
}

[PunRPC]
public void SetSlow(float slow) { 
    if (Slow < slow) Slow = slow; 
    slowDuration = 1.5f; 
}

}
