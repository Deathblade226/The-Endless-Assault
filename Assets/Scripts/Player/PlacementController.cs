using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.InputSystem;

public class PlacementController : MonoBehaviour {

[SerializeField] Text TowerDisplay = null;
[SerializeField] List<GameObject> Units = new List<GameObject>();
[SerializeField] PhotonView PV = null;
[SerializeField] LayerMask IgnoredLayers;

private GameObject currentObject = null;
private float rotation;
private int currentTower = -1;

private void Start() {
    if (!PV.IsMine) return;    
    //Camera.main.transform.position = transform.position;
    //Camera.main.transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    //Camera.main.transform.parent = this.transform;
}

void Update() {
    if (!PV.IsMine) return;

    if (currentObject != null) {
    PV.RPC("MovePlaceableToMouse", RpcTarget.All);
    //MovePlaceableToMouse();
    PV.RPC("RotatePlaceable", RpcTarget.All);
    //RotatePlaceable();It
    PV.RPC("SpawnObject", RpcTarget.All);
    //SpawnObject();
    }

}

[PunRPC]
private void DeleteUnit() {
    if (!PV.IsMine) return;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    if (hitInfo.collider.gameObject.tag == "Blue-Unit" || hitInfo.collider.gameObject.tag == "Red-Unit") { 
    if (PV.IsMine) PhotonNetwork.Destroy(hitInfo.collider.gameObject);
    }
    }
}

[PunRPC]
private void SpawnObject() {
    if (!PV.IsMine) return;
    if (Input.GetMouseButtonDown(0)) {
    //GameObject unit = currentObject;
    //currentObject.layer = 0;
    currentObject.GetComponent<CapsuleCollider>().enabled = true;
    currentObject = null;
    //Game.Rebuild = true;
    }
}

[PunRPC]
private void RotatePlaceable() {
    if (!PV.IsMine) return;
    rotation += Input.mouseScrollDelta.y;
    currentObject.transform.Rotate(Vector3.up, rotation * 10f);
}

[PunRPC]
private void MovePlaceableToMouse() {
    if (!PV.IsMine) return;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo)) {
    
    if (((1<<hitInfo.collider.gameObject.layer) & IgnoredLayers) == 0) { 
    currentObject.transform.position = hitInfo.point;
    currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    }
    }
}

public void KeyZ(InputAction.CallbackContext context) { 
    if (currentObject == null) { PV.RPC("DeleteUnit", RpcTarget.All); }
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
