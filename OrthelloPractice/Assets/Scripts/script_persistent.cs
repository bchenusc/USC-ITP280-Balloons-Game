using UnityEngine;
using System.Collections;

public class script_persistent : MonoBehaviour {
	
	public int current_level = 0;
	public int last_level = 0;
	
	void Awake(){
	   DontDestroyOnLoad (transform.gameObject);
	}
}
