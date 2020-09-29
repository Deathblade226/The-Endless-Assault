using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

private bool hit = false;

public override void Attack() { hit = false; }

private void Awake() { Type = "Melee"; }

void PlayerAttack(GameObject go) {
    //Debug.Log("Gameobject: " + go + " | Tags: " + go.tag != gameObject.tag);
    if (go != null && go.tag != gameObject.tag) { 
    Damagable health = go.GetComponent<Damagable>();    
    if (health != null) {
    health.RunRPCMethod(Damage);
    hit = true;
    }
    if (DestroyOnHit) Destroy(gameObject);
    }   
}

private void OnTriggerEnter(Collider other) {
    //Debug.Log("This: " + gameObject.tag + " | Other: " + other);
    Attack();
    GameObject go = other.gameObject;
    GameObject[] objects = new GameObject[1];
    objects[0] = go;
    if (go.tag != "Weapon" && hit == false) { PlayerAttack(go); }
}

}
