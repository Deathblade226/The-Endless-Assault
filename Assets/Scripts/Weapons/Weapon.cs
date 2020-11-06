using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

[SerializeField] PhotonView pv = null;
[SerializeField] float damage = 0.0f;
[SerializeField] List<string> enemies;

public PhotonView PV { get => pv; set => pv = value; }
public AttackNavMP attack { get; set; }
public string Type = "Weapon";
public float Damage { get => damage; set => damage = value; }
public bool CanAttack { get; set; }
public List<string> Enemies { get => enemies; set => enemies =  value ; }

}

