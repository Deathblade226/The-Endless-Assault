using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

[SerializeField]Transform target = null;

private void Start() {
	Camera.main.transform.position = transform.position;
	Camera.main.transform.parent = gameObject.transform;
}

void Update() {
    //Camera.main.transform.transform.position = transform.position;        
    Camera.main.transform.LookAt(target);
}

	private void OnDestroy() {
		
}

}
