using UnityEngine;
using System.Collections;

public class script_persistent : MonoBehaviour {
	
	public int current_level = 0;
	public int furthest_level = 0;
	public int last_level = 0;
	
	public GUIStyle restartStyle;
	
	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
	
	void OnLevelWasLoaded(int level) {
		if (level > furthest_level) {
			furthest_level = level;
		}
	}
	
	void OnGUI() {
		if (current_level > 0) {
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8 - Screen.width/15, Screen.height/2  - Screen.height/8 + Screen.width/15, Screen.width/15, Screen.width/15), "", restartStyle)) {
				Application.LoadLevel(current_level);
			}
		}
	}
}
