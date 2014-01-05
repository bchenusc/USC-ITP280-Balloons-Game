using UnityEngine;
using System.Collections;

/*
 * How to use script:
 * 1. Place Door.cs on the Door prefab.
 * 2. In editor, link the DoorButton/Trigger as t_button.
 * 
 * @Brian Chen
 */


[RequireComponent (typeof (Animator))]
public class Door : MonoBehaviour {

	public Transform t_button;

	Animator animator;

	public bool b_IsOpen = false; //changeable from the inspector.
	
	void Start(){
		animator = transform.GetComponent<Animator>();
	}

	#region Used By Other Scripts
	public void ToggleDoor(){
		b_IsOpen = !b_IsOpen;
		animator.SetBool("DoorOpen", b_IsOpen);
	}

	#endregion
	
}









