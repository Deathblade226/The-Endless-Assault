using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCluster : MonoBehaviour {

[SerializeField] GameObject monster;
[SerializeField] float count;
[SerializeField] float spawnCD;

public GameObject Monster { get => monster; }
public float Count { get => count; }
public float SpawnCD { get => spawnCD; }

}
