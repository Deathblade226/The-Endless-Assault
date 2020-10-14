using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerController : MonoBehaviour {
    
[SerializeField]float walkSpeed = 1;
[SerializeField]float roationSpeed = 1;

private Rigidbody rb;
private float currentSpeed;
private Vector2 walkInput;
private float jumpInput;
private float sprintInput;

private void Awake() {
	rb = gameObject.GetComponent<Rigidbody>();

}

private void Start() {
	currentSpeed = walkSpeed;		
}

private void Update() {
	transform.Translate(new Vector3((walkInput.x * Time.deltaTime) * currentSpeed,0, (walkInput.y * Time.deltaTime) * currentSpeed));
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


private void OnEnable() {

}
private void OnDisable() {

}

}
