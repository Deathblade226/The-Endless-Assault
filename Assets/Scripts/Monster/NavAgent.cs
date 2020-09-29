using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour {

[SerializeField][Tooltip("The nav agent that will move the enemy.")] NavMeshAgent m_navAgent = null;
[SerializeField][Tooltip("The weapon the enemy holds.")] GameObject m_weapon = null;
[SerializeField][Tooltip("The target string the enemy will attack.")] string target;
[SerializeField][Tooltip("How long the enemy will wait when they reach their destination.")] float MoveCd = 5.0f;
[SerializeField][Tooltip("The range they can wander.")] float searchDistance = 5.0f;
[SerializeField][Tooltip("The cooldown between attacks.")] float attackCD = 3.0f;
[SerializeField][Tooltip("The range they can attack from.")] float attackRange = 2.0f;
[SerializeField][Tooltip("The animator for attacking and moving.")] Animator animator = null;
[SerializeField][Tooltip("If checked the monster can move on the x axis.")] bool m_x = true;
[SerializeField][Tooltip("If checked the monster can move on the z axis.")] bool m_z = true;
[SerializeField][Tooltip("Allows the monster to see through walls if checked.")] bool SeeThroughWalls = false;
[SerializeField][Tooltip("The vision cone of the enemy.")] float fov = 180.0f;
[SerializeField][Tooltip("They range of their vision cone.")] float range = 10.0f;

private float MoveTime = 0;
private float AttackTime = 0;
private bool Attacking = false;

void Update() {
	if (m_navAgent.isOnNavMesh) { 
	var player = AIUtilities.GetNearestGameObject(gameObject, target, range, fov, SeeThroughWalls);
	//Debug.Log($"Player: {player}");
	if (player != null) { 
	Attacking = ((transform.position - player.transform.position).magnitude <= attackRange && AttackTime <= 0);
	//Debug.Log($"Attacking: {Attacking}");
	if (Attacking) { 
	transform.LookAt(player.transform);
	if (animator != null) animator.SetTrigger("Attack");  
	AttackTime = attackCD; m_navAgent.isStopped = true; }
	else if ((transform.position - player.transform.position).magnitude <= attackRange) { m_navAgent.isStopped = true; }
	else { m_navAgent.SetDestination(player.transform.position); m_navAgent.isStopped = false; }
	
	} else if (MoveTime <= 0) {

	if (m_navAgent.isStopped && m_navAgent != null && m_navAgent.isOnNavMesh) {m_navAgent.isStopped = false;}
	//Debug.Log("Moving");
	if (animator != null) animator.SetTrigger("StopAttack");
	MoveTime = MoveCd;
	Vector3 target = Vector3.up;
	if (m_x && m_z) target = new Vector3(gameObject.transform.position.x + Random.Range(-searchDistance, searchDistance+1), 0, gameObject.transform.position.z + Random.Range(-searchDistance, searchDistance+1));
	else if (!m_x && m_z) target = new Vector3(transform.position.x, 0, gameObject.transform.position.z + Random.Range(-searchDistance, searchDistance+1));
	else if (m_x && !m_z) target = new Vector3(gameObject.transform.position.x + Random.Range(-searchDistance, searchDistance+1), 0, transform.position.z);

	if (target != null && m_navAgent != null && m_navAgent.isOnNavMesh) { 
	m_navAgent.SetDestination(target); 
	if (m_weapon != null && !m_weapon.gameObject.activeSelf) { m_weapon.SetActive(true); }		
	}

	}

	if (MoveTime > 0 && m_navAgent.destination == transform.position) { MoveTime -= Time.deltaTime; }
	if (AttackTime > 0) { AttackTime -= Time.deltaTime; }

	if (animator != null) animator.SetFloat("Speed", m_navAgent.velocity.magnitude);
	}
}

}
