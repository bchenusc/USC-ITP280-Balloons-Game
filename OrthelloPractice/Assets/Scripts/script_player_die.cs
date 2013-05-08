using UnityEngine;
using System.Collections;

public class script_player_die : MonoBehaviour {

	script_player_move player_script;
		
	void Start(){
		player_script = transform.parent.GetComponent<script_player_move>();
	}
	
	void OnTriggerEnter(Collider other){
		if (other.transform.CompareTag("spikes")){
			player_script.player_death();	
		}
	}
}
