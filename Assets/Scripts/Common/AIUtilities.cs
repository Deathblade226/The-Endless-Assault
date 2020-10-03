using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtilities {

public static GameObject GetNearestGameObject(GameObject source, string tagName, float radius = float.MaxValue, float angle = 180.0f, bool xray = false) { 
    GameObject nearestGO = null;
    //Debug.Log(tagName);
    if (tagName != "") { 

    Collider[] colliders =  Physics.OverlapSphere(source.transform.position, radius);

    float nearestDistance = float.MaxValue;

    foreach(Collider collider in colliders) { 

    if (collider.tag != tagName) { continue; }

    Vector3 targetDir = collider.transform.position - source.transform.position;
    float angleDif = Vector3.Angle(targetDir, source.transform.forward);

    float distance = (source.transform.position - collider.transform.position).sqrMagnitude;

    bool valid = true;
    if (!xray && Physics.Raycast(source.transform.position, targetDir.normalized, out RaycastHit hit, radius)) { valid = hit.collider.gameObject.tag == tagName; }

    if (valid && collider.gameObject != source && collider.gameObject.CompareTag(tagName) && distance < nearestDistance && angleDif <= angle) { 
    nearestGO = collider.gameObject;
    nearestDistance = distance;
    }
    }
    }
return nearestGO;}

public static GameObject[] GetGameObjects(GameObject source, string tagName, float radius = float.MaxValue, float angle = 180.0f) { 
    List<GameObject> gameObjects = new List<GameObject>();
    if (tagName != "") { 
    Collider[] colliders =  Physics.OverlapSphere(source.transform.position, radius);
    if (colliders.Length != 0) { 
    foreach(Collider collider in colliders) { 
    
    Vector3 targetDir = collider.transform.position - source.transform.position;
    float angleDif = Vector3.Angle(targetDir, source.transform.forward);

    if (collider.gameObject != source && collider.gameObject.CompareTag(tagName) && angleDif <= angle) gameObjects.Add(collider.gameObject); 
        
    }
    }
    }

return gameObjects.ToArray();}

public static Vector3 WrapPosition(Vector3 position, Vector3 min, Vector3 max) {
		Vector3 newPosition = position;

		if (position.x > max.x) newPosition.x = min.x + (position.x - max.x);
		if (position.x < min.x) newPosition.x = max.x - (min.x - position.x);
		if (position.y > max.y) newPosition.y = min.y + (position.y - max.y);
		if (position.y < min.y) newPosition.y = max.y - (min.y - position.y);
		if (position.z > max.z) newPosition.z = min.z + (position.z - max.z);
		if (position.z < min.z) newPosition.z = max.z - (min.z - position.z);

		return newPosition;
}

}
