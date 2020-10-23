using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

[Header("Photon")]
[SerializeField]PhotonView pv;    

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
	if (!pv.IsMine) return;
	rb = gameObject.GetComponent<Rigidbody>();
	Cursor.lockState = CursorLockMode.Confined;
}

private void Start() {
	if (!pv.IsMine) return;
	currentSpeed = walkSpeed;		
}

private void FixedUpdate() {
	if (!pv.IsMine) return;
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

	if (Mathf.Abs(position.x) > horizontalWindow && Cursor.lockState != CursorLockMode.Locked) { rb.AddTorque(new Vector3(0, -position.normalized.x * roationSpeedX)); }

	if (Mathf.Abs(position.y) > verticalWindow && Cursor.lockState != CursorLockMode.Locked) { 
	rotation += -position.normalized.y * roationSpeedY;
	rotation = Mathf.Clamp(rotation, -camClamp, camClamp);
	camTarget.transform.localEulerAngles = new Vector3(rotation, 0, 0);
	}

	animator.SetBool("Moving", (walkInput.x != 0 || walkInput.y != 0));
	animator.SetFloat("SpeedX", walkInput.x);
	animator.SetFloat("SpeedY", walkInput.y);
}

public void StartGame(InputAction.CallbackContext context) {
	if (!pv.IsMine) return;
	ActivateWaves();
}

private void ActivateWaves() { 
	if (!PhotonNetwork.IsMasterClient) return;
	GameObject go = GameObject.FindGameObjectWithTag("Spawner");
	go.GetComponent<PhotonView>().RPC("StartWave", RpcTarget.All);
}

public void OnMove(InputAction.CallbackContext context) {
	if (!pv.IsMine) return;
	walkInput = context.ReadValue<Vector2>();
}
public void OnJump(InputAction.CallbackContext context) {
	if (!pv.IsMine) return;
	if (grounded && jumpCDC <= 0) { 
	rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
	grounded = false; 
	jumpCDC = jumpCD;	
	}
}
public void OnSprint(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
	sprintInput = context.ReadValue<float>();
}
public void OnMouseMove(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
	mouseInput = context.ReadValue<Vector2>();
}
public void OnToggleCursor(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
	Cursor.lockState = ( Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.Confined : CursorLockMode.Locked;
}

}
