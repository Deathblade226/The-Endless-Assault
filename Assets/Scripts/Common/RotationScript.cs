using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {

[SerializeField] float speed = 1;
private void Update() {
	transform.Rotate(Vector3.up, speed);		
}

}
