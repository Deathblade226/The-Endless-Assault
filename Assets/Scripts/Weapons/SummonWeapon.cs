using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : Weapon {

[SerializeField] GameObject summonMonster = null;
[SerializeField] int maxSummons = 1;
[SerializeField] float summonDistance = 1;

private bool canSummon = false;
private List<GameObject> Summons = new List<GameObject>();

void Awake() { Type = "Summon"; }

public override void Attack() { canSummon = true; }

private void Update() {
    if (!PV.IsMine) return;
    PV.RPC("RPC_Summon", RpcTarget.All);
}

[PunRPC]
void RPC_Summon() { 
    if (Summons.Count < maxSummons && canSummon && attack.Nc.Animator.GetCurrentAnimatorStateInfo(0).IsName("Standing 2H Magic Area Attack 01")) {
    canSummon = false;
    StartCoroutine(SummonMonster(attack.Nc.Animator.GetCurrentAnimatorStateInfo(0).length));
    }
}

IEnumerator SummonMonster(float time) {
    yield return new WaitForSeconds(time/2);
    GameObject sum = GameObject.Instantiate(summonMonster);
    sum.transform.position = new Vector3(transform.position.x, transform.transform.position.y, transform.position.z);
    sum.transform.position += transform.forward * summonDistance;
    Summons.Add(sum);
    StopCoroutine(SummonMonster(0));
yield return null;}

}
