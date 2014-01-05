using UnityEngine;
using System.Collections;

public class script_player_grounded : MonoBehaviour {
	script_player_move player_script;
		
	void Start(){
		player_script = transform.parent.GetComponent<script_player_move>();

	}
	
	void OnTriggerStay(Collider other){
		//Grounded
		if (player_script.has_balloon==0&&other.transform.CompareTag("tile")){
			player_script.changeGrounded(true);	
		}
		
	}
	
	void OnTriggerEnter(Collider other){
		if (other.transform.CompareTag("spikes")&&player_script.has_balloon==-1){
				player_script.changeHasBalloon(0);
				player_script.destroyBalloon();
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.transform.CompareTag ("tile")){
			player_script.changeGrounded(false);
		}
	}

}
