using UnityEngine;
using System.Collections;

public class script_restart_button : MonoBehaviour {
	private script_persistent persistent;
	private OTAnimatingSprite sprite;
	bool pressed;
	
	// Use this for initialization
	void Start () {
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - transform.localScale.x/2, Screen.height - transform.localScale.y/2, 0));
		
		persistent = GameObject.Find("Persistent(Clone)").gameObject.GetComponent<script_persistent>();
		sprite = transform.GetComponent<OTAnimatingSprite>();
		sprite.speed = 0;
		pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit)){
				if (hit.transform.name == "pref_restart_button") {
					sprite.frameIndex = 1;
					pressed = true;
					Application.LoadLevel(persistent.current_level);
					return;
				}
			}
		}
		
		if (Input.GetMouseButtonUp(0)) {
			sprite.frameIndex = 0;
			if (pressed) {
				pressed = false;
			}
		}
	}
}