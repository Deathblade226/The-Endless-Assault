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

public void Start() {
	if (PlayerPrefs.HasKey("MusicLevels")) { PlayerPrefs.SetFloat("MusicLevels", 100); }
	if (PlayerPrefs.HasKey("SFXLevels")) { PlayerPrefs.SetFloat("SFXLevels", 100); }
}

public void ShowTitle() {
	titleScreen.SetActive(true);
	lobbyMenu.SetActive(false);
	optionsMenu.SetActive(false);
}
public void ShowLobby() { 
	titleScreen.SetActive(false);
	lobbyMenu.SetActive(true);
	optionsMenu.SetActive(false);
}
public void ShowOptions() { 
	titleScreen.SetActive(false);
	lobbyMenu.SetActive(false);
	optionsMenu.SetActive(true);
}
public void ToggleHelp() { 
	helpMenu.SetActive(!helpMenu.activeSelf);
}

}
