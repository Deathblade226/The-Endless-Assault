using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffWeapon : Weapon {

[SerializeField] GameObject projectile;
[SerializeField] float waitTime = 1;

private float waitCD = 0;

void Start() {
        
}

void Update() {

}

public override void Attack() { }

}
