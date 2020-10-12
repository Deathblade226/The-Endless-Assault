using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour {
    
[SerializeField]float walkSpeed = 1;
[SerializeField]float roationSpeed = 1;

private float currentSpeed = 1;

private void Start() {
	currentSpeed = walkSpeed;		
}

public void Walk(CallbackContext context) { 
	Debug.Log("Walk hit");
}
public void Jump(CallbackContext context) { 
	Debug.Log("Jump hit");
}
public void Sprint(CallbackContext context) {
	Debug.Log("Sprint hit");
}

}
