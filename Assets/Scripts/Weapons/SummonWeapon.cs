using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : Weapon {

[SerializeField] GameObject summonMonster = null;
[SerializeField] int maxSummons = 1;
[SerializeField] float summonDistance = 1;
[SerializeField] float summonTime = 1;
[SerializeField] float summonWait = 1;
[SerializeField] NavigationControllerMP nc = null;

private float summonCD = 0;
private bool canSummon = false;
private List<GameObject> Summons = new List<GameObject>();

private void Start() {
    summonCD = summonTime;		
}

private void Update() {
    if (!PV.IsMine && !PhotonNetwork.IsMasterClient) return;
    for (int i = 0; i < Summons.Count; i++) { if (Summons[i] == null) { Summons[i] = Summons[Summons.Count - 1]; Summons.RemoveAt(Summons.Count - 1); } } 

    if (Summons.Count != maxSummons && summonCD <= 0) {
    nc.Agent.isStopped = true;
    nc.Animator.SetTrigger("Attack");
    summonCD = summonTime;
    int count = (Summons.Count == maxSummons) ? 0 : Random.Range(1, maxSummons - Summons.Count);
    for (int i = 0; i < count; i++) {
    Vector3 offset = new Vector3(Random.Range(-summonDistance, summonDistance), 0, Random.Range(-summonDistance, summonDistance));
    GameObject spawn = PhotonNetwork.Instantiate(summonMonster.gameObject.name, transform.position + offset, Quaternion.identity);
    Summons.Add(spawn);
    }
    } else if (summonCD > 0) { 
    summonCD -= Time.deltaTime; 
    if (summonCD < summonTime - summonWait) { nc.Agent.isStopped = false; }
    }
}

}
