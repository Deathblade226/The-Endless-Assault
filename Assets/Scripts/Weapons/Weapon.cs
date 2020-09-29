using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

[SerializeField] float damage = 0.0f;
[SerializeField] bool destroyOnHit = false;
[SerializeField] PhotonView pv = null;
public AttackNav attack { get; set; }
public PhotonView PV { get => pv; set => pv = value; }
public string Type = "Weapon";
public float Damage { get => damage; set => damage = value; }
public bool DestroyOnHit { get => destroyOnHit; set => destroyOnHit = value; }
public abstract void Attack();

}

