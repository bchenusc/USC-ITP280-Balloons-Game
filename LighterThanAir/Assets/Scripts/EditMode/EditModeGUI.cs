using UnityEngine;
using System.Collections;

/*
 * How to use:
 * 
 * 1. Place script on singleton.
 * 2. Press '1' on keyboard when game is playing to toggle Edit Mode.
 * 3. On actual build make sure to remove this script.
 * 
 * Edit Mode:
 * 
 * 1. Select an object to spawn.
 * 2. Go back into game mode and click in where you want the object to spawn.
 * 
 */

public class EditModeGUI : MonoBehaviour {

	bool b_guiOn = false;

	//Prefabs
	public Transform prefab_Player;
	public Transform prefab_Balloon;
	public Transform prefab_BlockCol;
	public Transform prefab_Block;
	public Transform prefab_Spike;
	public Transform prefab_DoorCombo;
	public Transform prefab_NextLevelTrigger;
	public Transform prefab_Background;

	//Tools bar
	private int toolbarInt = -1;
	private string[] toolbarStrings = {
		"None", //0
		"Add", //1
		"Move" //2
	};
	private enum ToolBarState{
		none,
		add,
		move,
	};
	private ToolBarState toolbar;

	//Selection bar
	private int selectionGridInt = 0;
	private string[] selectionStrings = {
		"Remove", //--0
		"Block", //--1
		"BlockCol", //--2
		"Player", //--3
		"Background", //--4
		"EndLvlTrigger", //--5
		"Balloon", //--6
		"Spike", //--7
		"DoorCombo"//--8
	};



	void Start(){
		#region Delegate registration
		GameState.Get.InputManager.OnMouseClick += MouseClick;
		GameState.Get.InputManager.OnKeyDown += OnKeyDown;
		#endregion

		OnEnabled();

	}
	void OnDestroy(){
		#region Delegate unregistration
		try{
		GameState.Get.InputManager.OnMouseClick -= MouseClick;
		GameState.Get.InputManager.OnKeyDown -= OnKeyDown;
		}catch{};
		#endregion
	}

	void OnGUI () {
		if (!b_guiOn) return;
		#region background box
		// Make a background box
		GUI.Box(new Rect(0,0,Screen.width,Screen.height), "EditMode - Tools");
		#endregion

		#region Export button
		//Make the export button
		if (GUI.Button (new Rect (5, Screen.height-35, 90, 30), "Export")) {
			Button_Export();
		}
		#endregion

		#region toolbar
		//make the toolbar
		toolbarInt = GUI.Toolbar (new Rect (25, 25, 250, 30), toolbarInt, toolbarStrings);
		if (toolbarInt!= -1){
			#region handle toolbar int
			switch(toolbarInt){
			case 0: toolbar = ToolBarState.none;break;
			case 1: toolbar = ToolBarState.add; break;
			case 2: toolbar = ToolBarState.move; break;
			default: toolbar = ToolBarState.none; break;
			}
			#endregion
			toolbarInt = -1;
		}
		#endregion


		#region add selection grid
		if (toolbar == ToolBarState.add){
			selectionGridInt = GUI.SelectionGrid (new Rect (25, 75, Screen.width-50, 70), selectionGridInt, selectionStrings, 4);
		}
		#endregion

	}

	#region GUI Button Functions
	private void Button_Export(){
		//export to txt here.
	}

	private Transform SelectionGrid_Block(){
		switch(selectionGridInt){
		case 1:
			//Spawn a block here.
			return prefab_Block;

		default: return null;
		}
	}

	#region Helper Gui functions
	private void helper_ChangeTool(){
		selectionGridInt = -1;
	}

	#endregion

	#endregion

	void OnEnabled(){
		helper_MakeAllEditable();
	}
	void OnDisabled(){
		helper_MakeAllDisable();
	}

	#region Make Editable Helper
	private void helper_MakeAllEditable(){
		GameObject[] gameObjs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach(GameObject g in gameObjs){
			if (helper_IsEditable(g)){
				Editable editableScript = g.GetComponent<Editable>();
				if (editableScript != null){
					editableScript.enabled = true;
				}
				else{
					helper_AddEditableScript(g);
				}
			}
		}
	}
	private void helper_AddEditableScript(GameObject gameObj){
		gameObj.AddComponent<Editable>();
	}
	private void helper_MakeAllDisable(){
		GameObject[] gameObjs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach(GameObject g in gameObjs){
			try{
				g.GetComponent<Editable>().enabled = false;
			}catch{}
		}
	}
	private bool helper_IsEditable(GameObject g){
		if (
			g.CompareTag("Block")||
		    g.CompareTag("BlockCol")||
		    g.CompareTag("BalloonUp")||
		    g.CompareTag("BalloonDown")||
		    g.CompareTag("Spike")||
		    g.CompareTag("Door")||
		    g.CompareTag("DoorTrigger")
		    )
		{
			return true;
		}
		return false;
	}
	#endregion


	#region Update functions
	private void MouseClick(){

		if (selectionGridInt > 0){
			Vector3 mousePositionRaw = Input.mousePosition;
			mousePositionRaw.z = 7;
			Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePositionRaw);
			Debug.Log(worldMouse);
			Transform clone = Instantiate(SelectionGrid_Block(), 
			            new Vector3(worldMouse.x,
			            			worldMouse.y, 
			            			0),
			            Quaternion.identity) as Transform;
			helper_AddEditableScript(clone.gameObject);
			clone.gameObject.AddComponent<BoxCollider2D>();
			clone.GetComponent<Editable>().Snap();
		}
		else{
			//Check if the selection grid is -2 (for removal of objects)
			Debug.Log (selectionGridInt);
			if (selectionGridInt == 0){
				//Raycast to the screen and remove the object that is there.
				Vector3 mousePositionRaw = Input.mousePosition;
				//mousePositionRaw.z = 8;

				Vector3 wp = Camera.main.ScreenToWorldPoint(mousePositionRaw);
				Vector2 touchPos = new Vector2(wp.x, wp.y);
				if (collider2D == Physics2D.OverlapPoint(touchPos))
				{
					//your code
					
				}
			}
		}
	}

	private void OnKeyDown(KeyCode key){
		if (key == KeyCode.Alpha1){
			b_guiOn = !b_guiOn;
			Debug.Log ("Toggling Edit Mode GUI");
		}
	}

	#endregion

}







