using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[Serializable] public class UnitTypes {

public List<GameObject> variants;

}

public class PlacementController : MonoBehaviour {

[SerializeField] Text TowerDisplay = null;
[SerializeField] List<UnitTypes> Units = new List<UnitTypes>();
[SerializeField] PhotonView PV = null;
[SerializeField] LayerMask IgnoredLayers;

private GameObject currentObject = null;
private float rotation;
private int currentTower = -1;
private int side = 0;

private void Start() {
    if (!PV.IsMine) return;    
    Camera.main.transform.position = transform.position;
    Camera.main.transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    Camera.main.transform.parent = this.transform;
}

void Update() {
    if (!PV.IsMine) return;
    if (Game.game.GameRunning) return;


    PV.RPC("CreatePlacable", RpcTarget.All);
    //CreatePlacable();
    
    //if (TowerDisplay != null) { 
    //string display = "Tower: ";   
    //display += (currentObject != null) ? $"{currentObject.GetComponent<DefenseTD>().TowerName} \nCost: {currentObject.GetComponent<DefenseTD>().Cost}" : "None";
    //TowerDisplay.text = display;
    //}

    //if (!Game.game.Paused && BuildCam != null) Cursor.lockState = (BuildCam.gameObject.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;

    if (currentObject != null) {
    PV.RPC("MovePlaceableToMouse", RpcTarget.All);
    //MovePlaceableToMouse();
    PV.RPC("RotatePlaceable", RpcTarget.All);
    //RotatePlaceable();It
    PV.RPC("SpawnObject", RpcTarget.All);
    //SpawnObject();
    }

    if (currentObject == null && Input.GetKey(KeyCode.Z) && !Game.game.GameRunning) { 
    PV.RPC("DeleteUnit", RpcTarget.All);
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
    // Blue <- 0 -> Red
    if (currentObject.transform.position.x > 0) { 
    PhotonNetwork.Destroy(currentObject);
    side = 1;
    currentObject = PhotonNetwork.Instantiate(Units[currentTower].variants[side].name, currentObject.transform.position, currentObject.transform.rotation); 
    }
    else if (currentObject.transform.position.x < 0) {
    PhotonNetwork.Destroy(currentObject);
    side = 0;
    currentObject = PhotonNetwork.Instantiate(Units[currentTower].variants[side].name, currentObject.transform.position, currentObject.transform.rotation); 
    }
}

[PunRPC]
private void CreatePlacable() {
    if (!PV.IsMine) return;
    StartCoroutine(crCreatePlaceAble());
}

IEnumerator crCreatePlaceAble() {
    for (int i = 0; i < Units.Count; i++) {

    if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i)) { 

    if (Pressed(i)) { 
    PhotonNetwork.Destroy(currentObject);
    //Destroy(currentObject); 
    currentTower = -1; 
    
    } else { 
    
    if (currentObject != null) { 
    PhotonNetwork.Destroy(currentObject);
    //Destroy(currentObject); 
    }    
    currentTower = i;
    currentObject = PhotonNetwork.Instantiate(Units[currentTower].variants[side].name, new Vector3(), Quaternion.identity);
    //currentObject = Instantiate(placeableObjects[i]);
    }
    break;
    }
    }
    StopCoroutine("crCreatePlaceAble");
    yield return null;
}

private bool Pressed(int i) { 
    //Debug.Log("I: " + i); 
    //Debug.Log("ID: " + currentTower); 
    //Debug.Log(currentTower % ( placeableObjects.Count / 2 )); 
return currentObject != null && currentTower == i; }

}
