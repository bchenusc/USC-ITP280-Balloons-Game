using UnityEngine;
using System.Collections;

/*
 * How to use script:
 * 1. Place DoorButton.cs on the DoorButton/Trigger prefab.
 * 2. In editor, link the DoorPrefab as t_door.
 * 3. Make sure the BoxCollider2D is a trigger.
 * 
 * @Brian Chen
 */

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class DoorButton : MonoBehaviour {

	public Transform t_door;
	Door scr_doorScript;
	Animator animator;

	//There is currently a problem with OnTriggerExit2D. I am hacking this to make it work.
	bool HACK_canReTrigger = true;

	public bool b_IsPressed = false;

	void Start(){
		scr_doorScript = t_door.GetComponent<Door>();
		animator = transform.GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Player") && HACK_canReTrigger){
			HACK_canReTrigger = false; //HACK--Used only because OnTriggerEnter2D in Unity is buggy!
			b_IsPressed = !b_IsPressed;
			animator.SetBool("ButtonPress", true);
			scr_doorScript.ToggleDoor();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag("Player") && !HACK_canReTrigger){
			HACK_canReTrigger = true;
			animator.SetBool("ButtonPress", false);
		}
	}



}





