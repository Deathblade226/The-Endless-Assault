using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerController : MonoBehaviour {
    
[SerializeField]float walkSpeed = 1;
[SerializeField]float roationSpeed = 1;
[SerializeField]GameObject camTarget = null;
[SerializeField]float camClamp = 10;

private Rigidbody rb;
private float currentSpeed;
private Vector2 walkInput;
private Vector2 rotationInput;
private float jumpInput;
private float sprintInput;

private void Awake() {
	rb = gameObject.GetComponent<Rigidbody>();
	Cursor.lockState = CursorLockMode.Confined;
}

private void Start() {
	currentSpeed = walkSpeed;		
}

private void FixedUpdate() {
	transform.Translate(new Vector3((walkInput.x * Time.deltaTime) * currentSpeed,0, (walkInput.y * Time.deltaTime) * currentSpeed));
	Vector2 mousePosition = rotationInput - new Vector2(Screen.width / 2, Screen.height / 2);
	//Debug.Log(mousePosition);
	if(Cursor.lockState != CursorLockMode.Locked) transform.Rotate(Vector3.up, mousePosition.normalized.x);
	Quaternion rotation = camTarget.transform.rotation;
	rotation.x += mousePosition.y;
	Debug.Log(rotation.x);
	if (rotation.x > -camClamp && rotation.x < camClamp) camTarget.transform.Rotate(Vector3.right, mousePosition.normalized.y);
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
public void OnRotate(InputAction.CallbackContext context) { 
	rotationInput = context.ReadValue<Vector2>();
}
public void OnToggleCursor(InputAction.CallbackContext context) { 
	Cursor.lockState = ( Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.Confined : CursorLockMode.Locked;
}


private void OnEnable() {

}
private void OnDisable() {

}

}
