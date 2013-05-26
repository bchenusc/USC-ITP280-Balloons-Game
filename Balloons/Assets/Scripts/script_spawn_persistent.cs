using UnityEngine;
using System.Collections;

public class script_spawn_persistent : MonoBehaviour {
	
	public Transform persistentPref;
	
	void Awake () {
		if (GameObject.Find("Persistent(Clone)") == null){
			Instantiate(persistentPref, Vector3.zero, Quaternion.identity);
		}
	}
}
