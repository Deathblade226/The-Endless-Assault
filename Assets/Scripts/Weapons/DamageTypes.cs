using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class DefenseDamageTypes : DamageTypes {
	[Tooltip("Range of damage resistance. The lower the number to higher damage and the higher the lower the damage up to .99 (99% of damage). Ex: -1 = 200% damage taken")]
	[Range(float.MinValue,.99f)] public float resistance;
}

[System.Serializable] public class ImmuneDamageTypes : DamageTypes { 

}

[System.Serializable] public class DamageTypes {
	public Type type;
	public List<Element> damageTypes;
}

[System.Serializable] public enum Type {
	All,
	Void,
	Physical,
	Melee,
	Explosive
}

[System.Serializable] public enum Element {
	Holy,
	Necrotic,
	Fire,
	Water,
	Wind,
	Earth,
	Acid,
	Poison,
	Shock,
	Cold,
	Slashing,
	Bludgeoning,
	Piercing
}