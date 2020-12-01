using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

[SerializeField] List<AudioSource> musicCollection = null;
[SerializeField] PhotonView pv = null;

private int spot = 0;
private bool changed = false;

public int Spot { get => spot; set => spot =  value ; }

private void Update() {
	if (!pv.IsMine) { gameObject.SetActive(false); return; }
	else musicCollection[Spot].enabled = true;
	
	spot = (GameObject.FindGameObjectWithTag("Monster") != null && SceneManagerHelper.ActiveSceneName != "Testing World") ? 1 : 0;

	if (spot == 1) { 
	musicCollection[0].enabled = false; 
	musicCollection[1].enabled = true; 
	} else if (spot == 0) { 
	musicCollection[0].enabled = true; 
	musicCollection[1].enabled = false; 
	}

	if (!musicCollection[spot].isPlaying) {
	musicCollection[spot].volume = 0f;
	musicCollection[spot].Play();
	for(float i = 0; i <= PlayerPrefs.GetFloat("MusicLevels") * 0.002f; i += 0.01f) {
	musicCollection[spot].volume = i;
	}
	}
	musicCollection[Spot].volume = PlayerPrefs.GetFloat("MusicLevels") * 0.002f;
}

}
