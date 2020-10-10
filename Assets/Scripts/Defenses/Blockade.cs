using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : Defense {

private void Update() {
	GameObject target = gameObject.GetComponent<VisionSystem>().SeenTarget;
	
	if (target != null) {
	tag = "Defense";
	} else { tag = "Untagged"; }

}

}
