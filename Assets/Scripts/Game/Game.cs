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
[SerializeField]TextMeshProUGUI statTracker = null;
[SerializeField]GameObject endScreen = null;
[SerializeField]TextMeshProUGUI endScreenMessage = null;

[Header("Loose Conditions")]
[SerializeField] List<GameObject> objectives;

public static Game game;

private float fpsUpdateTime = 1;

public bool GameRunning { get; set; } = false;
public int Currency { get => currency; set => currency =  value ; }
public PhotonView Pv { get => pv; set => pv =  value ; }

private void Start() { 
	game = this; 
}

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	if(stream.IsWriting) {
	stream.SendNext(this.Currency);
	} else {
	this.Currency = (int) stream.ReceiveNext();
	}
}

void Update() {
	currencyDisplay.text = $"{currency}";
	Spawner spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
	if (spawner.Waves.Count <= spawner.Wave && GameObject.FindGameObjectsWithTag("Monster") == null && Game.game.objectives[0] != null) { 
	Game.game.endScreenMessage.text = "Victory";
	Game.game.endScreen.SetActive(true); 
	} else if (Game.game.objectives[0] == null) {
	Game.game.endScreenMessage.text = "Defeat";
	Game.game.endScreen.SetActive(true); 
	}
}

private void LateUpdate() {
	if (pv.IsMine && fpsUpdateTime <= 0) { 
	float ms = PhotonNetwork.GetPing();
	int fps = (int)(1.0f/Time.deltaTime);
	if (statTracker != null) statTracker.text = $"FPS: {fps} MS: {ms}";
	fpsUpdateTime = 1;
	} else { fpsUpdateTime -= Time.deltaTime; }
}

[PunRPC]
public void ModifyCurrency(int input) {
	if (Currency - input >= 0) { Currency -= input; }
}

}
