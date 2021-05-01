using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class DefenseDamageTypes : DamageTypes {
	[Range(-1,1)] public float resistance;
}

[System.Serializable] public class ImmuneDamageTypes : DamageTypes { 

}

[System.Serializable] public class DamageTypes {
	public Type type;
	public List<Element> damageTypes;
}

[System.Serializable] public enum Type {
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