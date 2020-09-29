using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour {

public void ExitGame() { 
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
    Application.OpenURL(webplayerQuitURL);
    #else   
    Application.Quit();
    #endif
}

public void LoadScene(string SceneName) { SceneManager.LoadScene(SceneName); }
public static void LoadSceneFunction(string SceneName) { SceneManager.LoadScene(SceneName); }

public static int GetIntValue(string name) { 
return PlayerPrefs.GetInt(name);}
public static float GetFloatValue(string name) { 
return PlayerPrefs.GetFloat(name);}
public static string GetStringValue(string name) { 
return PlayerPrefs.GetString(name);}

public static void SetValue(string name, int value) { PlayerPrefs.SetInt(name, value); }
public static void SetValue(string name, float value) { PlayerPrefs.SetFloat(name, value); }
public static void SetValue(string name, string value) { PlayerPrefs.SetString(name, value); }

}
