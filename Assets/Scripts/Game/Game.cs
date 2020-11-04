using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Game : MonoBehaviourPun, IPunObservable {

[Header("Photon")]
[SerializeField]PhotonView pv = null;

[Header("Starting Values")]
[SerializeField] int currency = 0;

[Header("UI Elements")]
[SerializeField]TextMeshProUGUI currencyDisplay = null;

public static Game game;

public bool GameRunning { get; set; } = false;
public int Currency { get => currency; set => currency =  value ; }
public PhotonView Pv { get => pv; set => pv =  value ; }

private void Start() { game = this; }

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	if(stream.IsWriting) {
	stream.SendNext(this.Currency);
	} else {
	this.Currency = (int) stream.ReceiveNext();
	}
}

void Update() {
	currencyDisplay.text = $"{currency}";
}

[PunRPC]
public void ModifyCurrency(int input) {
	if (Currency - input >= 0) { Currency -= input; }
}

}
