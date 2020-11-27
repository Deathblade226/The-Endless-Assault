using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseController : MonoBehaviour {

[Header("Photon View")]
[SerializeField] PhotonView pv = null;

[Header("Placement Controller")]
[SerializeField] PlacementController pc = null;

[Header("Defenses")]
[SerializeField] List<GameObject> DefensesObjects;
[SerializeField] List<Sprite> DefensesImages;

private GameObject d1 = null;
private GameObject d2 = null;
private GameObject d3 = null;
private GameObject d4 = null;

private void Awake() {
	if (!pv.IsMine) return;
	d1 = GameObject.FindGameObjectWithTag("D1");
	int defense1 = (PlayerPrefs.HasKey("Defense1") ) ? PlayerPrefs.GetInt("Defense1") : 0;
	UpdateDefenses(defense1, 0);
	d2 = GameObject.FindGameObjectWithTag("D2");
	int defense2 = (PlayerPrefs.HasKey("Defense2") ) ? PlayerPrefs.GetInt("Defense2") : 1;
	UpdateDefenses(defense2, 1);
	d3 = GameObject.FindGameObjectWithTag("D3");
	int defense3 = (PlayerPrefs.HasKey("Defense3") ) ? PlayerPrefs.GetInt("Defense3") : 2;
	UpdateDefenses(defense3, 2);
	d4 = GameObject.FindGameObjectWithTag("D4");
	int defense4 = (PlayerPrefs.HasKey("Defense4") ) ? PlayerPrefs.GetInt("Defense4") : 3;	
	UpdateDefenses(defense4, 3);
}

public void UpdateDefenses(int defense, int spot) {
	pc.Defenses[spot] = DefensesObjects[defense];
	switch(spot) { 
	case 0: d1.GetComponent<Image>().sprite = DefensesImages[defense]; break;
	case 1: d2.GetComponent<Image>().sprite = DefensesImages[defense]; break;
	case 2: d3.GetComponent<Image>().sprite = DefensesImages[defense]; break;
	case 3: d4.GetComponent<Image>().sprite = DefensesImages[defense]; break;
	default: Debug.LogError($"{spot} is not 0-3"); break;
	}
}

}
