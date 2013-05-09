using UnityEngine;
using System.Collections;

public class script_persistent : MonoBehaviour {
	
	public int current_level = 0;
	public int furthest_level = 0;
	public int last_level = 0;
	
	public GUIStyle restartStyle;
	private bool displayRestart = false;
	
	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
	
	void OnLevelWasLoaded(int level) {
		if (level > furthest_level) {
			furthest_level = level;
		}
		
		if (level > 0) {
			displayRestart = true;
		} else {
			displayRestart = false;
		}
	}
	
	void OnGUI() {
		if (displayRestart) {
			if (GUI.Button(new Rect(Screen.width - Screen.width/13f, Screen.height/40f, Screen.width/15, Screen.width/15), "", restartStyle)) {
				Application.LoadLevel(current_level);
			}
		}
	}
}
