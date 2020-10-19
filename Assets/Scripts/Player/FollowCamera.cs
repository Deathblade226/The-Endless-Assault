using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

[SerializeField]Transform target = null;
[SerializeField]PhotonView pv = null;

private void Start() {
	if (!pv.IsMine) return;
	//Camera.main.transform.position = transform.position;
	//Camera.main.transform.parent = gameObject.transform;
}

void Update() {
	if (!pv.IsMine) return;
    Camera.main.transform.transform.position = transform.position;        
    Camera.main.transform.LookAt(target);
}

private void OnDestroy() {
		
}

}
