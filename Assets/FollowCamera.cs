using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

[SerializeField]Transform target = null;

void Update() {
    Camera.main.transform.transform.position = transform.position;        
    Camera.main.transform.LookAt(target);
}

}
