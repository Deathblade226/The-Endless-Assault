using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerController : MonoBehaviour {
    
[SerializeField]float walkSpeed = 1;
[SerializeField]float roationSpeed = 1;

private KeyboardMouse controls;

private float currentSpeed = 1;

private void Awake() {
	controls = new KeyboardMouse();	

	controls.Player.Walk.performed += ctx => Walk();
}

private void Start() {
	currentSpeed = walkSpeed;		
}

private void Walk() { 

}
private void Jump() { 

}
private void Sprint() 

private void OnEnable() {
	controls.Player.Enable();		
}
private void OnDisable() {
	controls.Player.Disable();		
}

}
