using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Game : MonoBehaviour {

[SerializeField]PhotonView PV = null;
[SerializeField]TextMeshProUGUI blueCounter = null;
[SerializeField]TextMeshProUGUI redCounter = null;
[SerializeField]TextMeshProUGUI blueWins = null;
[SerializeField]TextMeshProUGUI redWins = null;

[SerializeField]GameObject winPanel = null;
[SerializeField]TextMeshProUGUI winMsg = null;

    public static Game game;
public bool GameRunning { get; set; } = false;

private void Start() { game = this; }

private int BlueWins = 0;
private int RedWins = 0;

void Update() {
    Debug.Log($"Name: {PhotonNetwork.NickName} | Ping: {PhotonNetwork.GetPing()}");
    if (blueWins.text != "Wins: " + BlueWins) blueWins.text = "Wins: " + BlueWins;
    if (redWins.text != RedWins + " :Wins") redWins.text = RedWins + " :Wins";

    if(!GameRunning) {
    blueCounter.text = "Blue: " + GameObject.FindGameObjectsWithTag("Blue-Unit").Length;
    redCounter.text = GameObject.FindGameObjectsWithTag("Red-Unit").Length + ":Red";
    }

    if (PhotonNetwork.MasterClient != null && PhotonNetwork.MasterClient.IsLocal && !GameRunning) {    
    if (Input.GetKey(KeyCode.Space)) { PV.RPC("ToggleGame", RpcTarget.All); }    
    }
    
    int redCount = GameObject.FindGameObjectsWithTag("Red-Unit").Length;
    int blueCount = GameObject.FindGameObjectsWithTag("Blue-Unit").Length;

    if ((redCount == 0 || blueCount == 0) && GameRunning) { 
    
    if (redCount == 0) { BlueWins++; winMsg.text = "Blue Victory"; winPanel.SetActive(true); }
    else if (blueCount == 0) { RedWins++; winMsg.text = "Red Victory"; winPanel.SetActive(true); }

    PV.RPC("ToggleGame", RpcTarget.All); ClearUnits("Both"); 
    }

}
public void HidePanel() {
    winPanel.SetActive(false);
}

public void ClearUnits(string type) {
    switch(type) {
    case "Red":
    DestroySelection("Red-Unit");
    break;
    case "Blue": 
    DestroySelection("Blue-Unit");
    break;
    case "Both": 
    DestroySelection("Blue-Unit");
    DestroySelection("Red-Unit");
    break;
    }
}

[PunRPC]
private void ToggleGame() { 
    GameRunning = (GameRunning) ? false : true;
}

private void DestroySelection(string type) {
    GameObject[] gos = GameObject.FindGameObjectsWithTag(type);
    if (gos.Length == 0) return;
    foreach(GameObject go in gos) {
    if (PV.IsMine) PhotonNetwork.Destroy(go);
    }
}

}
