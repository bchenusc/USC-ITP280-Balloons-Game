using UnityEngine;
using System.Collections;

public class script_falling_balloon_trigger : MonoBehaviour {
	
	script_player_move player_script;
	
	void Start(){
		player_script = GameObject.FindWithTag("Player").GetComponent<script_player_move>();	
	}
	

	void OnTriggerEnter(Collider other){
		if (other.transform.CompareTag("spikes") && player_script.has_balloon==-1){
			if (player_script.has_balloon==-1){
				player_script.changeHasBalloon(0);
				player_script.hitTop(0);
				player_script.destroyBalloon();
			}
		}
	}
}
