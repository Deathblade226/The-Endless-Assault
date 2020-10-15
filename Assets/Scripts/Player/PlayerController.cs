﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerController : MonoBehaviour {
    
[SerializeField]float walkSpeed = 1;
[SerializeField]float roationSpeedX = 1;
[SerializeField]float roationSpeedY = 1;
[SerializeField]GameObject camTarget = null;
[SerializeField]float camClamp = 10;

private Rigidbody rb;
private float currentSpeed;
private Vector2 walkInput;
private Vector2 mouseInput;
private float jumpInput;
private float sprintInput;
private float rotation = 0;

private void Awake() {
	rb = gameObject.GetComponent<Rigidbody>();
	Cursor.lockState = CursorLockMode.Confined;
}

private void Start() {
	currentSpeed = walkSpeed;		
}

private void FixedUpdate() {
	transform.Translate(new Vector3((walkInput.x * Time.deltaTime) * currentSpeed,0, (walkInput.y * Time.deltaTime) * currentSpeed));
	Vector2 position = new Vector2((Screen.width/2) - mouseInput.x, (Screen.height/2) - mouseInput.y);

	if (Mathf.Abs(position.x) > 200) { rb.AddTorque(new Vector3(0, -position.normalized.x * roationSpeedX)); }

	if (Mathf.Abs(position.y) > 75) { 
	rotation += -position.normalized.y * roationSpeedY;
	rotation = Mathf.Clamp(rotation, -camClamp, camClamp);
	camTarget.transform.localEulerAngles = new Vector3(rotation, 0, 0);
	}
}

public void OnMove(InputAction.CallbackContext context) { 
	walkInput = context.ReadValue<Vector2>();
}
public void OnJump(InputAction.CallbackContext context) { 
	jumpInput = context.ReadValue<float>();
}
public void OnSprint(InputAction.CallbackContext context) { 
	sprintInput = context.ReadValue<float>();
}
public void OnMouseMove(InputAction.CallbackContext context) { 
	mouseInput = context.ReadValue<Vector2>();
}
public void OnToggleCursor(InputAction.CallbackContext context) { 
	Cursor.lockState = ( Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.Confined : CursorLockMode.Locked;
}


private void OnEnable() {

}
private void OnDisable() {

}

}
