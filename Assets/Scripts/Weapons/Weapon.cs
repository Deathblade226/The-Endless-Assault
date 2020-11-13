using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

[SerializeField] PhotonView pv = null;
[SerializeField] float damage = 0.0f;
[SerializeField] List<string> enemies;
[SerializeField] bool continuousAttack = false;

[HideInInspector] public PhotonView PV { get => pv; set => pv = value; }
[HideInInspector] public AttackNavMP attack { get; set; }
public string Type = "Weapon";
[HideInInspector] public float Damage { get => damage + BuffDamage; set => damage = value; }
[HideInInspector] public bool CanAttack { get; set; }
[HideInInspector] public List<string> Enemies { get => enemies; set => enemies =  value ; }
[HideInInspector] public bool ContinuousAttack { get => continuousAttack; set => continuousAttack =  value ; }
[HideInInspector] public float BuffDamage = 0;
[HideInInspector] public float BuffDamageCd = 0;

}

