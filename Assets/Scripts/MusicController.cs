using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

[SerializeField] List<AudioSource> musicCollection = null;
[SerializeField] PhotonView pv = null;

private int spot = 0;

public int Spot { get => spot; set => spot =  value ; }

private void Update() {
	if (!pv.IsMine) { gameObject.SetActive(false); return; }
	else musicCollection[Spot].enabled = true;
	
	spot = (GameObject.FindGameObjectWithTag("Monster") != null) ? 1 : 0;

	if (spot == 1) { musicCollection[0].enabled = false; musicCollection[1].enabled = true; }
	if (spot == 0) { musicCollection[0].enabled = true; musicCollection[1].enabled = false; }

	musicCollection[Spot].volume = PlayerPrefs.GetFloat("MusicLevels") * 0.002f;
}

}
