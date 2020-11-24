using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarsTracker : MonoBehaviour {

[Header("Photon")]
[SerializeField] PhotonView PV = null;

[Header("Display")]
[SerializeField] Slider healthBar = null;
[SerializeField] Image fillArea = null;
[SerializeField] TextMeshProUGUI m_healthText = null;

[Header("Damagable")]
[SerializeField] Damagable damagable = null;

void Update() {
	if (!PV.IsMine) return;
	healthBar.minValue = 0;
	healthBar.maxValue = damagable.MaxHealth;
    healthBar.value = damagable.health; 
	damagable.HideObject.SetActive(false);
    healthBar.value = damagable.health;       
	m_healthText.text = $"{damagable.health}/{damagable.MaxHealth}";
	Color color = new Color();
	float percent = damagable.health/damagable.MaxHealth;
	if (percent <= 0.25f) { color = damagable.TwoFive; }
	else if (percent <= 0.5f) { color = damagable.Five; }
	else if (percent <= 0.75f) { color = damagable.SevenFive; }
	else { color = damagable.OneHundred; }
	fillArea.color = color;	
}

}
