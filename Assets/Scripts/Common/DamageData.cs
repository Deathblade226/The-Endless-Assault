using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Weapon System/Damage")]
public class DamageData : ScriptableObject
{
	[Range(0, 100)] public float amount = 0;
	[Range(0, 100)] public float innerRadius = 0;
	[Range(0, 100)] public float outerRadius = 0;
	public LayerMask layer = ~0;

	[Range(0, 500)] public float force = 0;
	[Range(0, 100)] public float upwardForce = 0;
	public bool isExplosion = false;

	public bool orientToNormal = true;
	[Range(0, 1)] public float zOffset = 0;
}
