using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

[SerializeField] PhotonView pv = null;
[SerializeField] float damage = 0.0f;
[SerializeField] List<string> enemies;
<<<<<<< Updated upstream
=======
[SerializeField] bool continuousAttack = false;
[SerializeField] List<DamageType.Damage> damageTypes;
>>>>>>> Stashed changes

public PhotonView PV { get => pv; set => pv = value; }
public AttackNavMP attack { get; set; }
public string Type = "Weapon";
<<<<<<< Updated upstream
public float Damage { get => damage; set => damage = value; }
public bool CanAttack { get; set; }
public List<string> Enemies { get => enemies; set => enemies =  value ; }
=======
[HideInInspector] public float Damage { get => damage + BuffDamage; set => damage = value; }
[HideInInspector] public bool CanAttack { get; set; }
[HideInInspector] public List<string> Enemies { get => enemies; set => enemies =  value ; }
[HideInInspector] public bool ContinuousAttack { get => continuousAttack; set => continuousAttack =  value ; }
[HideInInspector] public List<DamageType.Damage> DamageTypes { get => damageTypes; set => damageTypes = value; }
[HideInInspector] public float BuffDamage = 0;
[HideInInspector] public float BuffDamageCd = 0;

public virtual void Attack() { }

>>>>>>> Stashed changes
}

