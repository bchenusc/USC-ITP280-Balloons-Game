using UnityEngine;
using System.Collections;

public class script_spawn_persistent : MonoBehaviour {
	
	public Transform persistentPref;
	
	// Use this for initialization
	void Start () {
		if (GameObject.Find("Persistent") == null){
			Instantiate(persistentPref, Vector3.zero, Quaternion.identity);
		}
	}
}
