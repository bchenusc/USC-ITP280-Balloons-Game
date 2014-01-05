using UnityEngine;
using System.Collections;

public class InputManager {

	/*
	 * How to use:
	 * 
	 * 1. Create an input manager in your singleton.
	 * 2. Each script that needs to use this script must register to the event.
	 * 
	 */
	
	public delegate void Action();
	public delegate void ActionKey(KeyCode param);

	#region Events
	public event Action OnMouseClick;
	public event ActionKey OnKeyDown;

	#endregion

	// Update is called once per frame
	public void Update () {
		GetMouseClick();
		GetKeyDown();
	}

	private void GetKeyDown(){
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			if (OnKeyDown!=null)
				OnKeyDown(KeyCode.Alpha1);
		}
	}
	
	private void GetMouseClick(){
		if (Input.GetMouseButtonDown(0)){
			if (OnMouseClick!=null)
				OnMouseClick();
		}
	}



}





