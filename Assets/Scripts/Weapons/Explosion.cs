using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

[SerializeField] float damage;
[SerializeField] string target;

public string Target { get => target; set => target =  value ; }
public float Damage { get => damage; set => damage =  value ; }

private void OnTriggerEnter(Collider other) {

}

}
