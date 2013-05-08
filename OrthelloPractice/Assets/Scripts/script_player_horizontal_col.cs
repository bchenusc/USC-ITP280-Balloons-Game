using UnityEngine;
using System.Collections;

public class script_player_horizontal_col : MonoBehaviour {

	public bool usingLeft = true;
	
	script_player_move player_script;
	
	void Start(){
		player_script = transform.parent.GetComponent<script_player_move>();
		changeToBalloonSize(false);
	}
	
	
	void OnTriggerEnter(Collider other){
		if (usingLeft && other.transform.CompareTag("tile")){
			player_script.tooFarLeft(true);
		}
		if (!usingLeft && other.transform.CompareTag("tile")){
			player_script.tooFarRight(true);	
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
	
	public void changeToBalloonSize(bool change){
		if (change){
			transform.position = new Vector3(transform.position.x, transform.parent.position.y+17, transform.position.z);
			transform.localScale=new Vector3(0.2f, 2, 400);
			
		}
		else {
			transform.position = new Vector3 (transform.position.x, transform.parent.position.y-0.03f, transform.position.z);
			transform.localScale = new Vector3(0.2f, 0.75f, 400);
			
		}
	}
}
