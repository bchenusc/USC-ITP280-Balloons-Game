using UnityEngine;
using System.Collections;

public class script_button : MonoBehaviour {
	public Transform[] connectedDoors;
	
	bool pressed = false;
	bool beingSteppedOn = false;
	
	private OTAnimatingSprite sprite;
	
	// Use this for initialization
	void Start () {
		sprite = transform.GetComponent<OTAnimatingSprite>();
		sprite.speed = 0;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag("Player")) {
			if (!beingSteppedOn) {
				beingSteppedOn = true;
				pressed = !pressed;
				if (pressed) {
					foreach(Transform door in connectedDoors) {
						door.transform.rigidbody.collider.enabled = false;
						door.gameObject.GetComponent<OTAnimatingSprite>().PlayOnce();
					}
				} else {
					foreach(Transform door in connectedDoors) {
						door.transform.rigidbody.collider.enabled = true;
						door.gameObject.GetComponent<OTAnimatingSprite>().PlayOnceBackward();
					}
				}
				sprite.frameIndex = 1;
				//Debug.Log("Button " + ID + " is " + pressed);
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.transform.CompareTag("Player")) {
			beingSteppedOn = false;
			sprite.frameIndex = 0;
		}
	}
}
