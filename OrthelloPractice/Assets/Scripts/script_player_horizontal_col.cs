using UnityEngine;
using System.Collections;

public class script_player_horizontal_col : MonoBehaviour {

	public bool usingLeft = true;
	
	script_player_move player_script;
	
	void Start(){
		player_script = transform.parent.GetComponent<script_player_move>();	
	}
	
	
	void OnTriggerEnter(Collider other){
		if (usingLeft && other.transform.CompareTag("tile")){
			player_script.tooFarLeft(true);
			player_script.stop_X_velocity();
		}
		if (!usingLeft && other.transform.CompareTag("tile")){
			player_script.tooFarRight(true);	
			player_script.stop_X_velocity();
		}
	}
	void OnTriggerExit(Collider other){
		if (usingLeft && other.transform.CompareTag("tile")){
			player_script.tooFarLeft(false);
		}
		if (!usingLeft && other.transform.CompareTag("tile")){
			player_script.tooFarRight(false);
		}
	}
}
