using System.Collections;
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

	//Debug.Log(position);
	//Debug.Log($"X Abs: {Mathf.Abs(position.x)}");
	if (Mathf.Abs(position.x) > 200) { 
	
	rb.AddTorque(new Vector3(0, -position.normalized.x * roationSpeedX)); 
	camTarget.transform.rotation = new Quaternion(camTarget.transform.rotation.x, 0, 0, 0);	

	}
	//Debug.Log($"Y Abs: {Mathf.Abs(position.y)}");

	//Debug.Log($"{Mathf.Abs(camTarget.transform.rotation.x)} > {camClamp}{Mathf.Abs(camTarget.transform.rotation.x) > camClamp}");
	if (Mathf.Abs(position.y) > 75) { 
	rotation += -position.normalized.y * roationSpeedY;
	rotation = Mathf.Clamp(rotation, -camClamp, camClamp);
	camTarget.transform.localEulerAngles = new Vector3(rotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
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
