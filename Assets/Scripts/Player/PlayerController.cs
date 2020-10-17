using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    
[Header("Movement Controls")]
[SerializeField]float walkSpeed = 1;
[SerializeField]float jumpForce = 1;
[SerializeField]float jumpCD = 2;
[SerializeField]Animator animator = null;

[Header("Ground Detection")]
[SerializeField]GameObject groundStart = null;
[SerializeField]GameObject groundEnd = null;
[SerializeField]LayerMask groundLayers;

[Header("Camera Controls")]
[SerializeField]float roationSpeedX = 1;
[SerializeField]float roationSpeedY = 1;
[SerializeField]GameObject camTarget = null;
[SerializeField]float camClamp = 10;
[SerializeField]float horizontalWindow = 200;
[SerializeField]float verticalWindow = 75;

private Rigidbody rb;
private float currentSpeed;
private Vector2 walkInput;
private Vector2 mouseInput;
private float sprintInput;
private float rotation = 0;
private bool grounded = true;
private float jumpCDC = 0;

private void Awake() {
	rb = gameObject.GetComponent<Rigidbody>();
	Cursor.lockState = CursorLockMode.Confined;
}

private void Start() {
	currentSpeed = walkSpeed;		
}

private void FixedUpdate() {
	if(jumpCD > 0) jumpCDC -= Time.deltaTime;

	RaycastHit hit;
	Vector3 GS = groundStart.gameObject.transform.position;
	Vector3 GE = groundEnd.gameObject.transform.position;
	Vector3 GD = new Vector3(GS.x, GE.y - GS.y, GS.z);

	if (Physics.Raycast(GS, GD, out hit)) { 
	//Debug.Log(hit.collider.gameObject);
	grounded = groundLayers == (groundLayers | (1 << hit.collider.gameObject.layer)); 
	}	

	transform.Translate(new Vector3((walkInput.x * Time.deltaTime) * currentSpeed,0, (walkInput.y * Time.deltaTime) * currentSpeed));
	Vector2 position = new Vector2((Screen.width/2) - mouseInput.x, (Screen.height/2) - mouseInput.y);

	if (Mathf.Abs(position.x) > horizontalWindow) { rb.AddTorque(new Vector3(0, -position.normalized.x * roationSpeedX)); }

	if (Mathf.Abs(position.y) > verticalWindow) { 
	rotation += -position.normalized.y * roationSpeedY;
	rotation = Mathf.Clamp(rotation, -camClamp, camClamp);
	camTarget.transform.localEulerAngles = new Vector3(rotation, 0, 0);
	}

	animator.SetBool("Moving", (walkInput.x != 0 || walkInput.y != 0));
	animator.SetFloat("SpeedX", walkInput.x);
	animator.SetFloat("SpeedY", walkInput.y);
}

public void OnMove(InputAction.CallbackContext context) { 
	walkInput = context.ReadValue<Vector2>();
}
public void OnJump(InputAction.CallbackContext context) { 
	if (grounded && jumpCDC <= 0) { 
	rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
	grounded = false; 
	jumpCDC = jumpCD;	
	}
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
