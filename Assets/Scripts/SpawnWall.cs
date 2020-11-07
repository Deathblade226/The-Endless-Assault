using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWall : MonoBehaviour {

[SerializeField] List<string> tags;

private void OnCollisionEnter(Collision collision) {
	if (tags.Contains(collision.gameObject.tag)) { collision.gameObject.transform.Translate(collision.gameObject.transform.forward/5); }
}

}
