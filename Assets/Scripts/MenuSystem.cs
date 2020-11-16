using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {

[Header("Menus")]
[SerializeField] GameObject titleScreen = null;
[SerializeField] GameObject lobbyMenu = null;
[SerializeField] GameObject optionsMenu = null;
[SerializeField] GameObject helpMenu = null;

[Header("Settings")]
[SerializeField] Slider music = null;
[SerializeField] Slider sfx = null;

[Header("Lobby")]
[SerializeField] GameLobby lobbyObject = null;

[Header("Map Display")]
[SerializeField] Dropdown dropdown = null;
[SerializeField] List<GameObject> mapImages;


public void Start() {
	if (!PlayerPrefs.HasKey("MusicLevels")) { PlayerPrefs.SetFloat("MusicLevels", 100); }
	else { music.value = PlayerPrefs.GetFloat("MusicLevels"); }
	
	if (!PlayerPrefs.HasKey("SFXLevels")) { PlayerPrefs.SetFloat("SFXLevels", 100); }
	else { sfx.value = PlayerPrefs.GetFloat("SFXLevels"); }
}

public void Update() {
	mapImages.ForEach(g => g.SetActive(false));
	mapImages[dropdown.value].SetActive(true);
}

public void ShowTitle() {
	titleScreen.SetActive(true);
	lobbyMenu.SetActive(false);
	optionsMenu.SetActive(false);
	lobbyObject.GUIActive = false;
}
public void ShowLobby() { 
	lobbyMenu.SetActive(true);
	titleScreen.SetActive(false);
	optionsMenu.SetActive(false);
	lobbyObject.GUIActive = true;
}
public void ShowOptions() { 
	titleScreen.SetActive(false);
	lobbyMenu.SetActive(false);
	optionsMenu.SetActive(true);
	lobbyObject.GUIActive = false;
}
public void ToggleHelp() { 
	helpMenu.SetActive(!helpMenu.activeSelf);
}

}
