using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationMP : MonoBehaviourPun {

[Tooltip("The nav mesh agen to make the monster move on a navmesh.")][SerializeField] NavMeshAgent agent = null;
[Tooltip("The monsters animator to have them be animated.")][SerializeField] Animator animator = null;
[Tooltip("Allows the monster to move on the x axis.")][SerializeField] bool m_x = true;
[Tooltip("Allows the monster to move on the z axis.")][SerializeField] bool m_z = true;
[Tooltip("Allows the monster to see through walls.")][SerializeField] bool seeThroughWalls = false;
[Tooltip("The fov the monster can see.")][SerializeField] float fov = 180.0f;
[Tooltip("The vision range of the monster.")][SerializeField] float range = 10.0f;

public NavMeshAgent Agent { get => agent; set => agent = value; }
public Animator Animator { get => animator; set => animator = value; }
public bool X { get => m_x; set => m_x = value; }
public bool Z { get => m_z; set => m_z = value; }

//Debug
}
