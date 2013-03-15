using UnityEngine;
using System.Collections;

public class script_player_grounded : MonoBehaviour {
	script_player_move player_script;
	
	void Start(){
		player_script = transform.parent.GetComponent<script_player_move>();	
	}
	
	void OnTriggerEnter(Collider other){
		if (other.transform.CompareTag("tile")){
			player_script.changeGrounded(true);	
		}
	}
	void OnTriggerExit(Collider other){
		if (other.transform.CompareTag ("tile")){
			player_script.changeGrounded(false);
		}
	}
}
