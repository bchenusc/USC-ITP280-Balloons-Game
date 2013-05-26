using UnityEngine;
using System.Collections;

public class script_main_menu : MonoBehaviour {
	private script_persistent persistent;
	
	public GUIStyle menuStyle;
	
	
	// Use this for initialization
	void Start () {
		persistent = GameObject.Find("Persistent(Clone)").gameObject.GetComponent<script_persistent>();
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8 - Screen.width/15, Screen.height/2  - Screen.height/8 + Screen.width/15, Screen.width/15, Screen.width/15), "", menuStyle)) {
			persistent.current_level = 1;
			Application.LoadLevel(1);
		}
	}
}
