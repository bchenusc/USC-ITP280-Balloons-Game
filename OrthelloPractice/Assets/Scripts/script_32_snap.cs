using UnityEngine;
using System.Collections;

//comment comment

public class script_32_snap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float new_x, new_y;
		
		new_x = transform.position.x - transform.position.x % 32f + 16;
		new_y = transform.position.y - transform.position.y % 32f + 16;
		
		transform.position = new Vector2(new_x, new_y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
