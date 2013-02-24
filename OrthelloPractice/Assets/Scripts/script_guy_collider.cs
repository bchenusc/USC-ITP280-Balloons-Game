using UnityEngine;
using System.Collections;

public class script_guy_collider : MonoBehaviour {
	private script_guy_control guy;
	
	// Use this for initialization
	void Start () {
		guy = transform.parent.GetComponent<script_guy_control>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("tile"))
			guy.changeGrounded(true);
	}
	
	void OnTriggerExit(Collider other) {
		if (other.CompareTag("tile"))
			guy.changeGrounded(false);
	}
}
