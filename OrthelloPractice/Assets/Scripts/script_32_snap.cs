using UnityEngine;
using System.Collections;

public class script_32_snap : MonoBehaviour {
	
	public float snap_to_x = 32f, snap_to_y = 32f;

	// Use this for initialization
	void Start () {
		
		//Make correct size
		if (transform.localScale.x >32){
			transform.localScale = new Vector3((int)(transform.localScale.x / 32) * 32, transform.localScale.y, transform.localScale.z);
		}
		if(transform.localScale.y>32){
			transform.localScale = new Vector3(transform.localScale.x, (int)(transform.localScale.y/32)*32, transform.localScale.z);	
		}
		
		//Snap
		float new_x, new_y;
		
		if ((transform.localScale.x / snap_to_x) % 2 == 0) {
			new_x = transform.position.x - transform.position.x % snap_to_x + snap_to_x;
		} else {
			new_x = transform.position.x - transform.position.x % snap_to_x + snap_to_x/2;
		}
		
		if ((transform.localScale.y / snap_to_y) % 2 == 0) {
			new_y = transform.position.y - transform.position.y % snap_to_y + snap_to_y;
		} else {
			new_y = transform.position.y - transform.position.y % snap_to_y + snap_to_y/2;
		}
		
		transform.position = new Vector2(new_x, new_y);
		

		
	}
}
