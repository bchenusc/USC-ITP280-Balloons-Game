using UnityEngine;
using System.Collections;

/*
	 * How to use:
	 * 
	 * 1. Create an input manager in your singleton.
	 * 2. Each script that needs to use this script must register to the event.
	 * 
	 * @Brian Chen
	 */


public class InputManager {


	#region Parameters
	public float f_restartButtonRadius = 0.48f;
	#endregion

	#region cache
	public Transform t_restartButton;
	#endregion

	#region delegates
	public delegate void Action();
	public delegate void ActionKey(KeyCode param);
	#endregion

	#region Events
	public event Action OnMouseClick;
	public event ActionKey OnKeyDown;
	#endregion
	
	public void OnDestroy(){
		OnMouseClick-= TouchRestart;
	}

	public void Start(){
		OnLevelLoaded(0);
		OnMouseClick+= TouchRestart;
	}

	public void OnLevelLoaded(int i){
		//Cache the restart button
		t_restartButton = GameObject.FindGameObjectWithTag("RestartButton").transform;

	}
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
			if (OnMouseClick!=null){
				OnMouseClick();
			}

		}
	}

	private void TouchRestart(){
		Vector3 mousePositionRaw = Input.mousePosition;
		mousePositionRaw.z = 7;
		Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePositionRaw);
		worldMouse.z = 0;
		if (Vector3.SqrMagnitude(worldMouse - t_restartButton.position)
		    <= Mathf.Pow(f_restartButtonRadius, 2)){
			GameState.Get.FadeOutToRestartLevel();
		}
	}

}





