using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Security.Cryptography;

public class PlacementController : MonoBehaviourPun, IPunObservable {

[SerializeField] PhotonView pv;
[SerializeField] Text TowerDisplay = null;
[SerializeField] float rotationSpeed = 1;
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
    if (!pv.IsMine) return;
    if (currentObject != null) {
    MovePlaceableToMouse();
    RotatePlaceable();
    }
}
private void DeleteUnit() {
    if (!pv.IsMine) return;
    Ray ray = Camera.main.ScreenPointToRay(mouseInput);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    if (tags.Contains(hitInfo.collider.gameObject.tag)) { 
    PhotonNetwork.Destroy(hitInfo.collider.gameObject);
    UpdateNav();
    }
    }
}
private void RotatePlaceable() {
    if (!pv.IsMine) return;
    currentObject.transform.Rotate(Vector3.up, (scrollInput / 12) * rotationSpeed);
    scrollInput = 0;
}
private void MovePlaceableToMouse() {
    if (!pv.IsMine) return;
    Ray ray = Camera.main.ScreenPointToRay(mouseInput);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    if (((1<<hitInfo.collider.gameObject.layer) & IgnoredLayers) == 0) { 
    currentObject.transform.position = hitInfo.point;
    //currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    }
    }
}
public void PlaceObject(InputAction.CallbackContext context) {
    if (!pv.IsMine) return;
    if (this.currentObject != null) {
    pv.RPC("UpdateNav", RpcTarget.All);
    pv.RPC("SettingValues", RpcTarget.All, currentObject.GetComponent<PhotonView>().ViewID);
    this.currentObject = null;
    }
}
[PunRPC]
public void SettingValues(int id) { 
    GameObject go = PhotonView.Find(id).gameObject;
    go.layer = LayerMask.NameToLayer("World");
    VisionSystem vs = go.GetComponent<VisionSystem>();
    if(vs == null) { vs = go.transform.GetComponentInChildren<VisionSystem>(true); }
    vs.Active = true;
}

public void KeyZ(InputAction.CallbackContext context) { 
    if (!pv.IsMine) return;
    Ray ray = Camera.main.ScreenPointToRay(mouseInput);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    PhotonNetwork.Destroy(hitInfo.collider.gameObject);
    }
}
public void KeyOne(InputAction.CallbackContext context) { 
    if (!pv.IsMine) return;    
    if (currentTower == 0) { Destroy(); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 0);
    }
}
public void KeyTwo(InputAction.CallbackContext context) { 
    if (!pv.IsMine) return;
    if (currentTower == 1) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 1);
    }
}
public void KeyThree(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 2) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 2);
    }
}
public void KeyFour(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 3) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 3);
    }
}
public void KeyFive(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 4) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 4);
    }
}
public void KeySix(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 5) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 5);
    }
}
public void KeySeven(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 6) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 6);
    }
}
public void KeyEight(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 7) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 7);    
    }
}
public void KeyNine(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
    if (currentTower == 8) { pv.RPC("Destroy", RpcTarget.All); } 
    else { 
    pv.RPC("Destroy", RpcTarget.All);
    //Destroy();
    pv.RPC("Spawn", RpcTarget.All, 8);
    //Spawn(8);
    }
}

public void OnMouseMove(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
	mouseInput = context.ReadValue<Vector2>();
}
public void OnMouseScroll(InputAction.CallbackContext context) { 
	if (!pv.IsMine) return;
	scrollInput = context.ReadValue<float>();
}

[PunRPC]
private void Spawn(int key) {
    if (!pv.IsMine) return;
    if (Units.Count > key) { 
    currentTower = key;
    currentObject = PhotonNetwork.Instantiate(Units[currentTower].name, new Vector3(), Quaternion.identity);
    }
}
[PunRPC]
private void Destroy() {
    if (currentObject != null) 
    PhotonNetwork.Destroy(currentObject);
    currentTower = -1;
}
[PunRPC]
private void UpdateNav() { 
    if (!pv.IsMine) return;
    if (GameObject.FindGameObjectsWithTag("NavMesh").Length != 0) GameObject.FindGameObjectsWithTag("NavMesh")[0].GetComponent<NavMeshSurface>().BuildNavMesh();
}

}
