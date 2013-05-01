using UnityEngine;
using System.Collections;

public class script_transition : MonoBehaviour {

	script_persistent persistentScript;
	
	void Start(){
		persistentScript = GameObject.Find("Persistent").GetComponent<script_persistent>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag("Player")) {
			Debug.Log("HellO");
			if (persistentScript.current_level != persistentScript.last_level){
				persistentScript.current_level+=1;
				Application.LoadLevel(persistentScript.current_level);
			}
		}
	}
}	
