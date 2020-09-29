using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : MonoBehaviour {

[SerializeField] DamageData m_data = null;
public DamageData data { get => m_data; set => m_data = value; }
public abstract void Spawn(Vector3 position, Vector3 direction, Vector3 normal);
}
