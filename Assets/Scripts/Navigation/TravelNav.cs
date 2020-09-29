using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class TravelNav : MonoBehaviour {

[SerializeField] string targetTag = "";
[SerializeField] NavigationController nc = null;
[SerializeField] int value = 0;
public bool Moving { get; set; }
public string TargetTag { get => targetTag; set => targetTag = value; }
public NavigationController Nc { get => nc; set => nc = value; }
public GameObject Target { get => target; set => target = value; }
public int Value { get => value; set => this.value = value; }

private GameObject target = null;

[PunRPC]
public void StartTravel() {
    if (Target == null) Target = AIUtilities.GetNearestGameObject(gameObject, TargetTag, xray:true);
    Moving = true; 
    nc.Agent.SetDestination(Target.transform.position);
}

}
