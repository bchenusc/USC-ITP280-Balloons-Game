using UnityEngine;
using System.Collections;

public class script_balloon_pop : MonoBehaviour {
	
	//Place script on popper
	
	Transform parent, balloon_collider;
	script_guy_control playerScript;
	
	// Use this for initialization
	void Start () {
		parent = transform.parent;
		balloon_collider = transform.parent.FindChild("col_balloon_collider");
		playerScript = GameObject.FindWithTag("Player").GetComponent<script_guy_control>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c){
		Debug.Log (c.transform.tag);
		if (c.transform.CompareTag("spikes")){
			parent.DetachChildren();
			Destroy(balloon_collider.gameObject);
			Destroy(parent.gameObject);
			//Change the player back to normal settings.
			playerScript.changeHasBalloon(false);
			playerScript.changeGrounded(false);
			playerScript.changeGravity(true);
			
			Destroy(gameObject);
		}
	}
}
