using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.InputSystem;

public class PlacementController : MonoBehaviourPun, IPunObservable {

[SerializeField] Text TowerDisplay = null;
[SerializeField] List<GameObject> Units = new List<GameObject>();
[SerializeField] LayerMask IgnoredLayers;
[SerializeField] List<String> tags;

private GameObject currentObject = null;
private int currentTower = -1;
private Vector2 mouseInput;
private float scrollInput;

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    if(stream.IsWriting) {
	//stream.SendNext(this.currentObject);
	stream.SendNext(this.currentTower);
	} else {
	//this.currentObject = (GameObject) stream.ReceiveNext();
	this.currentTower = (int) stream.ReceiveNext();
	}
}

void Update() {
    if (currentObject != null) {
    MovePlaceableToMouse();
    RotatePlaceable();
    }
}

private void DeleteUnit() {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    if (tags.Contains(hitInfo.collider.gameObject.tag)) { 
    PhotonNetwork.Destroy(hitInfo.collider.gameObject);
    }
    }
}

private void RotatePlaceable() {
    Quaternion rotation = currentObject.transform.rotation;
    currentObject.transform.Rotate(Vector3.up, scrollInput * .1f);
    //Debug.Log(currentObject.transform.rotation);
    scrollInput = 0;
}


private void MovePlaceableToMouse() {
    Ray ray = Camera.main.ScreenPointToRay(mouseInput);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    if (((1<<hitInfo.collider.gameObject.layer) & IgnoredLayers) == 0) { 
    currentObject.transform.position = hitInfo.point;
    currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    }
    }
}

public void PlaceObject(InputAction.CallbackContext context) {
    if (currentObject == null) return;
    currentObject.layer = LayerMask.NameToLayer("World");
    currentObject = null;
    //Game.Rebuild = true;
}
public void KeyZ(InputAction.CallbackContext context) { 
    DeleteUnit();
}
public void KeyOne(InputAction.CallbackContext context) { 
    if (currentTower == 0) { Destroy(); } 
    else { 
    Destroy();
    Spawn(0);
    }
}
public void KeyTwo(InputAction.CallbackContext context) { 
    if (currentTower == 1) { Destroy(); } 
    else { 
    Destroy();
    Spawn(1);
    }
}
public void KeyThree(InputAction.CallbackContext context) { 
    if (currentTower == 2) { Destroy(); } 
    else { 
    Destroy();
    Spawn(2);
    }
}
public void KeyFour(InputAction.CallbackContext context) { 
    if (currentTower == 3) { Destroy(); } 
    else { 
    Destroy();
    Spawn(3);
    }
}
public void KeyFive(InputAction.CallbackContext context) { 
    if (currentTower == 4) { Destroy(); } 
    else { 
    Destroy();
    Spawn(4);
    }
}
public void KeySix(InputAction.CallbackContext context) { 
    if (currentTower == 5) { Destroy(); } 
    else { 
    Destroy();
    Spawn(5);
    }
}
public void KeySeven(InputAction.CallbackContext context) { 
    if (currentTower == 6) { Destroy(); } 
    else { 
    Destroy();
    Spawn(6);
    }
}
public void KeyEight(InputAction.CallbackContext context) { 
    if (currentTower == 7) { Destroy(); } 
    else { 
    Destroy();
    Spawn(7);
    }
}
public void KeyNine(InputAction.CallbackContext context) { 
    if (currentTower == 8) { Destroy(); } 
    else { 
    Destroy();
    Spawn(8);
    }
}

public void OnMouseMove(InputAction.CallbackContext context) { 
	mouseInput = context.ReadValue<Vector2>();
}
public void OnMouseScroll(InputAction.CallbackContext context) { 
	scrollInput = context.ReadValue<float>();
}

private void Spawn(int key) { 
    if (Units.Count > key) { 
    currentTower = key;
    currentObject = PhotonNetwork.Instantiate(Units[currentTower].name, new Vector3(), Quaternion.identity);
    }
}
private void Destroy() {
    if (currentObject != null) PhotonNetwork.Destroy(currentObject);
    currentTower = -1;
}

}
