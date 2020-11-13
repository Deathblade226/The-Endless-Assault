using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

[SerializeField] PhotonView pv = null;
[SerializeField] float damage = 0.0f;
[SerializeField] List<string> enemies;
[SerializeField] bool continuousAttack = false;

public PhotonView PV { get => pv; set => pv = value; }
public AttackNavMP attack { get; set; }
public string Type = "Weapon";
public float Damage { get => damage + BuffDamage; set => damage = value; }
public bool CanAttack { get; set; }
public List<string> Enemies { get => enemies; set => enemies =  value ; }
public bool ContinuousAttack { get => continuousAttack; set => continuousAttack =  value ; }
public float BuffDamage = 0;
public float BuffDamageCd = 0;

}

