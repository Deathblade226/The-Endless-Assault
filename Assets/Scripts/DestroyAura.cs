using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAura : MonoBehaviour {

[SerializeField] PhotonView PV = null;
[SerializeField] float range = 5;
[SerializeField] string[] tags;

void Update() {
    if (!PV.IsMine && tags.Length == 0) return;
    foreach(string tag in tags) { 
    GameObject unit = AIUtilities.GetNearestGameObject(gameObject, tag, range);
    if (unit != null) PhotonNetwork.Destroy(unit);
    }
}

}
