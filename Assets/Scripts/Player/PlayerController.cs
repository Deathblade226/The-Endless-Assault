using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerController : MonoBehaviour {
    
[SerializeField]float walkSpeed = 1;
[SerializeField]float roationSpeed = 1;

private KeyboardMouse controls;

private float currentSpeed = 1;
private Vector2 move;
private bool sprint = false;
private Rigidbody rb;

private void Awake() {
	controls = new KeyboardMouse();	
	rb = gameObject.GetComponent<Rigidbody>();

	controls.Player.Walk.performed += ctx => move = ctx.ReadValue<Vector2>();
	controls.Player.Walk.performed += ctx => move = Vector2.zero;
	controls.Player.Sprint.performed += ctx => sprint = ctx.ReadValue<bool>();
	controls.Player.Sprint.performed += ctx => sprint = false;
	controls.Player.Jump.performed += ctx => Jump();

}

private void Start() {
	currentSpeed = walkSpeed;		
}

private void Update() {
	//Vector3 movement = new Vector2(move.x, move.y) * Time.deltaTime;
	//rb.AddForce(movement, ForceMode.Acceleration);
	Debug.Log(move);
}

private void Jump() { 

}

private void OnEnable() {
	controls.Player.Enable();		
}
private void OnDisable() {
	controls.Player.Disable();		
}

}
