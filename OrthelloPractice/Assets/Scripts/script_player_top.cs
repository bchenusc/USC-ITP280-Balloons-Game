using UnityEngine;
using System.Collections;

public class script_player_top : MonoBehaviour {
		script_player_move player_script;
	
	void Start(){
		player_script = transform.parent.GetComponent<script_player_move>();	
	}
	
	void OnTriggerEnter(Collider other){
		if (other.transform.CompareTag("spikes")){
			if (player_script.has_balloon){
				player_script.destroyBalloon();
			}
		}
	}
}