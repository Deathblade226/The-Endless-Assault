using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseSelect : MonoBehaviour {

[Header("Defenses")]
[SerializeField] List<Sprite> DefensesImages;
[SerializeField] List<GameObject> DefenseSlots;
[Header("UI")]
[SerializeField] List<GameObject> SelectionHighlight;

private int autoSelect = 0;

void Start() {
    if (!PlayerPrefs.HasKey("Defense1")) { PlayerPrefs.SetInt("Defense1", 0); }
    if (!PlayerPrefs.HasKey("Defense2")) { PlayerPrefs.SetInt("Defense2", 1); }
    if (!PlayerPrefs.HasKey("Defense3")) { PlayerPrefs.SetInt("Defense3", 2); }
    if (!PlayerPrefs.HasKey("Defense4")) { PlayerPrefs.SetInt("Defense4", 3); }
    DefenseSlots[0].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense1")];
    DefenseSlots[1].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense2")];
    DefenseSlots[2].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense3")];
    DefenseSlots[3].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense4")];
}
void Update() {
    //Debug.Log(PlayerPrefs.GetInt("Defense1"));
    //Debug.Log(PlayerPrefs.GetInt("Defense2"));
    //Debug.Log(PlayerPrefs.GetInt("Defense3"));
    //Debug.Log(PlayerPrefs.GetInt("Defense4"));
    DefenseSlots[0].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense1")];
    DefenseSlots[1].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense2")];
    DefenseSlots[2].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense3")];
    DefenseSlots[3].GetComponent<Image>().sprite = DefensesImages[PlayerPrefs.GetInt("Defense4")];
}

public void ChangeDefense(int selected) { 
    if (selected < DefensesImages.Count) { 
    switch(autoSelect) { 
    case 0: PlayerPrefs.SetInt("Defense1", selected); autoSelect = 1; break;
    case 1: PlayerPrefs.SetInt("Defense2", selected); autoSelect = 2; break;
    case 2: PlayerPrefs.SetInt("Defense3", selected); autoSelect = 3; break;
    case 3: PlayerPrefs.SetInt("Defense4", selected); autoSelect = 0; break;
    default: Debug.LogError($"{autoSelect} is larger then 3 or less then 0"); break;
    }
    foreach(GameObject go in SelectionHighlight) { go.SetActive(false); }
    }
}
public void SelectDefense(int selected) { 
    foreach(GameObject go in SelectionHighlight) { go.SetActive(false); }
    SelectionHighlight[selected].SetActive(true);
    autoSelect = selected;
}

public void SetDefault() {
    PlayerPrefs.SetInt("Defense1", 0);
    PlayerPrefs.SetInt("Defense2", 1);
    PlayerPrefs.SetInt("Defense3", 2);
    PlayerPrefs.SetInt("Defense4", 3);
}

}
