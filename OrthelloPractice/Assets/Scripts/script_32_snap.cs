using UnityEngine;
using System.Collections;

//comment comment

public class script_32_snap : MonoBehaviour {
	
	public float snap_to_x = 32f, snap_to_y = 32f;

	// Use this for initialization
	void Start () {
		float new_x, new_y;
		
		new_x = transform.position.x - transform.position.x % snap_to_x + snap_to_x/2;
		new_y = transform.position.y - transform.position.y % snap_to_y + snap_to_y/2;
		
		transform.position = new Vector2(new_x, new_y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
